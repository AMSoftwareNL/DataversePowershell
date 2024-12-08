---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRow

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### RetrieveWithId
```
Get-DataverseRow -Table <String> -Id <Guid> [-Columns <String[]>]  
    
  [<RequestParameters>] [<CommonParameters>]
```

### RetrieveWithKey
```
Get-DataverseRow -Table <String> -Key <Hashtable> [-Columns <String[]>] 
    
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

### -Columns
{{ Fill Columns Description }}

```yaml
Type: System.String[]
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
Parameter Sets: RetrieveWithId
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Key
{{ Fill Key Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: RetrieveWithKey
Aliases:

Required: True
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
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
### System.Guid
### System.String[]
## OUTPUTS

### Microsoft.Xrm.Sdk.Entity
## NOTES

## RELATED LINKS

