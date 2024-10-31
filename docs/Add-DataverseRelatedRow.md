---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseRelatedRow

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### AddSingleRelatedRow (Default)
```
Add-DataverseRelatedRow -TargetTable <String> -TargetRow <Guid> -RelatedTable <String> -RelatedRow <Guid>
 -Relationship <String> [-BatchId <Guid>] [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### AddCollectionRelatedRows
```
Add-DataverseRelatedRow -TargetTable <String> -TargetRow <Guid> -Rows <EntityReference[]>
 -Relationship <String> [-BatchId <Guid>] [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
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

### -BatchId
{{ Fill BatchId Description }}

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases:

Required: False
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

### -RelatedRow
{{ Fill RelatedRow Description }}

```yaml
Type: System.Guid
Parameter Sets: AddSingleRelatedRow
Aliases: Id

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -RelatedTable
{{ Fill RelatedTable Description }}

```yaml
Type: System.String
Parameter Sets: AddSingleRelatedRow
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Relationship
{{ Fill Relationship Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Rows
{{ Fill Rows Description }}

```yaml
Type: Microsoft.Xrm.Sdk.EntityReference[]
Parameter Sets: AddCollectionRelatedRows
Aliases:

Required: True
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

### -TargetRow
{{ Fill TargetRow Description }}

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetTable
{{ Fill TargetTable Description }}

```yaml
Type: System.String
Parameter Sets: (All)
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

### System.String

### System.Guid

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS
