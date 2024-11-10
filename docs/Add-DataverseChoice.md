---
external help file: AMSoftware.Dataverse.PowerShell.dll-Help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Add-DataverseChoice

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### AddChoiceObject (Default)
```
Add-DataverseChoice -InputObject <OptionSetMetadata>  
    
  [<RequestParameters>] [<CommonParameters>]
```

### AddNewChoice
```
Add-DataverseChoice -Name <String> -DisplayName <String> [-Description <String>] [-ExternalName <String>]
 -Options <OptionMetadata[]>   
   
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

### -Description
{{ Fill Description Description }}

```yaml
Type: System.String
Parameter Sets: AddNewChoice
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
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ExternalName
{{ Fill ExternalName Description }}

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InputObject
{{ Fill InputObject Description }}

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.OptionSetMetadata
Parameter Sets: AddChoiceObject
Aliases: Choice, OptionSet

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
{{ Fill Name Description }}

```yaml
Type: System.String
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Options
{{ Fill Options Description }}

```yaml
Type: Microsoft.Xrm.Sdk.Metadata.OptionMetadata[]
Parameter Sets: AddNewChoice
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### RequestParameters
This cmdlet supports the request parameters: -Solution, -SharedTag, -Partition, -FailOnDuplicateDetection, -BypassSynchronousLogic, and -BypassPowerAutomateFlows. For more information, see about_DataverseRequestParameters.

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionMetadata[]
## OUTPUTS

### Microsoft.Xrm.Sdk.Metadata.OptionSetMetadataBase
## NOTES

## RELATED LINKS

