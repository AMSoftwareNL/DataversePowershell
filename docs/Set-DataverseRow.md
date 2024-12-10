---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRow

## SYNOPSIS
Update row in table

## SYNTAX

### SetObject (Default)
```
Set-DataverseRow -InputObject <Entity> [-Behavior <ConcurrencyBehavior>] [-BatchId <Guid>]
  [<CommonParameters>]
```

### SetValues
```
Set-DataverseRow -Table <String> -Id <Guid> -Values <Hashtable> [-Behavior <ConcurrencyBehavior>]
 [-BatchId <Guid>]  [<CommonParameters>]
```

## DESCRIPTION
Update row in table

## EXAMPLES

### Example 1: Update with Object

```powershell
$row = Get-DataverseRows -Table 'account' -Top 1
$row.name='Account Updated'

$row | Set-DataverseRow
```

### Example 2: Update with Values

```powershell
Set-DataverseRow -Table $row.LogicalName -Id $row.Id -Values @{name='Account Updated'}
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
Logicalname of the table containing the row to updateLogicalname of the table containing the row to update

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
Updated values for the row

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

### Microsoft.Xrm.Sdk.Entity
## OUTPUTS

### Microsoft.Xrm.Sdk.EntityReference
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseRow.md)


