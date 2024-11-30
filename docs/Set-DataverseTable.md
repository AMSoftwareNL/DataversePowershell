---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseTable

## SYNOPSIS
Update a table

## SYNTAX

### SetTableObject
```
Set-DataverseTable -InputObject <EntityMetadata> [-MergeLabels] 
 [<CommonParameters>]
```

### SetTable
```
Set-DataverseTable -Name <String> [-DisplayName <String>] [-PluralName <String>] [-Description <String>]
 [-ExternalName <String>] [-ExternalPluralName <String>] [-HasAttachments] [-IsActivityParty] [-TrackChanges]
 [-DataProviderId <Guid>] [-DataSourceId <Guid>] [-MergeLabels] 
 [<CommonParameters>]
```

## DESCRIPTION
Update a table

## PARAMETERS

### -DataProviderId
The Id that identifies the associated virtual table data provider

```yaml
Type: System.Guid
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DataSourceId
The Id that identifies the associated virtual table data source

```yaml
Type: System.Guid
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Description
New description for the table

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisplayName
New displayname for the table

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
New external name for the table

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalPluralName
New plural external name for the table

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -HasAttachments
Can the table have attachments

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
Entity Metadata object describing the updated table

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.EntityMetadata
Parameter Sets: SetTableObject
Aliases: Entity

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IsActivityParty
Table can be used as party in an activity

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### -MergeLabels
Keep other language labels for DisplayName and Description

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
The logicalname of the table to update

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -PluralName
The new plural name for the table

```yaml
Type: System.String
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TrackChanges
Track change is enabled

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: SetTable
Aliases:

Required: False
Position: Named
Default value: False
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata
## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Set-DataverseTable.md)
