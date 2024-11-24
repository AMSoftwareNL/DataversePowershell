# Variables
## about_DataverseVariables

# SHORT DESCRIPTION
Describes the available global variables.

# LONG DESCRIPTION
The module adds two global variables, that can be used in scripts:

- DataverseSession
- DataverseClient

## DataverseSession
When you connect to Dataverse with `Connect-DataverseEnvironment` a session object is initialized (`AMSoftware.Dataverse.PowerShell.Session`). This session holds the following read-only information:

- UserId
- UserLanguageId
- UserLocaleId
- BusinessUnitId
- OrganizationLanguageId
- OrganizationLocaleId
- SystemUserId
- LanguageId
- LocaleId
- ActiveSolution

## DataverseClient
Stores the active `Microsoft.PowerPlatform.Dataverse.Client.ServiceClient` used with `Connect-DataverseEnvironment`.

# SEE ALSO
[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/about_DataverseVariables.md)
