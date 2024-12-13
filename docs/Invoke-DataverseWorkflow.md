---
external help file: AMSoftware.Dataverse.PowerShell.Administration.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Invoke-DataverseWorkflow

## SYNOPSIS
Start a workflow (classic) on a specific record.

## SYNTAX

```
Invoke-DataverseWorkflow [-Row] <Guid> [-Workflow] <Guid> 
 [<CommonParameters>]
```

## DESCRIPTION
Start a workflow (classic) on a specific record. The workflow has to support manual start. The Table is infered from the workflow definition.

## EXAMPLES


## PARAMETERS

### -Row
Id of the Row as input for the Workflow

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Id

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Workflow
The ID of the workflow to run

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Process

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

### System.Guid
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Invoke-DataverseWorkflow.md)



