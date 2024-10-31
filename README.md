# AMSoftware.Dataverse.PowerShell

PowerShell Module for Power Platform Dataverse

> **NOTE**
> This project is in development. Ideas and suggestions are always welcome.
> The main branch is basically my dev branche where I play around, and try to get things to work.
> Changes are day to day, and it might not build or work every day.
> Nice to haves like documentation and error handling are not on the top of the list.

Complete rebuild of my previous PowerShell Module for CRM ([AMSoftware.Crm.PowerShell](https://github.com/AMSoftwareNL/crmpowershell)). Trying to take into account all my leassons learned.

## The main goals for this version

* Compatible with Microsoft PowerShell (instead of Windows PowerShell) through the [Microsoft Power Platform Dataverse Client](https://github.com/microsoft/PowerPlatform-DataverseServiceClient)
* Improved performance and stability
* Simplified Commands and Parameters (in line with the UI in the Maker Portal)
* Support advanced scenarios, like the Optional Request Parameters [Use Optional Parameters](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters?tabs=sdk)
* Take the Service/Request Limits of Dataverse into consideration. Commands and Parameters for batched operations in addition to pipelined (single request) operations.
* Better logging and verbose information
* Authentication for terminal-only systems
* Less complex PowerShell code and constructs

## Priorities / Deliverables

 1. :white_check_mark: Authentication and basic session
 2. :white_check_mark: Framework for handling SDK objects in PowerShell
 3. :white_check_mark: Content/Row Commands (Get, Set, Add, Remove, ...)
 4. :white_check_mark: Table Metadata Commands
 5. :white_check_mark: Column Metadata Commands
 6. :white_check_mark: OptionSet Metadata Commands
 7. :bookmark: Relationship Metadata Commands
 8. :bulb: ... :bulb:

This is a "free time when I want to project", so it will take some time. 
The goal is to have a first stable working version at years end. 
When to framework for PowerShell (Types, Adapters, Converters, etc.) is done, it should be fairly quick and easy to add the different sets of Commands.
