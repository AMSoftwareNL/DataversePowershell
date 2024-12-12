# PowerShell Module for Power Platform Dataverse
# Copyright(C) 2024  AMSoftwareNL
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <https://www.gnu.org/licenses/>.

function Wait-AsyncOperation {
    param (
        [guid]$AsyncOperationId,
        [string]$ProgressActivity
    )
    
    $progressActivityId = [System.Random]::new().Next()

    # Wait for asyncjob to complete (success or fail)
    $exportStart = [datetime]::UtcNow
    $checkcount = 0
    $loopcount = 1
    do {
        Write-Progress -Id $progressActivityId -Activity $ProgressActivity -Status ([datetime]::UtcNow - $exportStart).ToString('c')
        Start-Sleep -Seconds 1

        $checkcount++
        if (($checkcount % (10 * $loopcount)) -eq 0) {
            $asyncOperation = Get-DataverseRow -Table 'asyncoperation' -Id $AsyncOperationId -Columns @(
                'statecode',
                'statuscode',
                'message'
            )
            if ($loopcount -gt 10) {
                $loopcount = 10
            }
            else {
                $loopcount++
            }
            $checkcount = 0
        }
    } until ($asyncOperation.statecode -eq 3)
    Write-Progress -Id $progressActivityId -Completed

    Write-Output $asyncOperation
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Export-DataverseWebResource {
    [CmdletBinding()]
    [OutputType([System.IO.FileInfo])]
    param ( 
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [guid]$Id,

        [Parameter(Mandatory = $true)]
        [string]$OutputPath
    )

    begin {
        $resolvedPath = Resolve-Path -Path $OutputPath
    }

    process { 
        $resource = Get-DataverseRow -Table 'webresource' -Id $Id -Columns 'name', 'content', 'contentfileref'

        # Only keep filename part of the webresource path
        $resourceFilename = $resource.Name
        [System.IO.Path]::GetInvalidFileNameChars() | ForEach-Object { $resourceFilename = $resourceFilename.Replace($_, '-') }

        $resourceFilepath = Join-Path -Path $resolvedPath -ChildPath $resourceFilename

        if (-not [string]::IsNullOrWhiteSpace($resource.content)) {
            # Content is in the table
            Set-Content -LiteralPath $resourceFilepath -Value ([System.Convert]::FromBase64String($resource.content)) -AsByteStream -Force
        }
        else {
            # Content is a fileattachment
            Export-DataverseFile -Table 'webresource' -Row $Id -Column 'contentfileref' | `
                Set-Content -LiteralPath $resourceFilepath -Force
        }
        Get-Item -LiteralPath $resourceFilepath
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Export-DataversePluginAssembly {
    [CmdletBinding()]
    [OutputType([System.IO.FileInfo])]
    param ( 
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [guid]$Id,

        [Parameter(Mandatory = $true)]
        [string]$OutputPath
    )

    begin {
        $resolvedPath = Resolve-Path -Path $OutputPath
    }

    process { 
        $assembly = Get-DataverseRow -Table 'pluginassembly' -Id $Id -Columns 'name', 'path', 'content'
        $assemblyFilepath = Join-Path -Path $resolvedPath -ChildPath $assembly.path

        if (-not [string]::IsNullOrWhiteSpace($assembly.content)) {
            # Content is in the table
            Set-Content -LiteralPath $assemblyFilepath -Value ([System.Convert]::FromBase64String($assembly.content)) -AsByteStream -Force        
            Get-Item -LiteralPath $assemblyFilepath
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Export-DataverseTranslation {
    [CmdletBinding()]
    [OutputType([System.IO.FileInfo])]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [ValidateNotNull()]
        [Alias('Name')]
        [string]$SolutionName,

        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$OutputPath
    )

    process {
        $solution = Get-DataverseRows -Table 'solution' -Query @{uniquename = $SolutionName } -Top 1

        $folderPath = Resolve-Path $OutputPath
        $filePath = Join-Path -Path $folderPath -ChildPath "$($solution.uniquename)_translation.zip"

        $result = Send-DataverseRequest -Name 'ExportTranslation' -Parameters @{'SolutionName' = $solution.uniquename }
        
        Set-Content -LiteralPath $filePath -Value $result.ExportTranslationFile -AsByteStream -Force
        Get-Item -LiteralPath $filePath
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Import-DataverseTranslation {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [Alias('PSPath')]
        [ValidateNotNullOrEmpty()]
        [string]$LiteralPath
    )

    process {
        $translationsPath = Resolve-Path -LiteralPath $LiteralPath

        $asyncResponse = Send-DataverseRequest -Name 'ImportTranslationAsync' -Parameters @{
            'TranslationFile' = Get-Content -LiteralPath $translationsPath -Raw
            'ImportJobId'     = [guid]::Newguid()
        }

        $asyncOperation = Wait-AsyncOperation `
            -AsyncOperationId $asyncResponse.AsyncOperationId `
            -ProgressActivity "Import Translation: $translationsPath"

        if ($asyncOperation.statuscode -eq 30) {
        }
        else {
            Write-Error -Message $asyncOperation.message
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Export-DataverseSolution {
    [CmdletBinding()]
    [OutputType([System.IO.FileInfo])]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [ValidateNotNull()]
        [Alias('Name')]
        [string]$SolutionName,

        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$OutputPath,

        [Parameter()]
        [switch]$AsManaged
    )

    process {
        $solution = Get-DataverseRows -Table 'solution' -Query @{uniquename = $SolutionName } -Top 1

        if ($AsManaged.ToBool()) {
            $filename = "$($solution.uniquename)_$($solution.version.Replace('.','_'))_managed.zip"
        }
        else {
            $filename = "$($solution.uniquename)_$($solution.version.Replace('.','_')).zip"
        }

        $folderPath = Resolve-Path $OutputPath
        $filePath = Join-Path -Path $folderPath -ChildPath $filename

        $asyncResponse = Send-DataverseRequest -Name 'ExportSolutionAsync' -Parameters @{
            SolutionName = $SolutionName;
            Managed      = $AsManaged.ToBool();
        }

        $asyncOperation = Wait-AsyncOperation `
            -AsyncOperationId $asyncResponse.AsyncOperationId `
            -ProgressActivity "Export Solution: $($solution.uniquename)"

        if ($asyncOperation.statuscode -eq 30) {
            $result = Send-DataverseRequest -Name 'DownloadSolutionExportData' -Parameters @{
                ExportJobId = $asyncResponse.ExportJobId
            }
    
            Set-Content -LiteralPath $filePath -Value $result.ExportSolutionFile -AsByteStream -Force
            Get-Item -LiteralPath $filePath
        }
        else {
            Write-Error -Message $asyncOperation.message
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Import-DataverseSolution {
    [CmdletBinding(DefaultParameterSetName = 'ImportSolution')]
    [OutputType([guid])]
    [OutputType([Microsoft.Xrm.Sdk.StageSolutionResults])]
    param (
        [Parameter(Mandatory = $true)]
        [Alias('PSPath')]
        [ValidateNotNullOrEmpty()]
        [string]$LiteralPath,

        [Parameter(ParameterSetName = 'ImportAsStaged')]
        [switch]$Stage,

        [Parameter(ParameterSetName = 'ImportAsUpgrade')]
        [switch]$Upgrade,

        [Parameter()]
        [switch]$Overwrite,

        [Parameter(ParameterSetName = 'ImportSolution')]
        [switch]$Hold,

        [Parameter(ParameterSetName = 'ImportSolution')]
        [Parameter(ParameterSetName = 'ImportAsUpgrade')]
        [switch]$PublishWorkflows,

        [Parameter(ParameterSetName = 'ImportSolution')]
        [Parameter(ParameterSetName = 'ImportAsUpgrade')]
        [Microsoft.Xrm.Sdk.EntityCollection]$ComponentParameters
    )

    process {
        $solutionPath = Resolve-Path -LiteralPath $LiteralPath

        switch ($PSCmdlet.ParameterSetName) {
            'ImportAsStaged' {
                $stageResponse = Send-DataverseRequest -Name 'StageSolution' -Parameters @{
                    CustomizationFile = (Get-Content -LiteralPath $solutionPath -Raw);
                }

                Write-Output $stageResponse.StageSolutionResults
            }
            'ImportAsUpgrade' {
                $asyncResponse = Send-DataverseRequest -Name 'StageAndUpgradeAsync' -Parameters @{
                    CustomizationFile                = (Get-Content -LiteralPath $solutionPath -Raw);
                    OverwriteUnmanagedCustomizations = $Overwrite.ToBool();
                    PublishWorkflows                 = $PublishWorkflows.ToBool();
                    ComponentParameters              = $ComponentParameters;
                }

                $asyncOperation = Wait-AsyncOperation `
                    -AsyncOperationId $asyncResponse.AsyncOperationId `
                    -ProgressActivity "Stage and Upgrade Solution: $solutionPath"

                if ($asyncOperation.statuscode -eq 30) {
                    Write-Output $asyncResponse.ImportJobKey
                }
                else {
                    Write-Error -Message $asyncOperation.message
                }
            }
            Default {
                $asyncResponse = Send-DataverseRequest -Name 'ImportSolutionAsync' -Parameters @{
                    CustomizationFile                = (Get-Content -LiteralPath $solutionPath -Raw);
                    HoldingSolution                  = $Hold.ToBool();
                    OverwriteUnmanagedCustomizations = $Overwrite.ToBool();
                    PublishWorkflows                 = $PublishWorkflows.ToBool();
                    ComponentParameters              = $ComponentParameters;
                }

                $asyncOperation = Wait-AsyncOperation `
                    -AsyncOperationId $asyncResponse.AsyncOperationId `
                    -ProgressActivity "Import Solution: $solutionPath"

                if ($asyncOperation.statuscode -eq 30) {
                    Write-Output $asyncResponse.ImportJobKey
                }
                else {
                    Write-Error -Message $asyncOperation.message
                }            
            }
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Update-DataverseSolution {
    [CmdletBinding()]
    [OutputType([guid])]
    param (
        [Parameter(Mandatory = $true, ParameterSetName = 'UpgradeFromHold')]
        [ValidateNotNullOrEmpty()]
        [Alias('UniqueName')]
        [string]$SolutionName,

        [Parameter(Mandatory = $true, ParameterSetName = 'UpgradeFromStaged')]
        [guid]$Stage,

        [Parameter(ParameterSetName = 'UpgradeFromStaged')]
        [switch]$PublishWorkflows,

        [Parameter(ParameterSetName = 'UpgradeFromStaged')]
        [Microsoft.Xrm.Sdk.EntityCollection]$ComponentParameters
    )

    process {
        switch ($PSCmdlet.ParameterSetName) {
            'UpgradeFromHold' {
                $solution = Get-DataverseRows -Table 'solution' -Query @{uniquename = $SolutionName } -Top 1

                $asyncResponse = Send-DataverseRequest -Name 'DeleteAndPromoteAsync' -Parameters @{
                    UniqueName = $solution.uniquename;
                }

                $asyncOperation = Wait-AsyncOperation `
                    -AsyncOperationId $asyncResponse.AsyncOperationId `
                    -ProgressActivity "Upgrade Holding Solution: $($solution.uniquename)"

                if ($asyncOperation.statuscode -eq 30) {
                }
                else {
                    Write-Error -Message $asyncOperation.message
                }
            }
            'UpgradeFromStaged' {
                $solutionParameters = New-Object `
                    -TypeName 'Microsoft.Xrm.Sdk.SolutionParameters' `
                    -Property @{ StageSolutionUploadId = $Stage }

                $asyncResponse = Send-DataverseRequest -Name 'ImportSolutionAsync' -Parameters @{
                    SolutionParameters               = $solutionParameters;
                    OverwriteUnmanagedCustomizations = $Overwrite.ToBool();
                    PublishWorkflows                 = $PublishWorkflows.ToBool();
                    ComponentParameters              = $ComponentParameters;
                }

                $asyncOperation = Wait-AsyncOperation `
                    -AsyncOperationId $asyncResponse.AsyncOperationId `
                    -ProgressActivity "Upgrade Solution from Stage: $Stage"

                if ($asyncOperation.statuscode -eq 30) {
                    Write-Output $asyncResponse.ImportJobKey
                }
                else {
                    Write-Error -Message $asyncOperation.message
                }            
            }
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Publish-DataverseComponent {
    [CmdletBinding(DefaultParameterSetName = 'PublishAll')]
    param ( 
        [Parameter(Mandatory = $true, ParameterSetName = 'PublishComponent')]
        [ValidateSet('Table', 'Choice', 'WebResource')]
        [string]$Type,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'PublishComponent')]
        [ValidateNotNullOrWhiteSpace()]
        [string]$ComponentId
    )

    begin { 
        $ids = @()
    }

    process {
        switch ($PSCmdlet.ParameterSetName) {
            'PublishComponent' {
                $ids += $ComponentId
            }
        }
    }

    end {
        switch ($PSCmdlet.ParameterSetName) {
            'PublishAll' {
                $asyncResponse = Send-DataverseRequest -Name 'PublishAllXmlAsync'

                $asyncOperation = Wait-AsyncOperation `
                    -AsyncOperationId $asyncResponse.AsyncOperationId `
                    -ProgressActivity "Publish Customizations"

                if ($asyncOperation.statuscode -eq 30) {
                }
                else {
                    Write-Error -Message $asyncOperation.message
                }
            }
            'PublishComponent' {
                switch ($Type) {
                    'Table' {
                        $grouplabel = 'entities'
                        $itemlabel = 'entity'
                    }
                    'Choice' {
                        $grouplabel = 'optionsets'
                        $itemlabel = 'optionset'
                    }
                    'WebResource' {
                        $grouplabel = 'webresources'
                        $itemlabel = 'webresource'
                    }
                }

                $publishxml = $ids | `
                    Select-Object -Property @{l = 'Item'; e = { "<$($itemlabel)>$_</$($itemlabel)>" } } | `
                    Join-String -Property 'Item' -OutputPrefix "<$($grouplabel)>" -OutputSuffix "</$($grouplabel)>"
                $parameterxml = "<importexportxml>$publishxml</importexportxml>"

                Send-DataverseRequest -Name 'PublishXml' -Parameters @{ParameterXml = $parameterxml } | Out-Null
            }
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataverseDataProvider {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataversePlugin {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataversePluginStep {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataversePluginStepImage {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataverseServiceEndpoint {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Register-DataverseWebhook {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataverseDataProvider {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataversePlugin {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataversePluginAssembly {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataversePluginStep {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataversePluginStepImage {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
function Unregister-DataverseServiceEndpoint {
    [CmdletBinding()]
    param ( )
    begin { throw [System.NotImplementedException]::new() }
}