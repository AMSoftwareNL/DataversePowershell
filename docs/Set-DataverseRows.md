---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRows

## SYNOPSIS
Update rows in table

## SYNTAX

### SetObject (Default)
```
Set-DataverseRows -InputObject <Entity[]> [-Behavior <ConcurrencyBehavior>] [-BatchId <Guid>]
  [<CommonParameters>]
```

### SetValues
```
Set-DataverseRows -Table <String> -Id <Guid> -Values <Hashtable> [-Behavior <ConcurrencyBehavior>]
 [-BatchId <Guid>]  [<CommonParameters>]
```

## DESCRIPTION
Update rows in table using UpdateMultiple request

## EXAMPLES

### Example 1: Bulk update

```powershell
$rows | Set-DataverseRow -Values @{name='Account Updated'}
```

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
Concurrency behavior of the update

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
Id of the row the update

```yaml
Type: System.Guid
Parameter Sets: SetValues
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -InputObject
Entity object with Attributes with updated values

```yaml
Type: Microsoft.Xrm.Sdk.Entity[]
Parameter Sets: SetObject
Aliases: Rows, Entities

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Table
Logicalname of the table containing the rows to update

```yaml
Type: System.String
Parameter Sets: SetValues
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Values
Updated values for the rows

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Entity[]
## OUTPUTS

### Microsoft.Xrm.Sdk.EntityReference
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseRows.md)
