---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseColumn

## SYNOPSIS
Retrieve table column metadata

## SYNTAX

### GetColumnsByFilter (Default)
```
Get-DataverseColumn -Table <String> [-Name <String>] [-Exclude <String>] [-Type <ColumnType>] [-Unmanaged]
  [<CommonParameters>]
```

### GetColumnById
```
Get-DataverseColumn -Id <Guid>  [<CommonParameters>]
```

## DESCRIPTION
Retrieve table column metadata

## EXAMPLES

### Example 1
```powershell
PS C:/> Get-DataverseColumn -Table 'account' -Name 'accountnumber'
```

### Example 2
```powershell
PS C:/>  Get-DataverseColumn -Table 'account' -Type Lookup
```

## PARAMETERS

### -Exclude
Filter of columns to exclude based on the logicalname. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetColumnsByFilter
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
MetadataId of the column to retrieve

```yaml
Type: System.Guid
Parameter Sets: GetColumnById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Name
The logicalname of the column to retrieve. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetColumnsByFilter
Aliases: Include

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Table
Logicalname of the table containing the column

```yaml
Type: System.String
Parameter Sets: GetColumnsByFilter
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The type of column to retrieve

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnType
Parameter Sets: GetColumnsByFilter
Aliases:
Accepted values: Boolean, Customer, DateTime, Decimal, Double, Integer, Lookup, Memo, Money, Owner, PartyList, Picklist, State, Status, String, Uniqueidentifier, CalendarRules, Virtual, BigInt, ManagedProperty, EntityName, Image, MultiSelectPicklist, File

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unmanaged
Only retrieve unmanaged columns

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetColumnsByFilter
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid
### System.String
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.AttributeMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseColumn.md)


