---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseRow

## SYNOPSIS
Add a new row to a table

## SYNTAX

### AddObject (Default)
```
Add-DataverseRow -InputObject <Entity> [-BatchId <Guid>] 
 [<CommonParameters>]
```

### AddValues
```
Add-DataverseRow -Table <String> -Values <Hashtable> [-Id <Guid>] [-Key <Hashtable>] [-BatchId <Guid>]
  [<CommonParameters>]
```

## DESCRIPTION
Add a new row to a table using Create or Upsert message.

## EXAMPLES

### Example 1: Add row with values

```powershell
$values = @{
  'name' = 'Test Account'
}

Add-DataverseRow -Table 'account' -Values $values
```

### Example 2: Add row with InputObject

```powershell
$inputobject = [dvrow]::new('account')
$inputobject.name = 'Test Account'

$inputobject | Add-DataverseRow
```

### Example 3: Upsert row with InputObject

```powershell
Add-DataverseRow -Table 'account' -Id $rowid -Values @{ name = 'Account (Updated)' }
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

### -Id
Id of the row to add. If provided an Upsert is tried instead of a Create.

```yaml
Type: System.Guid
Parameter Sets: AddValues
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
The object of the row to add. If the object contains a value for Id or values in the KeyAttributes an Upsert is used instead of Create.

```yaml
Type: Microsoft.Xrm.Sdk.Entity
Parameter Sets: AddObject
Aliases: Row, Entity

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Key
Name and Value for a Key on the Table. If provided an Upsert is tried instead of Create.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: AddValues
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
The table to add the row to

```yaml
Type: System.String
Parameter Sets: AddValues
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Columnnames and Values for the row

```yaml
Type: System.Collections.Hashtable
Parameter Sets: AddValues
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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseRow.md)
