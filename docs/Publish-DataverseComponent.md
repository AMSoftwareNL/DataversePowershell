---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Publish-DataverseComponent

## SYNOPSIS
Publish Customizations

## SYNTAX

### PublishAll (Default)
```
Publish-DataverseComponent  [<CommonParameters>]
```

### PublishComponent
```
Publish-DataverseComponent -Type <String> -ComponentId <String> 
 [<CommonParameters>]
```

## DESCRIPTION
Publish customizations. Publishish all customization, or a specific component.

If the Component is provided through the pipeline, all components are collected and published as a single action.

## EXAMPLES

## PARAMETERS

### -ComponentId
The ID of the component to publish. For a Table this is the LogicalName, for a Choice this is the Name, and for a WebResource this is the ID.

```yaml
Type: System.String
Parameter Sets: PublishComponent
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Type
The type of Component to publish. This can be a Table, Global Choice, or WebResource.

```yaml
Type: System.String
Parameter Sets: PublishComponent
Aliases:
Accepted values: Table, Choice, WebResource

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Publish-DataverseComponent.md)





