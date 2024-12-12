Import-Module '.\src\AMSoftware.Dataverse.PowerShell\bin\debug\AMSoftware.Dataverse.PowerShell.psd1' -Force

Update-MarkdownHelpModule '.\docs\' -RefreshModulePage -AlphabeticParamsOrder -UseFullTypeName -UpdateInputOutput -Force -ExcludeDontShow

Remove-Module 'AMSoftware.Dataverse.PowerShell'

Copy-Item -Path '.\docs\AMSoftware.Dataverse.PowerShell.md' -Destination '.\docs\index.md' -Force
# Remove common PowerShell Parameters
Get-ChildItem -Path '.\docs\*.md' -Exclude 'about_*.md' | Foreach-Object {
    $content = Get-Content -Path $_ -Raw
    $content = $content -replace '(### -(ProgressAction)(?:.|\n)*?)(?=##)',''
    $content = $content -replace '(\[-(ProgressAction).*?\])',''

    Set-Content -Path $_ -Value $content
}