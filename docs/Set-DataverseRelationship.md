---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRelationship

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### SetManyToOneRelationship (Default)
```
Set-DataverseRelationship -Relationship <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-Behavior <CascadeConfiguration>] [-MergeLabels]  
    
  [<RequestParameters>] [<CommonParameters>]
```

### SetRelationshipByInputObject
```
Set-DataverseRelationship -InputObject <RelationshipMetadataBase> [-MergeLabels] 
    
   [<RequestParameters>] [<CommonParameters>]
```

### SetManyToManyRelationship
```
Set-DataverseRelationship -Relationship <String> [-MenuConfiguration <AssociatedMenuConfiguration>]
 [-RelatedMenuConfiguration <AssociatedMenuConfiguration>] [-MergeLabels] 
    
   [<RequestParameters>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Behavior
{{ Fill Behavior Description }}

```yaml
Type: CascadeConfiguration
Parameter Sets: SetManyToOneRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
{{ Fill InputObject Description }}

```yaml
Type: RelationshipMetadataBase
Parameter Sets: SetRelationshipByInputObject
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -MenuConfiguration
{{ Fill MenuConfiguration Description }}

```yaml
Type: AssociatedMenuConfiguration
Parameter Sets: SetManyToOneRelationship, SetManyToManyRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -MergeLabels
{{ Fill MergeLabels Description }}

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedMenuConfiguration
{{ Fill RelatedMenuConfiguration Description }}

```yaml
Type: AssociatedMenuConfiguration
Parameter Sets: SetManyToManyRelationship
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Relationship
{{ Fill Relationship Description }}

```yaml
Type: String
Parameter Sets: SetManyToOneRelationship, SetManyToManyRelationship
Aliases: SchemaName, Name

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## NOTES

## RELATED LINKS

