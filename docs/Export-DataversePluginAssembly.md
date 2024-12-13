---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Export-DataversePluginAssembly

## SYNOPSIS
Export a Plugin Assembly to a file

## SYNTAX

```
Export-DataversePluginAssembly [-Id] <Guid> [-OutputPath] <String> 
 [<CommonParameters>]
```

## DESCRIPTION
Export a Plugin Assembly to a file

## EXAMPLES


## PARAMETERS

### -Id
Id of the specific Plugin Assembly to export

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -OutputPath
Folder path for the exported assembly. Filename is determined from the Plugin Assembly name.

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.Guid

## OUTPUTS

### System.IO.FileInfo

## NOTES

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Export-DataversePluginAssembly.md)




