Install-Module -Name 'platyPS' -Scope CurrentUser -Force

Import-Module '.\src\AMSoftware.Dataverse.PowerShell\bin\debug\AMSoftware.Dataverse.PowerShell.psd1'

if (-not (Test-Path -Path '.\docs\')) {
    New-MarkdownHelp -Module 'AMSoftware.Dataverse.PowerShell' -WithModulePage -AlphabeticParamsOrder -UseFullTypeName -Force -OutputFolder '.\docs\'
} 

Update-MarkdownHelpModule '.\docs\' -RefreshModulePage -AlphabeticParamsOrder -UseFullTypeName -UpdateInputOutput -Force

Copy-Item -Path '.\docs\AMSoftware.Dataverse.PowerShell.md' -Destination '.\docs\index.md' -Force

$requestParametersText = @'
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.
'@

# Remove shared Request Parameters
# Remove common PowerShell Parameters
# Add request parameters section if needed
Get-ChildItem -Path '.\docs\*.md' -Exclude 'about_*.md' | Foreach-Object {
    $content = Get-Content -Path $_ -Raw
    $content = $content -replace '(### -(Solution|SharedTag|Partition|FailOnDuplicateDetection|BypassSynchronousLogic|BypassPowerAutomateFlows|ProgressAction|WhatIf|Confirm)(?:.|\n)*?)(?=##)',''
    $content = $content -replace '(\[-(WhatIf|Confirm|ProgressAction).*?\])',''

    if ($content -match '(\[-(Solution|SharedTag|Partition|FailOnDuplicateDetection|BypassSynchronousLogic|BypassPowerAutomateFlows).*?\])') {
        $content = $content -replace '(\[-(Solution|SharedTag|Partition|FailOnDuplicateDetection|BypassSynchronousLogic|BypassPowerAutomateFlows).*?\])',''
        $content = $content.Replace('[<CommonParameters>]','[<RequestParameters>] [<CommonParameters>]')
        $content = $content.Replace('### CommonParameters', "### RequestParameters`n$($requestParametersText)`n`n### CommonParameters")
    }
    Set-Content -Path $_ -Value $content
}