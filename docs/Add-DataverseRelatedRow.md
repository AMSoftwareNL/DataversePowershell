---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseRelatedRow

## SYNOPSIS
Relate rows in a many-to-many relationship

## SYNTAX

```
Add-DataverseRelatedRow -TargetTable <String> -TargetRow <Guid> -RelatedTable <String> -RelatedRow <Guid>
 -Relationship <String> [-BatchId <Guid>]  [<CommonParameters>]
```

## DESCRIPTION
Relate rows in a many-to-many relationship

## EXAMPLES

### Example 1
```
PS C:\> Get-DataverseRows -Table 'contact' | Add-DataverseRelatedRow -TargetTable 'account' -TargetRow '6d7a8eda-4013-4705-813c-3a0ac8eb93ef' -Relationship 'ams_accounts_contacts'
```

Use pipeline to add multiple rows at once to the target.

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

### -RelatedRow
The Id of the related row to add

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Id

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -RelatedTable
The Table of the related row to add

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Relationship
The schemaname of the many-to-many relationship that applies

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

### -TargetRow
The Id of the row to add the related rows to

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetTable
The Table of the row to add the related rows to

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

### System.String
### System.Guid
## OUTPUTS

### System.Object
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Add-DataverseRelatedRow.md)


