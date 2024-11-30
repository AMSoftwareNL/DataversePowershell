---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseTable

## SYNOPSIS
Add new Table to Dataverse

## SYNTAX

### AddTableObject (Default)
```
Add-DataverseTable -TableInputObject <EntityMetadata> -ColumnInputObject <StringAttributeMetadata>
  [<CommonParameters>]
```

### AddElasticTable
```
Add-DataverseTable [-Elastic] -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-OwnershipType <TableOwnershipType>] [-TrackChanges] -ColumnDisplayName <String>
 [-ColumnDescription <String>] -ColumnName <String> [-ColumnLength <UInt32>]
 [-ColumnRequired <ColumnRequiredLevel>]  [<CommonParameters>]
```

### AddVirtualTable
```
Add-DataverseTable [-Virtual] -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-HasAttachments] [-IsActivityParty] -ColumnDisplayName <String> [-ColumnDescription <String>]
 -ColumnName <String> [-ColumnLength <UInt32>] [-ColumnRequired <ColumnRequiredLevel>] -ExternalName <String>
 -ExternalPluralName <String> [-ColumnExternalName <String>] [-DataProviderId <Guid>] [-DataSourceId <Guid>]
  [<CommonParameters>]
```

### AddActivityTable
```
Add-DataverseTable [-Activity] -Name <String> -DisplayName <String> -PluralName <String>
 [-Description <String>] [-TrackChanges] [-HideFromMenu] 
 [<CommonParameters>]
```

### AddStandardTable
```
Add-DataverseTable -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-OwnershipType <TableOwnershipType>] [-HasAttachments] [-IsActivityParty] [-TrackChanges]
 -ColumnDisplayName <String> [-ColumnDescription <String>] -ColumnName <String> [-ColumnLength <UInt32>]
 [-ColumnRequired <ColumnRequiredLevel>]  [<CommonParameters>]
```

## DESCRIPTION
Add a new Table to Dataverse, including the primary name column

## PARAMETERS

### -Activity
Add an Activity table

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddActivityTable
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnDescription
Description for the primary name column

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddStandardTable
Aliases: PrimaryAttributeDescription

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnDisplayName
Displayname for the primary name column

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddStandardTable
Aliases: PrimaryAttributeDisplayName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnExternalName
External name for the primary name column

```yaml
Type: System.String
Parameter Sets: AddVirtualTable
Aliases: PrimaryAttributeExternalName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnInputObject
String Attribute Metadata for the primary name column

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.StringAttributeMetadata
Parameter Sets: AddTableObject
Aliases: Attribute, PrimaryAttribute

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnLength
Length for the primary name column

```yaml
Type: System.UInt32
Parameter Sets: AddElasticTable, AddVirtualTable, AddStandardTable
Aliases: PrimaryAttributeLength

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnName
Logicalname for the primary name column

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddStandardTable
Aliases: PrimaryAttributeSchemaName, PrimaryAttributeLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnRequired
Requirement level for the primary name column

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnRequiredLevel
Parameter Sets: AddElasticTable, AddVirtualTable, AddStandardTable
Aliases: PrimaryAttributeRequirement
Accepted values: Optional, Required, Recommended

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DataProviderId
The Id that identifies the associated virtual table data provider

```yaml
Type: System.Guid
Parameter Sets: AddVirtualTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DataSourceId
The Id that identifies the associated virtual table data source

```yaml
Type: System.Guid
Parameter Sets: AddVirtualTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The description for the table

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddActivityTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
The displayname for the table

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddActivityTable, AddStandardTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Elastic
Create an eleastic table

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddElasticTable
Aliases:

Required: True
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
The external name for the table

```yaml
Type: System.String
Parameter Sets: AddVirtualTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalPluralName
The external plural name for the table

```yaml
Type: System.String
Parameter Sets: AddVirtualTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HasAttachments
Can the table have attachments

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideFromMenu
Hide the new activity from the activity menu

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddActivityTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsActivityParty
Table can be used as party in an activity

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Logicalname for the table

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddActivityTable, AddStandardTable
Aliases: SchemaName, LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -OwnershipType
The ownership type for the table

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.TableOwnershipType
Parameter Sets: AddElasticTable, AddStandardTable
Aliases:
Accepted values: User, Organization

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PluralName
The plural name of the table

```yaml
Type: System.String
Parameter Sets: AddElasticTable, AddVirtualTable, AddActivityTable, AddStandardTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TableInputObject
The EntityMetadata object describing the table

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.EntityMetadata
Parameter Sets: AddTableObject
Aliases: Entity

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrackChanges
Track change is enabled

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddElasticTable, AddActivityTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Virtual
Create a virtual table

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable
Aliases:

Required: True
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

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseTable.md)
