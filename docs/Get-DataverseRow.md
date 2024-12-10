---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRow

## SYNOPSIS
Get a single Dataverse Row

## SYNTAX

### RetrieveWithId
```
Get-DataverseRow -Table <String> -Id <Guid> [-Columns <String[]>] 
 [<CommonParameters>]
```

### RetrieveWithKey
```
Get-DataverseRow -Table <String> -Key <Hashtable> [-Columns <String[]>] 
 [<CommonParameters>]
```

## DESCRIPTION
Retrieve a single Dataverse Row

## EXAMPLES

### Example 1

```powershell
PS C:\> $ids | Get-DataverseRow -Table 'account' -Columns 'name'
```

### Example 2

```powershell
PS C:\> Get-DataverseRows -Table 'account' -Id $rowid -Columns 'name'
```

## PARAMETERS

### -Columns
The columns to include in the result

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
The Id of the row

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
The columns and values for a row based on Table Keys

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
The table to retrieve from

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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseRow.md)


