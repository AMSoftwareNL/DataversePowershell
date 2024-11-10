---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Set-DataverseTable

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### SetTableObject
```
Set-DataverseTable -InputObject <EntityMetadata> [-MergeLabels]  
    
  [<RequestParameters>] [<CommonParameters>]
```

### SetTable
```
Set-DataverseTable -Name <String> [-DisplayName <String>] [-PluralName <String>] [-Description <String>]
 [-ExternalName <String>] [-ExternalPluralName <String>] [-HasAttachments] [-IsActivityParty] [-TrackChanges]
 [-DataProviderId <Guid>] [-DataSourceId <Guid>] [-MergeLabels]  
    
  [<RequestParameters>] [<CommonParameters>]
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

### -DataProviderId
{{ Fill DataProviderId Description }}

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
{{ Fill DataSourceId Description }}

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
{{ Fill Description Description }}

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
{{ Fill DisplayName Description }}

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
{{ Fill ExternalName Description }}

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
{{ Fill ExternalPluralName Description }}

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
{{ Fill HasAttachments Description }}

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
{{ Fill InputObject Description }}

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
{{ Fill IsActivityParty Description }}

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
{{ Fill MergeLabels Description }}

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
{{ Fill Name Description }}

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
{{ Fill PluralName Description }}

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
{{ Fill TrackChanges Description }}

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

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.EntityMetadata
## NOTES

## RELATED LINKS

