---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseRows

## SYNOPSIS
Add new rows to a table

## SYNTAX

### AddObject (Default)
```
Add-DataverseRows -InputObject <Entity[]> [-Upsert] [-BatchId <Guid>] 
 [<CommonParameters>]
```

### AddValues
```
Add-DataverseRows -Table <String> -Values <Hashtable> [-BatchId <Guid>] 
 [<CommonParameters>]
```

## DESCRIPTION
Add new rows to a table using CreateMultiple or UpsertMultiple

## EXAMPLES

### Example 1: Add rows from InputObject

```powershell
PS C:\> 1..500 | ForEach-Object { 
    $row = [dvrow]::new('account'); 
    $row.name = "Multiple Account $_"; 
    $row 
} | Add-DataverseRows
```

### Example 2: Add rows from Values

```powershell
PS C:\> 1..500 | ForEach-Object { 
    $row = @{ name = "Multiple Account $_" }; 
    $row 
} | Add-DataverseRows -Table 'account'
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

### -InputObject
Entity instance containing the row to add. When Upsert make sure the instance contains all the information.

```yaml
Type: Microsoft.Xrm.Sdk.Entity[]
Parameter Sets: AddObject
Aliases: Rows, Entities

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Table
Logical name of the table to add the rows to

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

### -Upsert
Try UpsertMultiple instead of CreateMultiple.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddObject
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Values
Column Logical Names and Values (as hashtable) to use for the new rows.

```yaml
Type: System.Collections.Hashtable
Parameter Sets: AddValues
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseRows.md)


