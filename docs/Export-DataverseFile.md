---
external help file: AMSoftware.Dataverse.PowerShell-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Export-DataverseFile

## SYNOPSIS
Export the content of a File or Image column

## SYNTAX

```
Export-DataverseFile [-Table] <String> [-Row] <Guid> [-Column] <String> 
 [<CommonParameters>]
```

## DESCRIPTION
Export the content of a File or Image column. 
In case of an Image Column this only works when Full Image is enabled, as this stores the full image in File Storage.
For Image Columns the thumbnail is stored as content in the Field.

## EXAMPLES

## PARAMETERS

### -Column
The LogicalName of the File or Image Column

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Row
The Id of the Row to get the File or Image for

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Id

Required: True
Position: 1
Default value: None
Accept pipeline input: True (ByPropertyName, ByValue)
Accept wildcard characters: False
```

### -Table
The LogicalName of the Table to get the File or Image for

```yaml
Type: System.String
Parameter Sets: (All)
Aliases: LogicalName

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

### System.Guid

## OUTPUTS

### System.Byte[]

## NOTES

The Function returns the content as a Byte[] which can be saved to disk using Set-Content

## RELATED LINKS


[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Export-DataverseFile.md)


