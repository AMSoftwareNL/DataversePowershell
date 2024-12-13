---
external help file: AMSoftware.Dataverse.PowerShell.Development.psm1-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Export-DataverseWebResource

## SYNOPSIS
Export the content of a WebResource

## SYNTAX

```
Export-DataverseWebResource [-Id] <Guid> [-OutputPath] <String> 
 [<CommonParameters>]
```

## DESCRIPTION
Export the content of a WebResource to a file.

## EXAMPLES

## PARAMETERS

### -Id
Id of the WebResource to export

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
Path of the folder to export the WebResource to. The Filename for the exported WebResource is set from the name of the WebResource. 

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

The name of the WebResource determines the name of the export file. In case the webresource name contains invalid path characters these are replaced by '-'. The URL folder structure of the webresource is not maintained on export, but included in the filename.

## RELATED LINKS

[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Export-DataverseWebResource.md)





