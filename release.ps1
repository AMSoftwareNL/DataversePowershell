# Cleanup Dist and ExternalHelp content to ensure all is new
if (Test-Path '.\dist\') {
    Remove-Item -Path '.\dist\' -Force -Recurse
}
if (Test-Path '.\externalhelp\') {
    Remove-Item -Path '.\externalhelp\' -Force -Recurse
}

# Create ExternalHelp
New-Item -Path '.\externalhelp\' -ItemType Directory
New-ExternalHelp -Path '.\docs\' -OutputPath '.\externalhelp\' -Force

# Copy Build Output
New-Item -Path '.\dist\AMSoftware.Dataverse.PowerShell\' -ItemType Directory
Copy-Item -Path '.\src\AMSoftware.Dataverse.PowerShell\bin\Release\*.*' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\' -Force

# Copy ExternalHelp
New-Item -Path '.\dist\AMSoftware.Dataverse.PowerShell\en-us' -ItemType Directory
Copy-Item -Path '.\externalhelp\*.*' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\en-us\' -Force

# Copy License
Copy-Item -Path '.\LICENSE' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\license.txt' -Force

# Create ZIP for publish release on GitHub
Compress-Archive -Path '.\dist\AMSoftware.Dataverse.PowerShell\*' -DestinationPath '.\dist\AMSoftware.Dataverse.PowerShell.zip' -Force

# Publish release to PowerShell Gallery
# Publish-Module -Path '.\dist\AMSoftware.Dataverse.PowerShell\' -NuGetApiKey "$env:NUGETAPIKEY"
