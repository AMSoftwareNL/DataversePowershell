Remove-Item -Path '.\dist\' -Force -Recurse
Remove-Item -Path '.\externalhelp\' -Force -Recurse

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

Publish-Module -Path '.\dist\AMSoftware.Dataverse.PowerShell\' -NuGetApiKey "$env:NUGETAPIKEY"
