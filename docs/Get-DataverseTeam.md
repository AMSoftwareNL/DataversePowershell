---
external help file: AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseTeam

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### GetAllTeams (Default)
```
Get-DataverseTeam [-Name <String>] [-Exclude <String>] [-TeamType <TeamType>] [-BusinessUnit <Guid>]
 [-Administrator <Guid>]  [<CommonParameters>]
```

### GetTeamById
```
Get-DataverseTeam -Id <Guid>  [<CommonParameters>]
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

### -Administrator
{{ Fill Administrator Description }}

```yaml
Type: System.Guid
Parameter Sets: GetAllTeams
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BusinessUnit
{{ Fill BusinessUnit Description }}

```yaml
Type: System.Guid
Parameter Sets: GetAllTeams
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
{{ Fill Exclude Description }}

```yaml
Type: System.String
Parameter Sets: GetAllTeams
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
Parameter Sets: GetTeamById
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

```yaml
Type: System.String
Parameter Sets: GetAllTeams
Aliases: Include

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -TeamType
{{ Fill TeamType Description }}

```yaml
Type: TeamType
Parameter Sets: GetAllTeams
Aliases:
Accepted values: Owner, Access, AADSecurityGroup, AADOfficeGroup

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseTeam.md)


