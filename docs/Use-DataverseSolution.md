---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Use-DataverseSolution

## SYNOPSIS
Sets the active solution for the current session.

## SYNTAX

```
Use-DataverseSolution -Name <String>  [<CommonParameters>]
```

## DESCRIPTION
Sets the active unmanaged solution for the current session. Solution aware customization are added to this solution.
Is combined with the Optional Request Parameter 'Solution'.
If solution is set in the session and provided as a request parameter, the request parameter is used.

## EXAMPLES

### Example 1
```
PS C:\> Use-DataverseSolution -Name 'myunmanagedsolution'
```

Sets 'myunmanagedsolution' as the solution for solution-aware customizations.

## PARAMETERS

### -Name
Uniquename of the Unmanaged Solution

```yaml
Type: System.String
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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Use-DataverseSolution.md)



