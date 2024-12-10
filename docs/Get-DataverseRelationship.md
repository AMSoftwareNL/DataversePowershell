---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRelationship

## SYNOPSIS
Retrieve Relationship Metadata

## SYNTAX

### GetRelationshipForTable (Default)
```
Get-DataverseRelationship -Table <String> [-RelatedTable <String>] [-Type <RelationshipType>]
 [-Include <String>] [-Exclude <String>] [-Custom] [-Unmanaged] 
 [<CommonParameters>]
```

### GetRelationshipById
```
Get-DataverseRelationship -Id <Guid>  [<CommonParameters>]
```

### GetRelationshipByName
```
Get-DataverseRelationship -Name <String>  [<CommonParameters>]
```

## DESCRIPTION
Retrieve Relationship Metadata

## EXAMPLES

### Example 1
```powershell
PS C:\>  Get-DataverseRelationship -Table 'account'
```

### Example 2
```powershell
PS C:\> Get-DataverseRelationship -Name 'ams_account_contact'
```

### Example 3
```powershell
PS C:\>  Get-DataverseRelationship -Table 'account' -RelatedTable 'contact'
```

## PARAMETERS

### -Custom
Only retrieve custom relationships

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Filter of relationships to exclude based on the schemaname. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
MetadataId of the relationship to retrieve

```yaml
Type: System.Guid
Parameter Sets: GetRelationshipById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Include
The schemaname of the relationship to retrieve. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Name
Schemaname of the relationship to retrieve

```yaml
Type: System.String
Parameter Sets: GetRelationshipByName
Aliases: SchemaName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedTable
Logicalname of the table on the other side of the relationship

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
Logicalname of the table to retrieve the relationship for

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
The type of relationship to retrieve

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.RelationshipType
Parameter Sets: GetRelationshipForTable
Aliases:
Accepted values: OneToManyRelationship, Default, ManyToManyRelationship

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unmanaged
Only retrieve unmanaged relationships

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetRelationshipForTable
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

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseRelationship.md)


