---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseTableKey

## SYNOPSIS
Retrieve table key metadata

## SYNTAX

```
Get-DataverseTableKey -Table <String> [-Name <String>] [-Exclude <String>] [-ExcludeManaged]
 [-Columns <String[]>]  [<CommonParameters>]
```

## DESCRIPTION
Retrieve table key metadata

## EXAMPLES

## PARAMETERS

### -Columns
Logicalnames of columns in the key

```yaml
Type: System.String[]
Parameter Sets: (All)
Aliases: Attributes

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exclude
Filter of key to exclude based on the logicalname. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -ExcludeManaged
Exclude managed keys

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The logicalname of the key to retrieve. Can contain wildcards.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: Include

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Table
The logicalname of the table to retrieve keys for

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: LogicalName, EntityLogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
### System.String[]
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityKeyMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Get-DataverseTableKey.md)


