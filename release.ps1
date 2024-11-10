Remove-Item -Path '.\dist\' -Force -Recurse
Remove-Item -Path '.\externalhelp\' -Force -Recurse

# Create ExternalHelp
New-Item -Path '.\externalhelp\' -ItemType Directory
New-ExternalHelp -Path '.\docs\' -OutputPath '.\externalhelp\' -Force

# Copy Build Output
New-Item -Path '.\dist\' -ItemType Directory
Copy-Item -Path '.\src\AMSoftware.Dataverse.PowerShell\bin\Release\*.*' -Destination '.\dist\' -Force

# Copy ExternalHelp
New-Item -Path '.\dist\en-us' -ItemType Directory
Copy-Item -Path '.\externalhelp\*.*' -Destination '.\dist\en-us\' -Force

Publish-Module -Path '.\dist\' -NuGetApiKey "$env:NUGETAPIKEY"
