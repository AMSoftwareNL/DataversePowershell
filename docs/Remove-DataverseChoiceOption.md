---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Remove-DataverseChoiceOption

## SYNOPSIS
Remove an option from a global choice or choice column

## SYNTAX

### RemoveGlobalChoiceOption (Default)
```
Remove-DataverseChoiceOption -OptionSet <String> -Value <Int32> [-Force] 
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### RemoveAttributeChoiceOption
```
Remove-DataverseChoiceOption -Table <String> -Column <String> -Value <Int32> [-Force]
  [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
Remove an option from a global choice or choice column

## PARAMETERS

### -Column
Logicalname of the choice column

```yaml
Type: System.String
Parameter Sets: RemoveAttributeChoiceOption
Aliases: AttributeLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Force
Remove without confirm

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

### -OptionSet
Logicalname of the choice to remove the option from

```yaml
Type: System.String
Parameter Sets: RemoveGlobalChoiceOption
Aliases: Name, OptionSetLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
Logicalname of the table containing the choice column

```yaml
Type: System.String
Parameter Sets: RemoveAttributeChoiceOption
Aliases: EntityLogicalName, LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
The numeric value of the choice option to remove

```yaml
Type: System.Int32
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs. The cmdlet is not run.

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: False
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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Remove-DataverseChoiceOption.md)
