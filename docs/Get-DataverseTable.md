---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseTable

## SYNOPSIS
Retrieve table metadata

## SYNTAX

### GetTablesByFilter (Default)
```
Get-DataverseTable [-Name <String>] [-Exclude <String>] [-Type <TableType>] [-Custom] [-Unmanaged]
 [-Intersects]  [<CommonParameters>]
```

### GetTableById
```
Get-DataverseTable -Id <Guid>  [<CommonParameters>]
```

### GetTableByEtc
```
Get-DataverseTable -TypeCode <Int32>  [<CommonParameters>]
```

## DESCRIPTION
Retrieve table metadata

## EXAMPLES

### Example 1
```powershell
PS C:\> Get-DataverseTable -Name 'account'
```

### Example 2
```powershell
PS C:\> $accountTable.MetadataId | Get-DataverseTable 
```

### Example 3
```powershell
PS C:\> Get-DataverseTable -TypeCode 1
```

### Example 4
```powershell
PS C:\> Get-DataverseTable -Name 'account' -Exclude 'contact' -Type Standard 
```

### Example 5
```powershell
PS C:\> Get-DataverseTable -Intersects
```

## PARAMETERS

### -Custom
Only retrieve custom tables

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Filter of tables to exclude based on the logicalname. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
MetadataId of the table to retrieve

```yaml
Type: System.Guid
Parameter Sets: GetTableById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Intersects
Include intersect tables from many-to-many relationships

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The logicalname of the table to retrieve. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetTablesByFilter
Aliases: Include, LogicalName

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Type
The type of table to retrieve

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.TableType
Parameter Sets: GetTablesByFilter
Aliases:
Accepted values: Standard, Activity, Virtual, Datasource, Elastic

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TypeCode
The Entity Type Code (etc) of the table to retrieve. Also known as the Object Type Code.

```yaml
Type: System.Int32
Parameter Sets: GetTableByEtc
Aliases: ObjectTypeCode, EntityTypeCode

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unmanaged
Only retrieve unmanaged tables

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetTablesByFilter
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
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseTable.md)


