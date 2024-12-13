---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Export-DataverseTranslation

## SYNOPSIS
Export the translations for customizations

## SYNTAX

```
Export-DataverseTranslation [-SolutionName] <String> [-OutputPath] <String>
  [<CommonParameters>]
```

## DESCRIPTION
Export the translations for customizations

## EXAMPLES

## PARAMETERS

### -OutputPath
The folder path for the exported translations. The filename is determined from the solution.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SolutionName
Uniquename of the solution to export the translations for

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: Name

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

## OUTPUTS

### System.IO.FileInfo

## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Export-DataverseTranslation.md)





