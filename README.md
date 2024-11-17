# AMSoftware.Dataverse.PowerShell

[![PowerShell Gallery Version](https://img.shields.io/powershellgallery/v/AMSoftware.Dataverse.PowerShell)](https://www.powershellgallery.com/packages/AMSoftware.Dataverse.PowerShell)
[![PowerShell Gallery Downloads](https://img.shields.io/powershellgallery/dt/AMSoftware.Dataverse.PowerShell)](https://www.powershellgallery.com/packages/AMSoftware.Dataverse.PowerShell)

[![GitHub forks](https://img.shields.io/github/forks/AMSoftwareNL/DataversePowershell?style=social)](https://github.com/AMSoftwareNL/DataversePowershell/network/members) [![GitHub Repo stars](https://img.shields.io/github/stars/AMSoftwareNL/DataversePowershell?style=social)](https://github.com/AMSoftwareNL/DataversePowershell/stargazers)

**PowerShell Module for Power Platform Dataverse**

Complete rebuild of my previous PowerShell Module for CRM ([AMSoftware.Crm.PowerShell](https://github.com/AMSoftwareNL/crmpowershell)). Trying to take into account all my leassons learned.

## The main goals for this project

* Compatible with Microsoft PowerShell (instead of Windows PowerShell) through the [Microsoft Power Platform Dataverse Client](https://github.com/microsoft/PowerPlatform-DataverseServiceClient)
* Improved performance and stability
* Simplified Commands and Parameters (in line with the UI in the Maker Portal)
* Support advanced scenarios, like the Optional Request Parameters [Use Optional Parameters](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters?tabs=sdk)
* Take the Service/Request Limits of Dataverse into consideration. Commands and Parameters for batched operations in addition to pipelined (single request) operations.
* Better logging and verbose information
* Authentication for terminal-only systems
* Less complex PowerShell code and constructs
* More user friendly scripting module for Dataverse compared to [Microsoft.Xrm.Data.PowerShell](https://github.com/seanmcne/Microsoft.Xrm.Data.PowerShell)

## Priorities / Deliverables

 1. :white_check_mark: Authentication and basic session
 2. :white_check_mark: Framework for handling SDK objects in PowerShell

    This core framework is build as a PowerShell Binary module using C#.
 3. :white_check_mark: Content/Row Commands (Get, Set, Add, Remove, ...)
 4. :white_check_mark: Table Metadata Commands
 5. :white_check_mark: Column Metadata Commands
 6. :white_check_mark: OptionSet Metadata Commands
 7. :white_check_mark: Relationship Metadata Commands
 8. :white_check_mark: Extensibility with _simple_ functions

    Through PowerShell Script modules (.psm1) which use the core framework Cmdlets.
 9. :bulb: ... :bulb:

    Post ideas or suggestions as an [issue on GitHub](https://github.com/AMSoftwareNL/DataversePowershell/issues) or create a Pull Request with the change (See the [Contribute](#contribute) section)

## Disclaimer

1. This is a "free time when I want to project", so no guarantees on the speed of bug fixes or feature requests. Contributions are appreciated and this will increase the speed of new features and bug fixes.
2. This program comes with ABSOLUTELY NO WARRANTY.
3. This is free software, and you are welcome to redistribute it under certain conditions;

## Getting Started

### Installation

Install the module through PowerShell Gallery (https://www.powershellgallery.com/packages/AMSoftware.Dataverse.PowerShell/)

``` powershell
Install-Module -Name 'AMSoftware.Dataverse.PowerShell' -Scope CurrentUser
```

Update to the latest release

``` powershell
Update-Module -Name 'AMSoftware.Dataverse.PowerShell' -Force
```

Documentation is available in the [GitHub repo](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/index.md) and with the `Get-Help` cmdlet.

An overview of all available cmdlets and functions
``` powershell
Get-Command -Module 'AMSoftware.Dataverse.PowerShell'
```

### Connect to Dataverse

The first step is to connect to a Dataverse environment using the Cmdlet `Connect-DataverseEnvironment`.

The following are available to connect:

* **Interactive (Default)**

  Interactive login using Username and Password. For terminal-only clients provide the parameter `UseDeviceCode` the login using an external browser and a devicecode.
  > **NOTE:**
  >
  > Login with DeviceCode uses a custom authenticator. As a result the AuthenticationToken in the Dataverse ServiceClient is **NOT** populated.

* **Connectionstring**

  Connect with a Dataverse ServiceClient [Connectionstring for XRM Tooling](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/xrm-tooling/use-connection-strings-xrm-tooling-connect). Can be used from Azure DevOps Pipelines with the Power Platform Build Tasks.

* **ClientId and ClientSecret**

  Connect with a Service Principal and Dataverse Application User providing ClientId and ClientSecret. Can be used from Azure DevOps Pipelines with the Power Platform Build Tasks.

* **ServiceClient**

  Provide your own authenticated [Microsoft.PowerPlatform.Dataverse.Client.ServiceClient](https://learn.microsoft.com/en-us/dotnet/api/microsoft.powerplatform.dataverse.client.serviceclient?view=dataverse-sdk-latest) instance.

> **Azure Entra App Registration**
>
> PowerShell is seen as an external application connecting to Dataverse, and therefor requires its own Azure Entra App Registration. Use the ClientId (AppId) of this App Registration with `Connect-DataverseEnvironment`. Ask your tenant administrator for a new App Registration or use an existing App Registration. For example the App Registration you use in combnation with Azure DevOps and Dataverse.
>
> To make life a bit easier Microsoft has a global App Registration which is available in every tenant. This is meant for development and prototyping pusposes and should not be used in production scenarios. `Connect-DataverseEnvironment` uses this App Registration as a fallback if no specific ClientId is provided.
>
>> __From Microsoft[^1]__
>>
>> _For development and prototyping purposes we have provided the following AppId or ClientId and Redirect URI for use in OAuth Flows.
For production use, you should create an AppId or ClientId that is specific to your tenant in the Azure Management portal.
Sample AppId or ClientId = 51f81489-12ee-4a9e-aaae-a2591f45987d
Sample RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97_

[^1]: https://learn.microsoft.com/en-us/power-apps/developer/data-platform/xrm-tooling/use-connection-strings-xrm-tooling-connect#connection-string-parameters

### Optional Request Parameters

The core request cmdlets support the use of [Optional Parameters](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters).

For details see [about_DataverseRequestParameters.md](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/about_DataverseRequestParameters.md) or `Get-Help about_DataverseRequestParameters`

### Type Accelerators

This module registers some Type Accelerators for Dataverse SDK objects.

See [about_DataverseTypeAccelerators](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/about_DataverseTypeAccelerators.md) or `Get-Help about_DataverseTypeAccelerators`.

### Language and Solutions

To set the default solution in the active session

``` powershell
Use-DataverseSolution -Name 'solutionuniquename'
```

To set the language in the current session
``` powershell
# For English
Use-DataverseLanguage -Language 1033 

# For Dutch 
Use-DataverseLanguage -Language 1043 
```

### Working with Metadata

Metadata commands are available for

* Tables
* Columns
* Relationships
* Keys
* Choices

### Using Table and Rows

Cmdlets are available for rows in Dataverse with

* Retrieve / RetrieveMultiple
* Create / CreateMultiple
* Update / UpdateMultiple
* Delete
* Associate / Disassociate

> **NOTE:**
> 
> When using Create with Alternate Keys the possibility for Upsert is available.

All these cmdlets use the [Entity Class](https://learn.microsoft.com/en-us/dotnet/api/microsoft.xrm.sdk.entity?view=dataverse-sdk-latest) from the SDK.

To make the use of this more PowerShell friendly PropertyAdapters arre implemented. This provides the following features:

Columns (attributes) are shown as part of the Row (Entity) without having to expand the Attributes collection.

``` powershell
Get-DataverseRows -Table 'account' | Select-Object -First 1 | fl

accountclassificationcode                 : 1
accountclassificationcode_FormattedValue  : Default Value
accountid                                 : 15abae1d-6398-ef11-a72d-000d3ade4f9a
accountratingcode                         : 1
accountratingcode_FormattedValue          : Default Value
businesstypecode                          : 1
businesstypecode_FormattedValue           : Default Value
createdby                                 : 52d80856-147e-ef11-ac21-0022489b6da8
createdby_Entity                          : systemuser
createdby_FormattedValue                  : AMSoftwareNL
createdon                                 : 1-11-2024 15:08:22
createdon_FormattedValue                  : 11/1/2024 4:08 PM
creditonhold                              : False
creditonhold_FormattedValue               : No
customersizecode                          : 1
customersizecode_FormattedValue           : Default Value
donotbulkemail                            : False
donotbulkemail_FormattedValue             : Allow
donotbulkpostalmail                       : False
donotbulkpostalmail_FormattedValue        : No
donotemail                                : False
donotemail_FormattedValue                 : Allow
donotfax                                  : False
donotfax_FormattedValue                   : Allow
donotphone                                : False
donotphone_FormattedValue                 : Allow
donotpostalmail                           : False
donotpostalmail_FormattedValue            : Allow
donotsendmm                               : False
donotsendmm_FormattedValue                : Send
followemail                               : True
followemail_FormattedValue                : Allow
marketingonly                             : False
marketingonly_FormattedValue              : No
merged                                    : False
merged_FormattedValue                     : No
modifiedby                                : 52d80856-147e-ef11-ac21-0022489b6da8
modifiedby_Entity                         : systemuser
modifiedby_FormattedValue                 : AMSoftwareNL
modifiedon                                : 1-11-2024 15:08:22
modifiedon_FormattedValue                 : 11/1/2024 4:08 PM
ownerid                                   : 52d80856-147e-ef11-ac21-0022489b6da8
ownerid_Entity                            : systemuser
ownerid_FormattedValue                    : AMSoftwareNL
owningbusinessunit                        : 5fd10856-147e-ef11-ac21-0022489b6da8
owningbusinessunit_Entity                 : businessunit
owningbusinessunit_FormattedValue         : amsoftwarenl
owninguser                                : 52d80856-147e-ef11-ac21-0022489b6da8
owninguser_Entity                         : systemuser
participatesinworkflow                    : False
participatesinworkflow_FormattedValue     : No
preferredcontactmethodcode                : 1
preferredcontactmethodcode_FormattedValue : Any
shippingmethodcode                        : 1
shippingmethodcode_FormattedValue         : Default Value
statecode                                 : 0
statecode_FormattedValue                  : Active
statuscode                                : 1
statuscode_FormattedValue                 : Active
territorycode                             : 1
territorycode_FormattedValue              : Default Value
territorycode                             : 1
territorycode_FormattedValue              : Default Value
```

Formatted values are directly available as rad-only properties using the convention `logicalname_FormattedValue`

``` powershell
Get-DataverseRows -Table 'account' | Select-Object -First 1 | fl

customersizecode                          : 1
customersizecode_FormattedValue           : Default Value
```

Lookups (EntityRefence) are automatically expanded with read-only properties for Tablename and Displayname using the convention `logicalname_Entity` and `logicalname_FormattedValue`. The Lookup column itself shows the ID.

``` powershell
Get-DataverseRows -Table 'account' | Select-Object -First 1 | fl

ownerid                                   : 52d80856-147e-ef11-ac21-0022489b6da8
ownerid_Entity                            : systemuser
ownerid_FormattedValue                    : AMSoftwareNL
```

Every row has the following properties:

* Id

  The unique row Id (primaryidattribute)
* LogicalName

  The logicalname of the Table
* Name

  The row display name (primarynameattribute)
* State

  The row status (statecode)
* Status

  The row status reason (statuscode)

``` powershell
Get-DataverseRows -Table 'account' | Select-Object -First 1 | fl

Id                                        : 15abae1d-6398-ef11-a72d-000d3ade4f9a
LogicalName                               : account
Name                                      : AMSoftware
State                                     : Active
Status                                    : Active
```

### Argument Completers

To support auto complete and intellisense in PowerShell the module contains argument completers for:

* Tables (entity logicalname)
* Columns (attribute logicalname)
* Global Choices (optionset logicalname)

### Actions and Functions

The execute any action, function, or Custom API in Dataverse use `Send-DataverseRequest`.

``` powershell
# Unbound
Send-DataverseRequest -Name 'WhoAmI'

# Bound
$response = Send-DataverseRequest -Name 'SendEmail' -TargetTable 'email' - TargetRow '196ef64d-6b66-4595-9552-55912af14ed9' -Parameters @{
    'IssueSend'=$true;
    'TrackingToken'='token'
}

```

### Batch and Multiple

See [about_DataverseBatch](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/about_DataverseBatch.md) or `Get-Help about_DataverseBatch`.

## Contribute

Contributions to the project are appreciated.

### Project Structure

* \docs
  The docs for the module
* src/AMSoftware.Dataverse.PowerShell
  A single project for the entire module.
  * Code files for the Binary Module part
  * .psd1 for the module description and exported cmdlets and functions.
  * Types.ps1xml for the Type Descriptors.
  * Formats.ps1xml for the Table and View definitions.
  * AMSoftware.Dataverse.PowerShell.psm1 as the startup script for the module.
  * .psm1 for the additional script modules.

### Tools

* VSCode
* .NET 8
* PlatyPS to generate the docs
* Pester to run tests (when available)

### Build

To build the module for test and documentation.

``` powershell
# Build Debug. This builds the module and copies neccesary files to output
dotnet build ./src/ --no-incremental

# Build docs for Debug module
./builddocs.ps1

# Update Docs with help and exmples

# Create Release which includes module and help content
dotnet build ./src/ --no-incremental --configuration 'Release'
# Release is placed in ./dist/
./release.ps1
```

### How to contribute

* Create a GitHib issue to track the contribution and to related the PR to.
* Fork the repo by click "Fork" button on right top corner, which brings entire repo to your GitHub account.
* Add a branch to work on. Please name the branch as issue number so that we can easily relate a pull request to an issue.
* Once you fixed the issue or added new function, please make pull request.

Make sure of the following before making a Pull Request:

* Use {Verb}-Dataverse{Noun} to name function or cmdlet.
* Specify Parameter parameters. At least Mandatory.
* Add docs page with help and examples.
