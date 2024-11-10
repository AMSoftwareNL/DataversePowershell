---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseColumn

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### AddColumnByInputObject
```
Add-DataverseColumn -Table <String> -InputObject <AttributeMetadata>  
    
  [<RequestParameters>] [<CommonParameters>]
```

### AddColumnByParameters
```
Add-DataverseColumn -Table <String> -Name <String> -Type <ColumnType> -DisplayName <String>
 [-Description <String>] [-Required <ColumnRequiredLevel>] [-ExternalName <String>] [-Searchable] [-Auditing]
 [-ColumnSecurity] [-Source <ColumnSourceType>]  
    
  [<RequestParameters>] [<CommonParameters>]
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

### -Auditing
{{ Fill Auditing Description }}

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
{{ Fill ColumnSecurity Description }}

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
{{ Fill Description Description }}

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
{{ Fill DisplayName Description }}

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
{{ Fill ExternalName Description }}

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
{{ Fill InputObject Description }}

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
{{ Fill Name Description }}

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

### -Required
{{ Fill Required Description }}

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnRequiredLevel
Parameter Sets: AddColumnByParameters
Aliases:
Accepted values: Optional, Required, Recommended

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Searchable
{{ Fill Searchable Description }}

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
{{ Fill Source Description }}

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
{{ Fill Table Description }}

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
{{ Fill Type Description }}

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

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
## NOTES

## RELATED LINKS

