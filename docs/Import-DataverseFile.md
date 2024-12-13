---
external help file: AMSoftware.Dataverse.PowerShell-help.xml
Module Name: AMSoftware.Dataverse.PowerShell
online version:
schema: 2.0.0
---

# Import-DataverseFile

## SYNOPSIS
Import a local File to an Image or File Column

## SYNTAX

```
Import-DataverseFile [-Table] <String> [-Row] <Guid> [-Column] <String> [-File] <FileInfo>
  [<CommonParameters>]
```

## DESCRIPTION
Import a local file to an Image or File Column

## EXAMPLES


## PARAMETERS

### -Column
LogicalName of the Image or File Column

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

### -File
The FileInfo of the File to import. Output from Get-Item or Get-ChildItem.

```yaml
Type: System.IO.FileInfo
Parameter Sets: (All)
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Row
The Id of the Row to import the content to

```yaml
Type: System.Guid
Parameter Sets: (All)
Aliases: Id

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Table
The Table to import the content to

```yaml
Type: System.String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### None

## OUTPUTS

### System.Guid

## NOTES

## RELATED LINKS


[Online](https://github.com/AMSoftwareNL/DataversePowershell/blob/main/docs/Import-DataverseFile.md)
