---
external help file: AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRole

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### GetAllRoles (Default)
```
Get-DataverseRole [-Name <String>] [-Exclude <String>] [-Inheritance <RoleInheritance>]
  [<CommonParameters>]
```

### GetRoleById
```
Get-DataverseRole -Id <Guid>  [<CommonParameters>]
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

### -Exclude
{{ Fill Exclude Description }}

```yaml
Type: System.String
Parameter Sets: GetAllRoles
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid
Parameter Sets: GetRoleById
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Inheritance
{{ Fill Inheritance Description }}

```yaml
Type: RoleInheritance
Parameter Sets: GetAllRoles
Aliases:
Accepted values: TeamOnly, DirectAndTeam

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

```yaml
Type: System.String
Parameter Sets: GetAllRoles
Aliases: Include

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseRole.md)
