---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseTable

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### GetTablesByFilter (Default)
```
Get-DataverseTable [-Name <String>] [-Exclude <String>] [-Type <TableType>] [-Custom] [-Unmanaged]
 [-Intersects] [-Solution <String>] [-SharedTag <String>] [-Partition <String>] [-FailOnDuplicateDetection]
 [-BypassSynchronousLogic] [-BypassPowerAutomateFlows] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### GetTableById
```
Get-DataverseTable -Id <Guid> [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### GetTableByEtc
```
Get-DataverseTable -TypeCode <Int32> [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
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

### -Custom
{{ Fill Custom Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
{{ Fill Exclude Description }}

```yaml
Type: System.String
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
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

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid
Parameter Sets: GetTableById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Intersects
{{ Fill Intersects Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
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
Parameter Sets: GetTablesByFilter
Aliases: Include, LogicalName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
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

### -Type
{{ Fill Type Description }}

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.TableType
Parameter Sets: GetTablesByFilter
Aliases:
Accepted values: Standard, Activity, Virtual, Datasource, Elastic

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TypeCode
{{ Fill TypeCode Description }}

```yaml
Type: System.Int32
Parameter Sets: GetTableByEtc
Aliases: ObjectTypeCode, EntityTypeCode

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unmanaged
{{ Fill Unmanaged Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid

## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata

## NOTES

## RELATED LINKS
