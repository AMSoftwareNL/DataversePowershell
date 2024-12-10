---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseTableKey

## SYNOPSIS
Add a table key

## SYNTAX

```
Add-DataverseTableKey -Table <String> -Name <String> -DisplayName <String> -Columns <String[]>
  [<CommonParameters>]
```

## DESCRIPTION
Add a table key

## EXAMPLES

### Example 1
```powershell
PS C:\> Add-DataverseTableKey -Table 'account' -Name 'ams_accountnumber' -DisplayName 'Accountnumber' -Columns 'accountnumber'
```

## PARAMETERS

### -Columns
The logicalnames of the column that build the key

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases: Attributes

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
Displayname for the table key

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

### -Name
Logicalname for the table key

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

### -Table
Logicalname of the table to add the key to

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: EntityLogicalName, LogicalName

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
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseTableKey.md)


