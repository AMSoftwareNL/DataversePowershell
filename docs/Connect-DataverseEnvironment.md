---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Connect-DataverseEnvironment

## SYNOPSIS
{{ Fill in the Synopsis }}

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
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -ClientId
{{ Fill ClientId Description }}

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
{{ Fill ClientSecret Description }}

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
{{ Fill Connectionstring Description }}

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
{{ Fill EnvironmentUrl Description }}

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
{{ Fill ServiceClient Description }}

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
{{ Fill UseDeviceCode Description }}

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

