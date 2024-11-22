Install-Module -Name 'platyPS' -Scope CurrentUser -Force

Import-Module '.\src\AMSoftware.Dataverse.PowerShell\bin\debug\AMSoftware.Dataverse.PowerShell.psd1'

if (-not (Test-Path -Path '.\docs\')) {
    New-MarkdownHelp -Module 'AMSoftware.Dataverse.PowerShell' -WithModulePage -AlphabeticParamsOrder -UseFullTypeName -Force -ExcludeDontShow -OutputFolder '.\docs\'
} 

Update-MarkdownHelpModule '.\docs\' -RefreshModulePage -AlphabeticParamsOrder -UseFullTypeName -UpdateInputOutput -Force -ExcludeDontShow

Copy-Item -Path '.\docs\AMSoftware.Dataverse.PowerShell.md' -Destination '.\docs\index.md' -Force

# Remove common PowerShell Parameters
Get-ChildItem -Path '.\docs\*.md' -Exclude 'about_*.md' | Foreach-Object {
    $content = Get-Content -Path $_ -Raw
    $content = $content -replace '(### -(ProgressAction)(?:.|\n)*?)(?=##)',''
    $content = $content -replace '(\[-(ProgressAction).*?\])',''

    Set-Content -Path $_ -Value $content
}