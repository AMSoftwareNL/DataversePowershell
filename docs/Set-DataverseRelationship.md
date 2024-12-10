---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRelationship

## SYNOPSIS
Update a relationship

## SYNTAX

### SetManyToOneRelationship (Default)
```
Set-DataverseRelationship -Name <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-Behavior <CascadeConfiguration>]  [<CommonParameters>]
```

### SetRelationshipByInputObject
```
Set-DataverseRelationship -InputObject <RelationshipMetadataBase> 
 [<CommonParameters>]
```

### SetManyToManyRelationship
```
Set-DataverseRelationship -Name <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-RelatedMenuConfiguration <AssociatedMenuConfiguration>] 
 [<CommonParameters>]
```

## DESCRIPTION
Update a relationship

## EXAMPLES

## PARAMETERS

### -Behavior
New cascade configuration for the relationship

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.CascadeConfiguration
Parameter Sets: SetManyToOneRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Relationship Metadata object describing the updated relationship

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase
Parameter Sets: SetRelationshipByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -MenuConfiguration
New menu configuration for the relationship

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AssociatedMenuConfiguration
Parameter Sets: SetManyToOneRelationship, SetManyToManyRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

```yaml
Type: System.String
Parameter Sets: SetManyToOneRelationship, SetManyToManyRelationship
Aliases: SchemaName, Relationship

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedMenuConfiguration
New menu configuration for the relationship

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.AssociatedMenuConfiguration
Parameter Sets: SetManyToManyRelationship
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

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseRelationship.md)


