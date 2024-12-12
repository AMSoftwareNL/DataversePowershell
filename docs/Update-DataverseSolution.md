---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Update-DataverseSolution

## SYNOPSIS
{{ Fill in the Synopsis }}

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
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -ComponentParameters
{{ Fill ComponentParameters Description }}

```yaml
Type: EntityCollection
Parameter Sets: UpgradeFromStaged
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -PublishWorkflows
{{ Fill PublishWorkflows Description }}

```yaml
Type: SwitchParameter
Parameter Sets: UpgradeFromStaged
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SolutionName
{{ Fill SolutionName Description }}

```yaml
Type: String
Parameter Sets: UpgradeFromHold
Aliases: UniqueName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Stage
{{ Fill Stage Description }}

```yaml
Type: Guid
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

