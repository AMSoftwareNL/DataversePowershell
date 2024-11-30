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
Set-DataverseRelationship -Relationship <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-Behavior <CascadeConfiguration>] [-MergeLabels]  [<CommonParameters>]
```

### SetRelationshipByInputObject
```
Set-DataverseRelationship -InputObject <RelationshipMetadataBase> [-MergeLabels]
  [<CommonParameters>]
```

### SetManyToManyRelationship
```
Set-DataverseRelationship -Relationship <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-RelatedMenuConfiguration <AssociatedMenuConfiguration>] [-MergeLabels] 
 [<CommonParameters>]
```

## DESCRIPTION
Update a relationship

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

### -MergeLabels
Keep other language labels for DisplayName and Description

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

### -Relationship
Schemaname of the relationship to update

```yaml
Type: System.String
Parameter Sets: SetManyToOneRelationship, SetManyToManyRelationship
Aliases: SchemaName, Name

Required: True
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
