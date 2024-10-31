---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Get-DataverseRows

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### RetrieveWithQuery (Default)
```
Get-DataverseRows -Table <String> [-Columns <String[]>] [-Sort <Hashtable>] [-Solution <String>]
 [-SharedTag <String>] [-Partition <String>] [-FailOnDuplicateDetection] [-BypassSynchronousLogic]
 [-BypassPowerAutomateFlows] [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### RetrieveWithAttributeQuery
```
Get-DataverseRows -Table <String> -Query <Hashtable> [-Columns <String[]>] [-Sort <Hashtable>]
 [-Solution <String>] [-SharedTag <String>] [-Partition <String>] [-FailOnDuplicateDetection]
 [-BypassSynchronousLogic] [-BypassPowerAutomateFlows] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### RetrieveWithBatch
```
Get-DataverseRows -Table <String> -Id <Guid[]> [-Columns <String[]>] [-Solution <String>] [-SharedTag <String>]
 [-Partition <String>] [-FailOnDuplicateDetection] [-BypassSynchronousLogic] [-BypassPowerAutomateFlows]
 [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

### RetrieveWithFetchXml
```
Get-DataverseRows -FetchXml <XmlDocument> [-Solution <String>] [-SharedTag <String>] [-Partition <String>]
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

### -Columns
{{ Fill Columns Description }}

```yaml
Type: System.String[]
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery, RetrieveWithBatch
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
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

### -FetchXml
{{ Fill FetchXml Description }}

```yaml
Type: System.Xml.XmlDocument
Parameter Sets: RetrieveWithFetchXml
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Id
{{ Fill Id Description }}

```yaml
Type: System.Guid[]
Parameter Sets: RetrieveWithBatch
Aliases:

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

### -Query
{{ Fill Query Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: RetrieveWithAttributeQuery
Aliases:

Required: True
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

### -Sort
{{ Fill Sort Description }}

```yaml
Type: System.Collections.Hashtable
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery
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
Parameter Sets: RetrieveWithQuery, RetrieveWithAttributeQuery, RetrieveWithBatch
Aliases: LogicalName

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid[]

### System.String[]

### System.Xml.XmlDocument

## OUTPUTS

### Microsoft.Xrm.Sdk.Entity

## NOTES

## RELATED LINKS
