# Module manifest for module 'AMSoftware.Dataverse.PowerShell'

@{
    # Script module or binary module file associated with this manifest.
    # NOTE: Using NestedModules instead of RootModule. This creates a "manifest module" containing Binary and PSM1 modules.
    RootModule = ''

    # Version number of this module.
    ModuleVersion = '0.6.0'

    # Supported PSEditions
    CompatiblePSEditions = 'Core'

    # ID used to uniquely identify this module
    GUID = '97489ae7-5cf5-4848-85f6-0d961f71f217'

    # Author of this module
    Author = 'AMSoftwareNL'

    # Company or vendor of this module
    CompanyName = 'AMSoftware'

    # Copyright statement for this module
    Copyright = 'Copyright(C) 2024 AMSoftwareNL. All rights reserved.'

    # Description of the functionality provided by this module
    Description = 'PowerShell Module for Power Platform Dataverse'

    # Minimum version of the PowerShell engine required by this module
    PowerShellVersion = '5.1'

    # Assemblies that must be loaded prior to importing this module
     RequiredAssemblies = @(
        'AMSoftware.Dataverse.PowerShell.dll',
        'Microsoft.PowerPlatform.Dataverse.Client.dll',
        'Microsoft.Xrm.Sdk.dll',
        'Microsoft.Crm.Sdk.Proxy.dll')

    # Script files (.ps1) that are run in the caller's environment prior to importing this module.
    # ScriptsToProcess = @()

    # Type files (.ps1xml) to be loaded when importing this module
    TypesToProcess = 'AMSoftware.Dataverse.PowerShell.Types.ps1xml'

    # Format files (.ps1xml) to be loaded when importing this module
    FormatsToProcess = 'AMSoftware.Dataverse.PowerShell.Formats.ps1xml'

    # Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
    NestedModules = @(
        'AMSoftware.Dataverse.PowerShell.dll',
        'AMSoftware.Dataverse.PowerShell.psm1',
        'AMSoftware.Dataverse.PowerShell.AccessManagement.psm1',
        'AMSoftware.Dataverse.PowerShell.Administration.psm1',
        'AMSoftware.Dataverse.PowerShell.Development.psm1'
    )

    # Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
    FunctionsToExport = @(
        'Get-DataverseUser',
        'Get-DataverseRole',
        'Get-DataverseTeam',
        'Get-DataverseRowAccess',
        'Set-DataverseRowOwner',
        'Add-DataverseLanguage',
        'Get-DataverseLanguage',
        'Remove-DataverseLanguage',
        'Export-DataverseTranslation',
        'Import-DataverseTranslation',
        'Get-DataverseEnvironmentVariableValue',
        'Get-DataverseSPDocumentLocation',
        'Invoke-DataverseWorkflow',
        'Export-DataverseWebResource',
        'Export-DataversePluginAssembly',
        'Import-DataverseWebResource',
        'Publish-DataverseComponent',
        'Register-DataverseDataProvider',
        'Register-DataversePlugin',
        'Register-DataversePluginStep',
        'Register-DataversePluginStepImage',
        'Register-DataverseServiceEndpoint',
        'Register-DataverseWebhook',
        'Test-DataverseSolution',
        'Test-DataverseSolutionComponent',
        'Unregister-DataverseDataProvider',
        'Unregister-DataversePlugin',
        'Unregister-DataversePluginAssembly',
        'Unregister-DataversePluginStep',
        'Unregister-DataversePluginStepImage',
        'Unregister-DataverseServiceEndpoint'
    )

    # Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
    CmdletsToExport = @(
        'Connect-DataverseEnvironment',
        'Use-DataverseLanguage',
        'Use-DataverseSolution',
        'Add-DataverseRow',
        'Add-DataverseRows',
        'Get-DataverseRow',
        'Get-DataverseRows',
        'Remove-DataverseRow',
        'Remove-DataverseRows',
        'Set-DataverseRow',
        'Set-DataverseRows',
        'Add-DataverseRelatedRow',
        'Remove-DataverseRelatedRow',
        'Send-DataverseRequest',
        'Add-DataverseTable',
        'Get-DataverseTable',
        'Remove-DataverseTable',
        'Set-DataverseTable',
        'Add-DataverseTableKey',
        'Get-DataverseTableKey',
        'Remove-DataverseTableKey',
        'Add-DataverseColumn',
        'Get-DataverseColumn',
        'Remove-DataverseColumn',
        'Set-DataverseColumn',
        'Add-DataverseRelationship',
        'Get-DataverseRelationship',
        'Remove-DataverseRelationship',
        'Set-DataverseRelationship',
        'Add-DataverseChoice',
        'Get-DataverseChoice',
        'Remove-DataverseChoice',
        'Set-DataverseChoice',
        'Set-DataverseChoiceOption',
        'Remove-DataverseChoiceOption',
        'Request-DataverseBatch',
        'Submit-DataverseBatch'
    )

    # Variables to export from this module
    VariablesToExport = @('DataverseSession','DataverseClient')

    # Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
    AliasesToExport = @()

    # List of all modules packaged with this module
    ModuleList = @()

    # List of all files packaged with this module
    FileList = @(
        'AMSoftware.Dataverse.PowerShell.dll',
        'AMSoftware.Dataverse.PowerShell.psd1',
        'AMSoftware.Dataverse.PowerShell.psm1',
        'AMSoftware.Dataverse.PowerShell.AccessManagement.psm1',
        'AMSoftware.Dataverse.PowerShell.Administration.psm1',
        'AMSoftware.Dataverse.PowerShell.Development.psm1',
        'AMSoftware.Dataverse.PowerShell.Types.ps1xml',
        'AMSoftware.Dataverse.PowerShell.Formats.ps1xml',
        'en-us\AMSoftware.Dataverse.PowerShell.dll-help.xml',
        'en-us\AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml',
        'en-us\AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml',
        'en-us\AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml',
        'en-us\about_DataverseBatch.help.txt',
        'en-us\about_DataverseRequestParameters.help.txt',
        'en-us\about_DataverseTypeAccelerators.help.txt',
        'en-us\about_DataverseVariables.help.txt',
        'license.txt',
        'Microsoft.PowerPlatform.Dataverse.Client.dll',
        'Microsoft.Xrm.Sdk.dll',
        'Microsoft.Crm.Sdk.Proxy.dll')

    # Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
    PrivateData = @{
        PSData = @{
            # Tags applied to this module. These help with module discovery in online galleries.
            Tags = @('dataverse','powerplatform','xrm','crm')

            # A URL to the license for this module.
            LicenseUri = 'https://github.com/AMSoftwareNL/DataversePowershell/blob/main/LICENSE'

            # A URL to the main website for this project.
            ProjectUri = 'https://github.com/AMSoftwareNL/DataversePowershell'

            # ReleaseNotes of this module
            ReleaseNotes = 'https://github.com/AMSoftwareNL/DataversePowershell/releases/tag/v0.6.0'

            # Flag to indicate whether the module requires explicit user acceptance for install/update/save
            RequireLicenseAcceptance = $true
        } # End of PSData hashtable
    } # End of PrivateData hashtable

    # HelpInfo URI of this module
    HelpInfoURI = 'https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/AMSoftware.Dataverse.PowerShell.md'
}

