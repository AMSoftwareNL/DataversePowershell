---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRows

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### RetrieveWithQuery (Default)
```
Get-DataverseRows -Table <String> [-Columns <String[]>] [-Sort <Hashtable>] 
    
   [<RequestParameters>] [<CommonParameters>]
```

### RetrieveWithAttributeQuery
```
Get-DataverseRows -Table <String> -Query <Hashtable> [-Columns <String[]>] [-Sort <Hashtable>]
    
   
 [<RequestParameters>] [<CommonParameters>]
```

### RetrieveWithBatch
```
Get-DataverseRows -Table <String> -Id <Guid[]> [-Columns <String[]>]  
    
  [<RequestParameters>] [<CommonParameters>]
```

### RetrieveWithFetchXml
```
Get-DataverseRows -FetchXml <XmlDocument>   
   
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
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery, RetrieveWithBatch
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -FetchXml
{{ Fill FetchXml Description }}

```yaml
Type: System.Xml.XmlDocument
Parameter Sets: RetrieveWithFetchXml
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid[]
Parameter Sets: RetrieveWithBatch
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Query
{{ Fill Query Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: RetrieveWithAttributeQuery
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Sort
{{ Fill Sort Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery
Aliases:

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
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery, RetrieveWithBatch
Aliases: LogicalName

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

### System.Guid[]
### System.String[]
### System.Xml.XmlDocument
## OUTPUTS

### Microsoft.Xrm.Sdk.Entity
## NOTES

## RELATED LINKS

