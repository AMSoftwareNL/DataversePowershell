---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseColumn

## SYNOPSIS
Update a column

## SYNTAX

### SetColumnByInputObject
```
Set-DataverseColumn -Table <String> -InputObject <AttributeMetadata> [-MergeLabels]
  [<CommonParameters>]
```

### SetColumnByParameters
```
Set-DataverseColumn -Table <String> -Name <String> [-DisplayName <String>] [-Description <String>]
 [-ExternalName <String>] [-Searchable] [-Auditing] [-MergeLabels] 
 [<CommonParameters>]
```

## DESCRIPTION
Update a column.
Additional dynamic parameters are available depending on the type of column. These come available when Table and Name are provided.

## PARAMETERS

### -Auditing
Is auditing enabled for the Column

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SetColumnByParameters
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description of the Column

```yaml
Type: System.String
Parameter Sets: SetColumnByParameters
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The displayname of the Column

```yaml
Type: System.String
Parameter Sets: SetColumnByParameters
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
The external name of the Column

```yaml
Type: System.String
Parameter Sets: SetColumnByParameters
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Attribute Metadata object describing the updated column

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
Parameter Sets: SetColumnByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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
Logicalname of the column to update

```yaml
Type: System.String
Parameter Sets: SetColumnByParameters
Aliases: AttributeLogicalName, ColumnName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Searchable
Is the column Searchable

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SetColumnByParameters
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
The logicalname of the Table containing the column to update

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: EntityLogicalName, LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseColumn.md)
