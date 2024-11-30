---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseColumn

## SYNOPSIS
Add a Column to a Dataverse Table

## SYNTAX

### AddColumnByInputObject
```
Add-DataverseColumn -Table <String> -InputObject <AttributeMetadata> 
 [<CommonParameters>]
```

### AddColumnByParameters
```
Add-DataverseColumn -Table <String> -Name <String> -Type <ColumnType> -DisplayName <String>
 [-Description <String>] [-Required <ColumnRequiredLevel>] [-ExternalName <String>] [-Searchable] [-Auditing]
 [-ColumnSecurity] [-Source <ColumnSourceType>]  [<CommonParameters>]
```

## DESCRIPTION
Add a Column to a Dataverse Table. Based on the Type additional dynamic properties become available. Do not use for columns with a relationship like Customer or Lookups. In this case use Add-DataverseRelationship.

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Auditing
Is auditing enabled for the new Column

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddColumnByParameters
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnSecurity
Available for Column Security Profile

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddColumnByParameters
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
Parameter Sets: AddColumnByParameters
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The display name of the Column

```yaml
Type: System.String
Parameter Sets: AddColumnByParameters
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
The external name of the Column

```yaml
Type: System.String
Parameter Sets: AddColumnByParameters
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
An AttributeMetadata object with the information for the Column

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
Parameter Sets: AddColumnByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
The logicalname of the Column

```yaml
Type: System.String
Parameter Sets: AddColumnByParameters
Aliases: AttributeLogicalName, ColumnName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Required
The requirement level of the Column

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnRequiredLevel
Parameter Sets: AddColumnByParameters
Aliases:
Accepted values: Optional, Required, Recommended

Required: False
Position: Named
Default value: Optional
Accept pipeline input: False
Accept wildcard characters: False
```

### -Searchable
Is the column Searchable

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddColumnByParameters
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
Set for Calculated, Rollup, Formula columns. When not provided a standard column is created.

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnSourceType
Parameter Sets: AddColumnByParameters
Aliases:
Accepted values: Calculated, Rollup, Formula

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
The logicalname of the Table to add the column to

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

### -Type
The type of Column to add. Depending on the type additional dynamic parameters become available.

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnType
Parameter Sets: AddColumnByParameters
Aliases:
Accepted values: Boolean, Customer, DateTime, Decimal, Double, Integer, Lookup, Memo, Money, Owner, PartyList, Picklist, State, Status, String, Uniqueidentifier, CalendarRules, Virtual, BigInt, ManagedProperty, EntityName, Image, MultiSelectPicklist, File

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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseColumn.md)




