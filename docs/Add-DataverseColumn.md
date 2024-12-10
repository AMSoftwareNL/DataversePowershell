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
PS C:\> Add-DataverseColumn -Table 'account' -Name 'ams_testdefaults' -Type String -DisplayName 'Test Defaults'
```

### Example 2

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testformula -Type String -DisplayName 'Test Formula'`
 -Source Formula
```

### Example 3

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_teststring -Type String -DisplayName 'Test String' -Description 'Test String Description' -Required Recommended -ExternalName 'teststring' -Searchable -Auditing -ColumnSecurity -Format Url -ImeMode Auto -MaxLength 155
```

### Example 4

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testboolean -Type Boolean -DisplayName 'Test Boolean' -DefaultValue $true
```

### Example 5

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testdatetime -Type DateTime -DisplayName 'Test DateTime' -Format DateOnly -ImeMode Auto -Behavior DateOnly
```

### Example 6

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testdecimal -Type Decimal -DisplayName 'Test Decimal' -MinValue 1.0 -MaxValue 100.0 -Precision 2 -ImeMode Auto

```

### Example 7

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testdouble -Type Double -DisplayName 'Test Double' -MinValue 1.0 -MaxValue 100.0 -Precision 2 -ImeMode Auto
```

### Example 8

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testinteger -Type Integer -DisplayName 'Test Integer' -MinValue 1 -MaxValue 100 -Format Duration
```

### Example 9

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testbigint -Type BigInt -DisplayName 'Test BigInt'
```

### Example 10

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testmoney -Type Money -DisplayName 'Test Money' -MinValue 1.0 -MaxValue 100.0 -Precision 2 -PrecisionSource Currency -ImeMode Auto
```

### Example 11

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testmemo -Type Memo -DisplayName 'Test Memo' -Format Text -MaxLength 4000 -ImeMode Auto
```

### Example 12

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testfile -Type File -DisplayName 'Test File' -MaxSizeInKB 10240
```

### Example 13

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testimage -Type Image -DisplayName 'Test Image' -MaxSizeInKB 10240 -IsPrimaryImage -CanStoreFullImage
```

### Example 14

```
PS C:\> $options = @(
    [dvchoiceoption]::new([dvlabel]'Monday', 1)
    [dvchoiceoption]::new([dvlabel]'Tuesday', 2)
    [dvchoiceoption]::new([dvlabel]'Wednesday', 3)
    [dvchoiceoption]::new([dvlabel]'Thursday', 4)
    [dvchoiceoption]::new([dvlabel]'Friday', 5)
   )

PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testlocalpicklist -Type Picklist -DisplayName 'Test Picklist' -DefaultValue 5 -Options $options
```

### Example 15

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testsyncedpicklist -Type Picklist -DisplayName 'Test Picklist' -DefaultValue 5 -GlobalOptionsetName 'yearlymonth_options'
```

### Example 16

```
PS C:\> $options = @(
    [dvchoiceoption]::new([dvlabel]'Monday', 1)
    [dvchoiceoption]::new([dvlabel]'Tuesday', 2)
    [dvchoiceoption]::new([dvlabel]'Wednesday', 3)
    [dvchoiceoption]::new([dvlabel]'Thursday', 4)
    [dvchoiceoption]::new([dvlabel]'Friday', 5)
   )

PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testlocalmultipicklist -Type MultiSelectPicklist -DisplayName 'Test Multi Picklist' -Options $options
```

### Example 17

```
PS C:\> Add-DataverseColumn -Table 'account' -Name "ams_testsyncedmultipicklist -Type MultiSelectPicklist -DisplayName 'Test Multi Picklist' -GlobalOptionsetName 'yearlymonth_options'
```

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
