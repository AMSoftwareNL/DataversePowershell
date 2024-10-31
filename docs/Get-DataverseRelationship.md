---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRelationship

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### GetRelationshipForTable (Default)
```
Get-DataverseRelationship -Table <String> [-RelatedTable <String>] [-Type <RelationshipType>]
 [-Include <String>] [-Exclude <String>] [-Custom] [-Unmanaged] [-Solution <String>] [-SharedTag <String>]
 [-Partition <String>] [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### GetRelationshipById
```
Get-DataverseRelationship -Id <Guid> [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### GetRelationshipByName
```
Get-DataverseRelationship -Name <String> [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
 [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -BypassPowerAutomateFlows
{{ Fill BypassPowerAutomateFlows Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -BypassSynchronousLogic
{{ Fill BypassSynchronousLogic Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Custom
{{ Fill Custom Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetRelationshipForTable
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
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -FailOnDuplicateDetection
{{ Fill FailOnDuplicateDetection Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid
Parameter Sets: GetRelationshipById
Aliases: MetadataId

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Include
{{ Fill Include Description }}

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: True
```

### -Name
{{ Fill Name Description }}

```yaml
Type: System.String
Parameter Sets: GetRelationshipByName
Aliases: SchemaName

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Partition
{{ Fill Partition Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: System.Management.Automation.ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RelatedTable
{{ Fill RelatedTable Description }}

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SharedTag
{{ Fill SharedTag Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Solution
{{ Fill Solution Description }}

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
{{ Fill Table Description }}

```yaml
Type: System.String
Parameter Sets: GetRelationshipForTable
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Type
{{ Fill Type Description }}

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.RelationshipType
Parameter Sets: GetRelationshipForTable
Aliases:
Accepted values: OneToManyRelationship, Default, ManyToManyRelationship

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Unmanaged
{{ Fill Unmanaged Description }}

```yaml
Type: System.Management.Automation.SwitchParameter
Parameter Sets: GetRelationshipForTable
Aliases:

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

### System.String

## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.RelationshipMetadataBase

## NOTES

## RELATED LINKS
