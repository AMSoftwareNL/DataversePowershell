Install-Module -Name 'platyPS' -Scope CurrentUser -Force

# Clean External Help
if (Test-Path -LiteralPath '.\externalhelp\' -PathType Container) {
    Remove-Item -LiteralPath '.\externalhelp\' -Recurse -Force
}

# Create ExternalHelp
New-Item -Path '.\externalhelp\' -ItemType Directory
New-ExternalHelp -Path '.\docs\' -OutputPath '.\externalhelp\' -Force