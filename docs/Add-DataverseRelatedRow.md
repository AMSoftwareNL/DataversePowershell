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
 -Relationship <String> [-BatchId <Guid>]   
   
  [<RequestParameters>] [<CommonParameters>]
```

### AddCollectionRelatedRows
```
Add-DataverseRelatedRow -TargetTable <String> -TargetRow <Guid> -Rows <EntityReference[]>
 -Relationship <String> [-BatchId <Guid>]   
   
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

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
### System.Guid
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

