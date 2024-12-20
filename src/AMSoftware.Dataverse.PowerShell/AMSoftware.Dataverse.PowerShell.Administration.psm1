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


# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Add-DataverseLanguage {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true)]
        [Alias('Language', 'LCID')]
        [ValidateRange(1, 99999)]
        [int]$Locale
    )

    process {
        Send-DataverseRequest -Name 'ProvisionLanguage' -Parameters @{Language = $Locale } | Out-Null
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Get-DataverseLanguage {
    [CmdletBinding()]
    [OutputType([System.Globalization.CultureInfo])]
    param (
        [Parameter()]
        [switch]$All
    )

    process {

        if ($All) {
            $response = Send-DataverseRequest -Name 'RetrieveAvailableLanguages'
            $languageIds = $response.LocaleIds
        }
        else {
            $response = Send-DataverseRequest -Name 'RetrieveProvisionedLanguages'
            $languageIds = $response.RetrieveProvisionedLanguages;
        }

        $cultures = $languageIds | Select-Object @{
            n = 'Culture';
            e = { [System.Globalization.CultureInfo]::GetCultureInfo($_) }
        }

        Write-Output $cultures
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Remove-DataverseLanguage {
    [CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
    param (
        [Parameter(Mandatory = $true)]
        [Alias('Language', 'LCID')]
        [ValidateRange(1, 99999)]
        [int]$Locale,
        [switch]$Force
    )

    process {
        if ($Force -and -not $PSBoundParameters.ContainsKey('Confirm')) {
            $ConfirmPreference = 'None'
        }

        if ($PSCmdlet.ShouldProcess("Deprovision Language ($Locale)", $DataverseClient.ConnectedOrgFriendlyName)) {
            Send-DataverseRequest -Name 'DeprovisionLanguage' -Parameters @{Language = $Locale } | Out-Null
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Get-DataverseEnvironmentVariableValue {
    [CmdletBinding()]
    [OutputType([string])]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('EnvironmentVariable')]
        [ValidateNotNullOrEmpty()]
        [string]$Name
    )

    process {
        Send-DataverseRequest -Name 'RetrieveEnvironmentVariableValue' -Parameters @{ 'DefinitionSchemaName' = $Name } | Select-Object -ExpandProperty 'Value'
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Get-DataverseSPDocumentLocation {
    [CmdletBinding()]
    [OutputType([Microsoft.Crm.Sdk.Messages.RetrieveAbsoluteAndSiteCollectionUrlResponse])]
    param (
        [Parameter(Mandatory = $true, ParameterSetName = 'GetBySharePointDocumentLocation')]
        [ValidateNotNullOrEmpty()]
        [guid]$DocumentLocation,

        [Parameter(Mandatory = $true, ParameterSetName = 'GetByRegardingObject')]
        [ValidateNotNullOrEmpty()]
        [guid]$RegardingObject
    )

    process {
        switch ($PSCmdlet.ParameterSetName) {
            'GetBySharePointDocumentLocation' {
                Send-DataverseRequest -Name 'RetrieveAbsoluteAndSiteCollectionUrl' -TargetTable 'sharepointdocumentlocation' -TargetRow $DocumentLocation
            }
            'GetByRegardingObject' {
                $documentlocations = Get-DataverseRows -Table 'sharepointdocumentlocation' -Query @{'regardingobjectid' = $RegardingObject }
                $documentlocations | ForEach-Object {
                    Send-DataverseRequest -Name 'RetrieveAbsoluteAndSiteCollectionUrl' -TargetTable 'sharepointdocumentlocation' -TargetRow $_.Id
                }
            }
            Default {}
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
function Invoke-DataverseWorkflow {
    [CmdletBinding()]
    [OutputType([guid])]
    param (
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('Id')]
        [ValidateNotNullOrEmpty()]
        [guid]$Row,

        [Parameter(Mandatory = $true)]
        [Alias('Process')]
        [ValidateNotNullOrEmpty()]
        [guid]$Workflow
    )

    process {
        Send-DataverseRequest -Name 'ExecuteWorkflow' -Parameters @{
            'EntityId' =  $Row;
            'WorkflowId' = $Workflow
        } | Select-Object -ExpandProperty 'Id'
    }
}