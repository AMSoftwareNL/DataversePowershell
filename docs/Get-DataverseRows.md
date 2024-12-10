---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRows

## SYNOPSIS
Get multiple rows from a Dataverse Table

## SYNTAX

### RetrieveWithQuery (Default)
```
Get-DataverseRows -Table <String> [-Columns <String[]>] [-Sort <Hashtable>] [-Top <Int32>]
  [<CommonParameters>]
```

### RetrieveWithAttributeQuery
```
Get-DataverseRows -Table <String> -Query <Hashtable> [-Columns <String[]>] [-Sort <Hashtable>] [-Top <Int32>]
  [<CommonParameters>]
```

### RetrieveWithBatch
```
Get-DataverseRows -Table <String> -Id <Guid[]> [-Columns <String[]>] 
 [<CommonParameters>]
```

### RetrieveWithFetchXml
```
Get-DataverseRows -FetchXml <XmlDocument> [-Top <Int32>] 
 [<CommonParameters>]
```

## DESCRIPTION
Get multiple rows from a Dataverse Table based in queries or a list of Ids.

Paging is applied in case of queries. In case of Ids batching is used to improve performance.

## EXAMPLES

### Example 1: Retrieve With FetchXml

```powershell
PS C:\> [xml]$fetchxml = 
@"
<fetch>
  <entity name='account'>
    <attribute name='name' />
  </entity>
</fetch>
"@

PS C:\> Get-DataverseRows -FetchXML $fetchxml -Top 50
```

### Example 2: Retrieve With Attribute Query

```powershell
PS C:\> Get-DataverseRows -Table 'account' -Columns 'name' -Sort @{name=[Microsoft.Xrm.Sdk.Query.OrderType]::Ascending} -Query @{name='Account 1'}
```

### Example 3: Retrieve from table

```powershell
PS C:\> Get-DataverseRows -Table 'account' -Columns 'name' -Sort @{name=[Microsoft.Xrm.Sdk.Query.OrderType]::Ascending} -Top 50
```

### Example 4: Retrieve batched

```powershell
PS C:\> $ids = Get-DataverseRows -Table 'contact' -Columns 'parentcustomerid' | Select-Object -ExpandProperty 'parentcustomerid' -Unique

PS C:\> $ids | Get-DataverseRows -Table 'account' | Format-List
```

## PARAMETERS

### -Columns
The columns to retrieve

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
The FetchXML query

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
Ids of rows to retrieve. Can come from the pipeline.

```yaml
Type: System.Guid[]
Parameter Sets: RetrieveWithBatch
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Query
Columnnames and Values to filter on. Multiple are combined with AND.

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
Columnnames and OrderType (Microsoft.Xrm.Sdk.Query.OrderType)

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
Table to get the rows from

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

### -Top
Top number of rows to retrieve. Must be less then max pagesize (=5000).

```yaml
Type: System.Int32
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery, RetrieveWithFetchXml
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





