---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseChoice

## SYNOPSIS
Add a global Choice (OptionSet)

## SYNTAX

### AddChoiceObject (Default)
```
Add-DataverseChoice -InputObject <OptionSetMetadata>  [<CommonParameters>]
```

### AddNewChoice
```
Add-DataverseChoice -Name <String> -DisplayName <String> [-Description <String>] [-ExternalName <String>]
 -Options <OptionMetadata[]>  [<CommonParameters>]
```

## DESCRIPTION
Add a Global Choice (OptionSet) to Dataverse. Options are provided as an array of OptionMetadata objects.

## EXAMPLES

### Example 1
```
PS C:\> $options = @(
  [dvchoiceoption]::new([dvlabel]'Label 1', 876999001)
  [dvchoiceoption]::new([dvlabel]'Label 2', 876999002)
)

PS C:\> Add-DataverseChoice -Name 'globaloption' -DisplayName 'Global Option' -Options $options
```

NOTE: The prefix for the choice options isn't set automatically based on the solution, but has to be provided and is used as-is.

## PARAMETERS

### -Description
The description for the global Choice

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name for the global Choice

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
The external name for the global Choice

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
A OptionSetMetadata object containing all information for the global Choice.

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata
Parameter Sets: AddChoiceObject
Aliases: Choice, OptionSet

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The logicalname for the Choice

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options
Array of OptionMetadata objects containing the Choices values

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.OptionMetadata[]
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionMetadata[]
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionSetMetadataBase
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseChoice.md)

