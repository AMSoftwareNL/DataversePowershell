---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseChoice

## SYNOPSIS
Update a global choice

## SYNTAX

### SetChoiceObject (Default)
```
Set-DataverseChoice -InputObject <OptionSetMetadata> [-MergeLabels] 
 [<CommonParameters>]
```

### SetChoice
```
Set-DataverseChoice -Name <String> [-DisplayName <String>] [-Description <String>] [-ExternalName <String>]
 [-MergeLabels]  [<CommonParameters>]
```

## DESCRIPTION
Update a global choice

## PARAMETERS

### -Description
New description for the choice

```yaml
Type: System.String
Parameter Sets: SetChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
New displayname for the choice

```yaml
Type: System.String
Parameter Sets: SetChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
New external name for the choice

```yaml
Type: System.String
Parameter Sets: SetChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Metadata description the updated choice

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata
Parameter Sets: SetChoiceObject
Aliases: Choice, OptionSet

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MergeLabels
Keep other language labels for DisplayName and Description

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Logicalname of the choice to update

```yaml
Type: System.String
Parameter Sets: SetChoice
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

### None
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionSetMetadataBase
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseChoice.md)
