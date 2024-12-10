---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Send-DataverseRequest

## SYNOPSIS
Execute a request

## SYNTAX

### Message (Default)
```
Send-DataverseRequest -Name <String> [-Parameters <Hashtable>] [-BatchId <Guid>]
  [<CommonParameters>]
```

### Function
```
Send-DataverseRequest -Name <String> [-Parameters <Hashtable>] -TargetTable <String> -TargetRow <Guid>
 [-BatchId <Guid>]  [<CommonParameters>]
```

## DESCRIPTION
Execute a request, such as an action, function, or custom API.

## EXAMPLES

### Example 1: Simple request

```powershell
Send-DataverseRequest -Name 'WhoAmI'
```

### Example 2: Request with parameters

```powershell
Send-DataverseRequest -Name 'CopySystemForm' -Parameters @{SourceId=$randomform.Id}
```

### Example 3: Request bound to Target

```powershell
$row = Get-DataverseRows -Table 'account' -Top 1
$row | Send-DataverseRequest -Name 'GenerateSharedLink' -Parameters @{SharedRights=[Microsoft.Crm.Sdk.Messages.AccessRights]::ReadAccess}
```

## PARAMETERS

### -BatchId
{{ Fill BatchId Description }}

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the request to execute

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: Request

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Parameters
Input parameters to provide with the request

```yaml
Type: System.Collections.Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetRow
For bound actions the Id of the table row to invoke the action on

```yaml
Type: System.Guid
Parameter Sets: Function
Aliases: Id

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -TargetTable
For bound actions the logicalname of the table to invoke the action on

```yaml
Type: System.String
Parameter Sets: Function
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
### System.Guid
## OUTPUTS

### Microsoft.Xrm.Sdk.ParameterCollection
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Send-DataverseRequest.md)


