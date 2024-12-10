---
external help file: AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseRowOwner

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

```
Set-DataverseRowOwner [-Table] <String> [-Id] <Guid> [-Owner] <EntityReference> [-Force]
  [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -Force
{{ Fill Force Description }}

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

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Row, RowId

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Owner
{{ Fill Owner Description }}

```yaml
Type: Microsoft.Xrm.Sdk.EntityReference
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
{{ Fill Table Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: LogicalName, EntityLogicalName, Entity

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
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
Default value: None
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
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
### System.Guid
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseRowOwner.md)


