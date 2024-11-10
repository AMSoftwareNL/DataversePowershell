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


# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
function Get-DataverseUser {
    [CmdletBinding(DefaultParameterSetName = 'GetAllUsers')]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'GetUserById')]
        [ValidateNotNullOrEmpty()]
        [Guid]$Id,

        [Parameter(ParameterSetName = 'GetAllUsers')]
        [Alias('Include')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Name,

        [Parameter(ParameterSetName = 'GetAllUsers')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Exclude,

        [Parameter(ParameterSetName = 'GetAllUsers')]
        [switch]$Disabled,

        [Parameter(ParameterSetName = 'GetAllUsers')]
        [switch]$Licensed,

        [Parameter(ParameterSetName = 'GetAllUsers')]
        [switch]$Application
    )

    process {

        switch ($PSCmdlet.ParameterSetName) {
            'GetAllUsers' { 
                $nameFilterAttributes = @('fullname','domainname','internalemailaddress')

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Name')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Name)) {
                        $pattern = [WildcardPattern]::new($Name);
                        $filterOperator = 'like'
                        $filterValue = $pattern.ToWql()
                    }
                    else {
                        $filterOperator = 'eq'
                        $filterValue = $Name
                    }

                    $includeFilter += "<filter type=""or"">"
                    $includeFilter +=  $nameFilterAttributes |ForEach-Object { 
                        "<condition attribute=""{0}"" operator=""{1}"" value=""{2}"" />" -f $_, $filterOperator,$filterValue 
                    } | Join-String
                    $includeFilter += "</filter>"
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Exclude')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Exclude)) {
                        $pattern = [WildcardPattern]::new($Exclude);
                        $filterValue = $pattern.ToWql()
                        $filterOperator = 'not-like'
                    }
                    else {
                        $filterValue = $Exclude
                        $filterOperator = 'ne'
                    }

                    $excludeFilter += "<filter type=""and"">"
                    $excludeFilter +=  $nameFilterAttributes |ForEach-Object { 
                        "<condition attribute=""{0}"" operator=""{1}"" value=""{2}"" />" -f $_, $filterOperator,$filterValue 
                    } | Join-String
                    $excludeFilter += "</filter>"
                }

                if ($Application) {
                    $mainConditions += "<condition attribute=""applicationid"" operator=""not-null"" value="""" />"
                }
                else {
                    $mainConditions += "<condition attribute=""applicationid"" operator=""null"" value="""" />"
                }
                if ($Disabled) {
                    $mainConditions += "<condition attribute=""isdisabled"" operator=""eq"" value=""1"" />"
                }
                else {
                    $mainConditions += "<condition attribute=""isdisabled"" operator=""eq"" value=""0"" />"
                }

                if ($Licensed) {
                    $mainConditions += "<condition attribute=""islicensed"" operator=""eq"" value=""1"" />"
                }

                [xml]$fetchxml = 
                @"
                        <fetch>
                            <entity name="systemuser">
                                <all-attributes />
                                <filter type="and">
                                    $($mainConditions)
                                    $($includeFilter)
                                    $($excludeFilter)
                                </filter>
                                <order attribute="domainname" />
                            </entity>
                        </fetch>
"@
                Get-DataverseRows -FetchXml $fetchxml
            }
            'GetUserById' {
                Get-DataverseRow -Table 'systemuser' -Id $Id
            }
            Default {}
        }
    }
}

enum RoleInheritance {
    TeamOnly = 0;
    DirectAndTeam = 1;
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
function Get-DataverseRole {
    [CmdletBinding(DefaultParameterSetName = 'GetAllRoles')]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'GetRoleById')]
        [ValidateNotNullOrEmpty()]
        [Guid]$Id,

        [Parameter(ParameterSetName = 'GetAllRoles')]
        [Alias('Include')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Name,

        [Parameter(ParameterSetName = 'GetAllRoles')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Exclude,

        [Parameter(ParameterSetName = 'GetAllRoles')]
        [RoleInheritance]$Inheritance
    )

    process {

        switch ($PSCmdlet.ParameterSetName) {
            'GetAllRoles' { 
                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Name')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Name)) {
                        $pattern = [WildcardPattern]::new($Name);
                        $filterOperator = 'like'
                        $filterValue = $pattern.ToWql()
                    }
                    else {
                        $filterOperator = 'eq'
                        $filterValue = $Name
                    }

                    $includeFilter +=  "<condition attribute=""name"" operator=""{1}"" value=""{2}"" />" -f $filterOperator,$filterValue 
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Exclude')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Exclude)) {
                        $pattern = [WildcardPattern]::new($Exclude);
                        $filterValue = $pattern.ToWql()
                        $filterOperator = 'not-like'
                    }
                    else {
                        $filterValue = $Exclude
                        $filterOperator = 'ne'
                    }

                    $excludeFilter +=  "<condition attribute=""name"" operator=""{1}"" value=""{2}"" />" -f $filterOperator,$filterValue 
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Inheritance')) {
                    $mainConditions += "<condition attribute=""isinherited"" operator=""eq"" value=""$($Inheritance)"" />"
                }

                [xml]$fetchxml = 
                @"
                        <fetch>
                            <entity name="role">
                                <all-attributes />
                                <filter type="and">
                                    $($mainConditions)
                                    $($includeFilter)
                                    $($excludeFilter)
                                </filter>
                                <order attribute="name" />
                            </entity>
                        </fetch>
"@
                Get-DataverseRows -FetchXml $fetchxml
            }
            'GetRoleById' {
                Get-DataverseRow -Table 'role' -Id $Id
            }
            Default {}
        }
    }
}

enum TeamType {
    Owner = 0; 
    Access = 1; 
    AADSecurityGroup = 2; 
    AADOfficeGroup = 3;
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
function Get-DataverseTeam {
    [CmdletBinding(DefaultParameterSetName = 'GetAllTeams')]
    param(
        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ParameterSetName = 'GetTeamById')]
        [ValidateNotNullOrEmpty()]
        [Guid]$Id,

        [Parameter(ParameterSetName = 'GetAllTeams')]
        [Alias('Include')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Name,

        [Parameter(ParameterSetName = 'GetAllTeams')]
        [ValidateNotNullOrEmpty()]
        [SupportsWildcards()]
        [string]$Exclude,

        [Parameter(ParameterSetName = 'GetAllTeams')]
        [TeamType]$TeamType,

        [Parameter(ParameterSetName = 'GetAllTeams')]
        [ValidateNotNullOrEmpty()]
        [guid]$BusinessUnit,

        [Parameter(ParameterSetName = 'GetAllTeams')]
        [ValidateNotNullOrEmpty()]
        [guid]$Administrator
    )

    process {

        switch ($PSCmdlet.ParameterSetName) {
            'GetAllTeams' { 
                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Name')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Name)) {
                        $pattern = [WildcardPattern]::new($Name);
                        $filterOperator = 'like'
                        $filterValue = $pattern.ToWql()
                    }
                    else {
                        $filterOperator = 'eq'
                        $filterValue = $Name
                    }

                    $includeFilter +=  "<condition attribute=""name"" operator=""{1}"" value=""{2}"" />" -f $filterOperator,$filterValue 
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Exclude')) {
                    if ([WildcardPattern]::ContainsWildcardCharacters($Exclude)) {
                        $pattern = [WildcardPattern]::new($Exclude);
                        $filterValue = $pattern.ToWql()
                        $filterOperator = 'not-like'
                    }
                    else {
                        $filterValue = $Exclude
                        $filterOperator = 'ne'
                    }

                    $excludeFilter +=  "<condition attribute=""name"" operator=""{1}"" value=""{2}"" />" -f $filterOperator,$filterValue 
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('TeamType')) {
                    $mainConditions += "<condition attribute=""teamtype"" operator=""eq"" value=""$([int]$TeamType)"" />"
                }

                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('BusinessUnit')) {
                    $mainConditions += "<condition attribute=""businessunitid"" operator=""eq"" value=""$($BusinessUnit)"" />"
                }
                if ($PSCmdlet.MyInvocation.BoundParameters.ContainsKey('Administrator')) {
                    $mainConditions += "<condition attribute=""administratorid"" operator=""eq"" value=""$($Administrator)"" />"
                }

                [xml]$fetchxml = 
                @"
                        <fetch>
                            <entity name="team">
                                <all-attributes />
                                <filter type="and">
                                    $($mainConditions)
                                    $($includeFilter)
                                    $($excludeFilter)
                                </filter>
                                <order attribute="name" />
                            </entity>
                        </fetch>
"@
                Get-DataverseRows -FetchXml $fetchxml
            }
            'GetTeamById' {
                Get-DataverseRow -Table 'team' -Id $Id
            }
            Default {}
        }
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
function Get-DataverseRowAccess {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('LogicalName', 'EntityLogicalName', 'Entity')]
        [ArgumentCompleter([AMSoftware.Dataverse.PowerShell.ArgumentCompleters.TableNameArgumentCompleter])]
        [string]$Table,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('Row', 'RowId')]
        [guid]$Id
    )

    process {
        Send-DataverseRequest -Name 'RetrieveSharedPrincipalsAndAccess' -TargetTable $Table -TargetRow $Id
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
function Set-DataverseRowOwner {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('LogicalName', 'EntityLogicalName', 'Entity')]
        [ValidateNotNullOrEmpty()]
        [ArgumentCompleter([AMSoftware.Dataverse.PowerShell.ArgumentCompleters.TableNameArgumentCompleter])]
        [string]$Table,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('Row', 'RowId')]
        [ValidateNotNullOrEmpty()]
        [guid]$Id,

        [Parameter(Mandatory = $true)]
        [ValidateNotNull()]
        [Microsoft.Xrm.Sdk.EntityReference]$Owner
    )

    process {
        Send-DataverseRequest -Name 'Assign' -TargetTable $Table -TargetRow $Id -Parameters @{'Assignee'=$Owner}
    }
}

