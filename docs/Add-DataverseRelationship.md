---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseRelationship

## SYNOPSIS
Add relationship to Table

## SYNTAX

### AddManyToOneRelationship (Default)
```
Add-DataverseRelationship -Table <String> -ColumnName <String> -ColumnDisplayName <String>
 [-ColumnRequired <ColumnRequiredLevel>] [-ColumnDescription <String>] -RelatedTable <String> -Name <String>
 [-Searchable] [-MenuConfiguration <AssociatedMenuConfiguration>] [-Behavior <CascadeConfiguration>]
 [-Polymorphic]  [<CommonParameters>]
```

### AddCustomerRelationship
```
Add-DataverseRelationship -Table <String> -ColumnName <String> -ColumnDisplayName <String>
 -ColumnRequired <ColumnRequiredLevel> -ColumnDescription <String> -AccountName <String> -ContactName <String>
 [-Searchable] [-Customer]  [<CommonParameters>]
```

### AddManyToManyRelationship
```
Add-DataverseRelationship -Table <String> -RelatedTable <String> -Name <String> -Intersect <String>
 [-Searchable] [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-RelatedMenuConfiguration <AssociatedMenuConfiguration>] 
 [<CommonParameters>]
```

## DESCRIPTION
Add relationship to Table.
Also use this to create Lookup Columns and Customer Columns

## EXAMPLES

### Example 1
```powershell
PS C:\> Add-DataverseRelationship -Table 'account' -RelatedTable 'contact' -Name 'ams_testmany' -Intersect 'ams_account_contact_intersect' -Searchable
```

### Example 2
```powershell
PS C:\>  Add-DataverseRelationship -Table 'account' -ColumnName 'ams_testlookup' -ColumnDisplayName 'Test Lookup' -ColumnRequired Recommended -ColumnDescription 'Test Lookup Description' -RelatedTable 'contact' -Name 'ams_testlookup' -Searchable
```

### Example 3
```powershell
PS C:\>  Add-DataverseRelationship -Table 'account' -ColumnName 'ams_testcustomer' -ColumnDisplayName 'Test Customer' -ColumnRequired Recommended -ColumnDescription 'Test Customer Description' -AccountName 'ams_account_customeraccount' -ContactName 'ams_account_customercontact' -Searchable
```

### Example 4
```powershell
PS C:\>  Add-DataverseRelationship -Table 'account' -ColumnName 'ams_testpolymorphic' -ColumnDisplayName 'Test Polymorphic' -ColumnRequired Recommended -ColumnDescription 'Test Polymorphic Description' -RelatedTable 'contact' -Name 'ams_testpolymorphic' -Searchable -Polymorphic
```

## PARAMETERS

### -AccountName
Schemaname for the relationship to the account Table

```yaml
Type: System.String
Parameter Sets: AddCustomerRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Behavior
CascadeConfiguration for the relationship

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.CascadeConfiguration
Parameter Sets: AddManyToOneRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnDescription
Description for the Lookup Column

```yaml
Type: System.String
Parameter Sets: AddManyToOneRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: System.String
Parameter Sets: AddCustomerRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnDisplayName
Display name for the Lookup Column

```yaml
Type: System.String
Parameter Sets: AddManyToOneRelationship, AddCustomerRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnName
Logical Name for the Lookup Column

```yaml
Type: System.String
Parameter Sets: AddManyToOneRelationship, AddCustomerRelationship
Aliases: AttributeLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ColumnRequired
Required level for the Lookup Column

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnRequiredLevel
Parameter Sets: AddManyToOneRelationship
Aliases:
Accepted values: Optional, Required, Recommended

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: AMSoftware.Dataverse.PowerShell.Commands.ColumnRequiredLevel
Parameter Sets: AddCustomerRelationship
Aliases:
Accepted values: Optional, Required, Recommended

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContactName
Schemaname for the relationship to the contact Table

```yaml
Type: System.String
Parameter Sets: AddCustomerRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Customer
Create a customer type relationship

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddCustomerRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Intersect
Logical Name forthe intersect table

```yaml
Type: System.String
Parameter Sets: AddManyToManyRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MenuConfiguration
AssociatedMenuConfiguration for the relationship Table

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AssociatedMenuConfiguration
Parameter Sets: AddManyToOneRelationship, AddManyToManyRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Schema name for the relationship

```yaml
Type: System.String
Parameter Sets: AddManyToOneRelationship, AddManyToManyRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Polymorphic
Create polymorphic lookup for the relationship.
When ColumnName already exists the Relationship is added to this Lookup.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: AddManyToOneRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedMenuConfiguration
AssociatedMenuConfiguration for the Related Table

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AssociatedMenuConfiguration
Parameter Sets: AddManyToManyRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedTable
The related table for the relationship.

```yaml
Type: System.String
Parameter Sets: AddManyToOneRelationship, AddManyToManyRelationship
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Searchable
Include the relationship and lookup column in Advanced Find

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

### -Table
Table to add the relationship to.
This is the table the lookup columns are added to.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: EntityLogicalName, LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseRelationship.md)

