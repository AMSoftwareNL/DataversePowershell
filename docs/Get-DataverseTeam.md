---
external help file: AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseTeam

## SYNOPSIS
Retrieve Teams

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
Retrieve Teams

## EXAMPLES


## PARAMETERS

### -Administrator
Id of the systemuser who is the administrator for the Team

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
ID of the Business Unit to retrieve Teams for

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
Teams to exclude from the result. Supports wildcards.

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
Id of the specific Team to retrieve

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
Name of the Team to retrieve. Supports Wildcards.

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
The type of Team to retrieve

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



