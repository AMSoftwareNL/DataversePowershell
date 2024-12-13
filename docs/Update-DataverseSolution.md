---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Update-DataverseSolution

## SYNOPSIS
Upgrade a staged or holding solution

## SYNTAX

### UpgradeFromHold
```
Update-DataverseSolution -SolutionName <String>  [<CommonParameters>]
```

### UpgradeFromStaged
```
Update-DataverseSolution -Stage <Guid> [-PublishWorkflows] [-ComponentParameters <EntityCollection>]
  [<CommonParameters>]
```

## DESCRIPTION
Upgrade a solution from staged or holding, as a result from an Import-DataverseSolution.

## EXAMPLES

## PARAMETERS

### -ComponentParameters
The list of entities to overwrite values from the solution. Used for Environment Variables and Connection References.

```yaml
Type: Microsoft.Xrm.Sdk.EntityCollection
Parameter Sets: UpgradeFromStaged
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
Parameter Sets: UpgradeFromStaged
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SolutionName
The uniquename of the solution to upgrade from holding

```yaml
Type: System.String
Parameter Sets: UpgradeFromHold
Aliases: UniqueName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stage
The StageId of the solution to Upgrade as the result from a staged solution through Import-DataverseSolution.

```yaml
Type: System.Guid
Parameter Sets: UpgradeFromStaged
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

### System.Guid

## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Update-DataverseSolution.md)

[Import-DataverseSolution](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Import-DataverseSolution.md)

[Solution staging, with asynchronous import and export](https://learn.microsoft.com/power-platform/alm/solution-async)

