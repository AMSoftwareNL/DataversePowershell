---
external help file: AMSoftware.Dataverse.PowerShell.AccessManagement.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseUser

## SYNOPSIS
Get available users in Dataverse

## SYNTAX

### GetAllUsers (Default)
```
Get-DataverseUser [-Name <String>] [-Exclude <String>] [-Disabled] [-Licensed] [-Application]
  [<CommonParameters>]
```

### GetUserById
```
Get-DataverseUser -Id <Guid>  [<CommonParameters>]
```

## DESCRIPTION
Get available users in Dataverse based on domainname, name, or emailaddress.

## EXAMPLES

## PARAMETERS

### -Application
Include Application Users in the result

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetAllUsers
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Disabled
Include Disabled Users in the result

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetAllUsers
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Users to exclude from the result

```yaml
Type: System.String
Parameter Sets: GetAllUsers
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Id
Id of the specific User to retrieve

```yaml
Type: System.Guid
Parameter Sets: GetUserById
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Licensed
Only return licensed users

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetAllUsers
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the user to retrieve. Supports wildcards.

```yaml
Type: System.String
Parameter Sets: GetAllUsers
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

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseUser.md)



