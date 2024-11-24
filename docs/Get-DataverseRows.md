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
  [-IncludeTotalCount] [-Skip <UInt64>] [-First <UInt64>]
 [<CommonParameters>]
```

### RetrieveWithAttributeQuery
```
Get-DataverseRows -Table <String> -Query <Hashtable> [-Columns <String[]>] [-Sort <Hashtable>]
  [-IncludeTotalCount] [-Skip <UInt64>] [-First <UInt64>]
 [<CommonParameters>]
```

### RetrieveWithBatch
```
Get-DataverseRows -Table <String> -Id <Guid[]> [-AsBatch] [-Columns <String[]>]
  [-IncludeTotalCount] [-Skip <UInt64>] [-First <UInt64>]
 [<CommonParameters>]
```

### RetrieveWithFetchXml
```
Get-DataverseRows -FetchXml <XmlDocument>  [-IncludeTotalCount]
 [-Skip <UInt64>] [-First <UInt64>] [<CommonParameters>]
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

### -AsBatch
{{ Fill AsBatch Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: RetrieveWithBatch
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

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

### -IncludeTotalCount
Reports the total number of objects in the data set (an integer) followed by the selected objects. If the cmdlet cannot determine the total count, it displays "Unknown total count." The integer has an Accuracy property that indicates the reliability of the total count value. The value of Accuracy ranges from 0.0 to 1.0 where 0.0 means that the cmdlet could not count the objects, 1.0 means that the count is exact, and a value between 0.0 and 1.0 indicates an increasingly reliable estimate.

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

### -Skip
Ignores the specified number of objects and then gets the remaining objects. Enter the number of objects to skip.

```yaml
Type: System.UInt64
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -First
Gets only the specified number of objects. Enter the number of objects to get.

```yaml
Type: System.UInt64
Parameter Sets: (All)
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

### System.Guid[]
### System.String[]
### System.Xml.XmlDocument
## OUTPUTS

### Microsoft.Xrm.Sdk.Entity
## NOTES

## RELATED LINKS
[Online Versions](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseRows.md)

