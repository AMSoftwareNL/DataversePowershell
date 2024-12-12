---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Import-DataverseSolution

## SYNOPSIS
{{ Fill in the Synopsis }}

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
{{ Fill Hold Description }}

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
{{ Fill LiteralPath Description }}

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
{{ Fill Overwrite Description }}

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
{{ Fill PublishWorkflows Description }}

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
{{ Fill Stage Description }}

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
{{ Fill Upgrade Description }}

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

## RELATED LINKS

