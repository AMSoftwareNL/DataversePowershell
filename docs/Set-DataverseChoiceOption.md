---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseChoiceOption

## SYNOPSIS
Add or Update an option in a choice

## SYNTAX

### SetGlobalChoiceOption (Default)
```
Set-DataverseChoiceOption -Name <String> -Value <Int32> [-Label <String>] [-Description <String>]
  [<CommonParameters>]
```

### SetAttributeChoiceOption
```
Set-DataverseChoiceOption -Table <String> -Column <String> -Value <Int32> [-Label <String>]
 [-Description <String>]  [<CommonParameters>]
```

## DESCRIPTION
Update an option in a global or column choice. Update is done based on the Value. If value doesn't already exist the Value is added to the choice.


## EXAMPLES

## PARAMETERS

### -Column
The logicalname of the choice column to update

```yaml
Type: System.String
Parameter Sets: SetAttributeChoiceOption
Aliases: AttributeLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
The new description for the choice option

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Label
The new label for the choice option

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Logical Name of the global Choice to update

```yaml
Type: System.String
Parameter Sets: SetGlobalChoiceOption
Aliases: OptionSet, OptionSetLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
The logicalname of the table containing the choice column to update

```yaml
Type: System.String
Parameter Sets: SetAttributeChoiceOption
Aliases: EntityLogicalName, LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Value
The value of the choice option to update. If the value doesn't exist a new choice option is added.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseChoiceOption.md)


