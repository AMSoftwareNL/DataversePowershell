# Cleanup Dist and ExternalHelp content to ensure all is new
if (Test-Path '.\dist\') {
    Remove-Item -Path '.\dist\' -Force -Recurse
}

# Copy Build Output
New-Item -Path '.\dist\AMSoftware.Dataverse.PowerShell\' -ItemType Directory
Copy-Item -Path '.\src\AMSoftware.Dataverse.PowerShell\bin\Release\*.*' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\' -Force

# Copy ExternalHelp
New-Item -Path '.\dist\AMSoftware.Dataverse.PowerShell\en-us' -ItemType Directory
Copy-Item -Path '.\externalhelp\*.*' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\en-us\' -Force

# Copy License
Copy-Item -Path '.\LICENSE' -Destination '.\dist\AMSoftware.Dataverse.PowerShell\license.txt' -Force

Compress-Archive -Path '.\dist\AMSoftware.Dataverse.PowerShell\*' -DestinationPath '.\dist\AMSoftware.Dataverse.PowerShell.zip' -Force
