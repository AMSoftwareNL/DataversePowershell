---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Use-DataverseLanguage

## SYNOPSIS
Set the language for the current user

## SYNTAX

```
Use-DataverseLanguage -Language <Int32>  [<CommonParameters>]
```

## DESCRIPTION
Set the language for the current user. This language is used with new or updated labels, and formatted values.
The language is set in the User Settings and applied to the current PowerShell session.

## EXAMPLES

### Example 1
```
PS C:\> Use-DataverseLanguage -Language 1043
```

Set session language to Dutch

## PARAMETERS

### -Language
LCID of the language. For example 1043 = Dutch, 1033 = English.

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

### AMSoftware.Dataverse.PowerShell.Session
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Use-DataverseLanguage.md)
