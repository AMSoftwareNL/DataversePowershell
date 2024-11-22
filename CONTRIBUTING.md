# Contribute

Contributions to the project are appreciated.

## Project Structure

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

## Tools

* VSCode
* .NET 8
* PlatyPS to generate the docs
* Pester to run tests (when available)

## Build

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

## How to contribute

* Create a GitHib issue to track the contribution and to related the PR to.
* Fork the repo by click "Fork" button on right top corner, which brings entire repo to your GitHub account.
* Add a branch to work on. Please name the branch as issue number so that we can easily relate a pull request to an issue.
* Once you fixed the issue or added new function, please make pull request.

Make sure of the following before making a Pull Request:

* Use {Verb}-Dataverse{Noun} to name function or cmdlet.
* Specify Parameter parameters. At least Mandatory.
* Add docs page with help and examples.
