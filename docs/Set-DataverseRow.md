---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRow

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### SetObject (Default)
```
Set-DataverseRow -InputObject <Entity> [-Behavior <ConcurrencyBehavior>] [-BatchId <Guid>] 
    
   [<RequestParameters>] [<CommonParameters>]
```

### SetValues
```
Set-DataverseRow -Table <String> -Id <Guid> -Values <Hashtable> [-Behavior <ConcurrencyBehavior>]
 [-BatchId <Guid>]    
   
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

### -Behavior
{{ Fill Behavior Description }}

```yaml
Type: Microsoft.Xrm.Sdk.ConcurrencyBehavior
Parameter Sets: (All)
Aliases:
Accepted values: Default, IfRowVersionMatches, AlwaysOverwrite

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
Parameter Sets: SetValues
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
{{ Fill InputObject Description }}

```yaml
Type: Microsoft.Xrm.Sdk.Entity
Parameter Sets: SetObject
Aliases: Row, Entity

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Table
{{ Fill Table Description }}

```yaml
Type: System.String
Parameter Sets: SetValues
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
{{ Fill Values Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: SetValues
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

### Microsoft.Xrm.Sdk.Entity
## OUTPUTS

### Microsoft.Xrm.Sdk.EntityReference
## NOTES

## RELATED LINKS

