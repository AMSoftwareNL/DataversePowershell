---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseChoice

## SYNOPSIS
Retrieve the metadata of global choices

## SYNTAX

### GetChoiceByName (Default)
```
Get-DataverseChoice [-Name <String>] [-Exclude <String>] [-Custom] [-Unmanaged]
  [<CommonParameters>]
```

### GetChoiceById
```
Get-DataverseChoice -Id <Guid>  [<CommonParameters>]
```

## DESCRIPTION
Retrieve the metadata of global choices (OptionSet)

## PARAMETERS

### -Custom
Retrieve only custom Choices

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetChoiceByName
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Filter of choices to exclude based on the logicalname. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetChoiceByName
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
MetadataId of the choice to retrieve

```yaml
Type: System.Guid
Parameter Sets: GetChoiceById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
The logicalname of the choice to retrieve. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: GetChoiceByName
Aliases: Include

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: True
```

### -Unmanaged
Only retrieve unmanaged globl choices

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetChoiceByName
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid
### System.String
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionSetMetadataBase
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseChoice.md)
