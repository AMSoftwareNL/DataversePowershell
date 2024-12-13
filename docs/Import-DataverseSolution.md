---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Import-DataverseSolution

## SYNOPSIS
Import a Dataverse Solution

## SYNTAX

### ImportSolution (Default)
```
Import-DataverseSolution -LiteralPath <String> [-Overwrite] [-Hold] [-PublishWorkflows]
 [-ComponentParameters <EntityCollection>]  [<CommonParameters>]
```

### ImportAsStaged
```
Import-DataverseSolution -LiteralPath <String> [-Stage] [-Overwrite] 
 [<CommonParameters>]
```

### ImportAsUpgrade
```
Import-DataverseSolution -LiteralPath <String> [-Upgrade] [-Overwrite] [-PublishWorkflows]
 [-ComponentParameters <EntityCollection>]  [<CommonParameters>]
```

## DESCRIPTION
Import a Dataverse Solution. Supports the following:

- Import a new solution
- Import a solution as holding (deprecated) for later upgrade using Update-DataverseSolution
- Import a solution as staged for later upgrade using Update-DataverseSolution
- Import and Upgrade a solution using the improved Stage and Upgrade process

## EXAMPLES


## PARAMETERS

### -ComponentParameters
The list of entities to overwrite values from the solution. Used for Environment Variables and Connection References.

```yaml
Type: Microsoft.Xrm.Sdk.EntityCollection
Parameter Sets: ImportSolution, ImportAsUpgrade
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Hold
Import the solution as a holding solution for later upgrade

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: ImportSolution
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -LiteralPath
Full path to the solution file to import

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: PSPath

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Overwrite
Overwrite existing unmnaged customizations

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

### -PublishWorkflows
Processes (workflows) included in the solution should be activated after they are imported.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: ImportSolution, ImportAsUpgrade
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stage
Import the solution as Staged for later Upgrade

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: ImportAsStaged
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Upgrade
Import the solution with stage and upgrade using the improved process

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: ImportAsUpgrade
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

### None

## OUTPUTS

### System.Guid

### Microsoft.Xrm.Sdk.StageSolutionResults

## NOTES

When Stage is used the Function returns StageSolutionResults. This Ouput contains the StageId which is needed to Upgrade using Update-DataverseSolution.

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Import-DataverseSolution.md)

[Update-DataverseSolution](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Update-DataverseSolution.md)

[Solution staging, with asynchronous import and export](https://learn.microsoft.com/power-platform/alm/solution-async)

