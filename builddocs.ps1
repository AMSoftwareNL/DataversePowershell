Install-Module -Name 'platyPS' -Scope CurrentUser -Force

Import-Module '.\src\AMSoftware.Dataverse.PowerShell\bin\debug\AMSoftware.Dataverse.PowerShell.psd1'

if (-not (Test-Path -Path '.\docs\')) {
    New-MarkdownHelp -Module 'AMSoftware.Dataverse.PowerShell' -WithModulePage -OutputFolder '.\docs\'
} 

Update-MarkdownHelpModule '.\docs\' -RefreshModulePage -AlphabeticParamsOrder -UseFullTypeName -UpdateInputOutput -Force

Copy-Item -Path '.\docs\AMSoftware.Dataverse.PowerShell.md' -Destination '.\docs\index.md' -Force

New-ExternalHelp -Path '.\docs\' -OutputPath '.\src\AMSoftware.Dataverse.PowerShell\bin\debug\en-US\' -Force