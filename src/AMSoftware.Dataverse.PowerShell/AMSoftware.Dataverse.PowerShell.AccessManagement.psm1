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

    begin {}
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
    end {}
}