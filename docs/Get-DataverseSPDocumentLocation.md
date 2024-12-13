---
external help file: AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseSPDocumentLocation

## SYNOPSIS
Retrieve to SharePoint Document Location URL for a Row

## SYNTAX

### GetBySharePointDocumentLocation
```
Get-DataverseSPDocumentLocation -DocumentLocation <Guid> 
 [<CommonParameters>]
```

### GetByRegardingObject
```
Get-DataverseSPDocumentLocation -RegardingObject <Guid> 
 [<CommonParameters>]
```

## DESCRIPTION
Retrieve to SharePoint Document Location for a Row or Document Location.

## EXAMPLES


## PARAMETERS

### -DocumentLocation
Id of the specific Document Location to get the URL information for

```yaml
Type: System.Guid
Parameter Sets: GetBySharePointDocumentLocation
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RegardingObject
Id of the specific Row to get the URL information for

```yaml
Type: System.Guid
Parameter Sets: GetByRegardingObject
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

### None
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseSPDocumentLocation.md)



