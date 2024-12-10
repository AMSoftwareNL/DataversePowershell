---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Connect-DataverseEnvironment

## SYNOPSIS
Connect to a specific Dataverse Environment.

## SYNTAX

### Interactive (Default)
```
Connect-DataverseEnvironment -EnvironmentUrl <Uri> [-ClientId <String>] [-UseDeviceCode]
  [<CommonParameters>]
```

### ServiceClient
```
Connect-DataverseEnvironment -ServiceClient <ServiceClient> 
 [<CommonParameters>]
```

### Connectionstring
```
Connect-DataverseEnvironment -Connectionstring <String> 
 [<CommonParameters>]
```

### ClientSecret
```
Connect-DataverseEnvironment -EnvironmentUrl <Uri> -ClientId <String> -ClientSecret <SecureString>
  [<CommonParameters>]
```

## DESCRIPTION
Connect to a specific Dataverse Environment. This connects the PowerShell session with the Dataverse Environment. Non of cmdlets or functions in this module work without a connected session.

## EXAMPLES

## PARAMETERS

### -ClientId
The ClientId / AppId of a registered application in Azure Entra ID as context for this module. If non provided the module will uses the Microsoft provided Client ID for development and prototyping purposes.

```yaml
Type: System.String
Parameter Sets: Interactive
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: System.String
Parameter Sets: ClientSecret
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ClientSecret
The client secret to use for authentication.

```yaml
Type: System.Security.SecureString
Parameter Sets: ClientSecret
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Connectionstring
A connectionstring to Dataverse including authentication information in the format of Connectionstring for XRM Tooling.

```yaml
Type: System.String
Parameter Sets: Connectionstring
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EnvironmentUrl
The URL of the Dataverse Environment to connect to.

```yaml
Type: System.Uri
Parameter Sets: Interactive, ClientSecret
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServiceClient
An external authenticated Dataverse ServiceClient instance.

```yaml
Type: Microsoft.PowerPlatform.Dataverse.Client.ServiceClient
Parameter Sets: ServiceClient
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -UseDeviceCode
Include switch to use DeviceCode for terminal-only systems. Login with DeviceCode uses a custom authenticator. As a result the AuthenticationToken in the Dataverse ServiceClient is NOT populated.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: Interactive
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### Microsoft.PowerPlatform.Dataverse.Client.ServiceClient
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Connect-DataverseEnvironment.md)
[Connectionstring for XRM Tooling](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/xrm-tooling/use-connection-strings-xrm-tooling-connect)
[Microsoft.PowerPlatform.Dataverse.Client.ServiceClient](https://learn.microsoft.com/en-us/dotnet/api/microsoft.powerplatform.dataverse.client.serviceclient?view=dataverse-sdk-latest)




