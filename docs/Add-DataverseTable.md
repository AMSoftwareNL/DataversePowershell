---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseTable

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### AddTableObject (Default)
```
Add-DataverseTable -TableInputObject <EntityMetadata> -ColumnInputObject <StringAttributeMetadata>
 [-Solution <String>] [-SharedTag <String>] [-Partition <String>] [-FailOnDuplicateDetection]
 [-BypassSynchronousLogic] [-BypassPowerAutomateFlows] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### AddElasticTable
```
Add-DataverseTable [-Elastic] -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-OwnershipType <TableOwnershipType>] [-TrackChanges] -ColumnDisplayName <String>
 [-ColumnDescription <String>] -ColumnName <String> [-ColumnLength <UInt32>]
 [-ColumnRequired <ColumnRequiredLevel>] [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### AddVirtualTable
```
Add-DataverseTable [-Virtual] -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-HasAttachments] [-IsActivityParty] -ColumnDisplayName <String> [-ColumnDescription <String>]
 -ColumnName <String> [-ColumnLength <UInt32>] [-ColumnRequired <ColumnRequiredLevel>] -ExternalName <String>
 -ExternalPluralName <String> [-ColumnExternalName <String>] [-DataProviderId <Guid>] [-DataSourceId <Guid>]
 [-Solution <String>] [-SharedTag <String>] [-Partition <String>] [-FailOnDuplicateDetection]
 [-BypassSynchronousLogic] [-BypassPowerAutomateFlows] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### AddActivityTable
```
Add-DataverseTable [-Activity] -Name <String> -DisplayName <String> -PluralName <String>
 [-Description <String>] [-TrackChanges] [-HideFromMenu] [-Solution <String>] [-SharedTag <String>]
 [-Partition <String>] [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### AddStandardTable
```
Add-DataverseTable -Name <String> -DisplayName <String> -PluralName <String> [-Description <String>]
 [-OwnershipType <TableOwnershipType>] [-HasAttachments] [-IsActivityParty] [-TrackChanges]
 -ColumnDisplayName <String> [-ColumnDescription <String>] -ColumnName <String> [-ColumnLength <UInt32>]
 [-ColumnRequired <ColumnRequiredLevel>] [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Activity
{{ Fill Activity Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddActivityTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BypassPowerAutomateFlows
{{ Fill BypassPowerAutomateFlows Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BypassSynchronousLogic
{{ Fill BypassSynchronousLogic Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnDescription
{{ Fill ColumnDescription Description }}

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
{{ Fill ColumnDisplayName Description }}

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
{{ Fill ColumnExternalName Description }}

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
{{ Fill ColumnInputObject Description }}

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
{{ Fill ColumnLength Description }}

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
{{ Fill ColumnName Description }}

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
{{ Fill ColumnRequired Description }}

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
{{ Fill DataProviderId Description }}

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
{{ Fill DataSourceId Description }}

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
{{ Fill Description Description }}

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
{{ Fill DisplayName Description }}

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
{{ Fill Elastic Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddElasticTable
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
Parameter Sets: AddVirtualTable
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalPluralName
{{ Fill ExternalPluralName Description }}

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

### -FailOnDuplicateDetection
{{ Fill FailOnDuplicateDetection Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HasAttachments
{{ Fill HasAttachments Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HideFromMenu
{{ Fill HideFromMenu Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddActivityTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsActivityParty
{{ Fill IsActivityParty Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

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
{{ Fill OwnershipType Description }}

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

### -Partition
{{ Fill Partition Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PluralName
{{ Fill PluralName Description }}

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

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: System.Management.Automation.ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharedTag
{{ Fill SharedTag Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Solution
{{ Fill Solution Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TableInputObject
{{ Fill TableInputObject Description }}

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
{{ Fill TrackChanges Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddElasticTable, AddActivityTable, AddStandardTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Virtual
{{ Fill Virtual Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddVirtualTable
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

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata

## NOTES

## RELATED LINKS
