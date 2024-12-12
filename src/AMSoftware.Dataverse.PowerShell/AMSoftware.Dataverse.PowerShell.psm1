[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSAvoidUsingWriteHost', '')]
param()

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.psm1-help.xml
function Export-DataverseFile {
    [CmdletBinding()]
    [OutputType([byte[]])]
    param (
        [Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('LogicalName')]
        [string]$Table,

        [Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
        [Alias('Id')]
        [guid]$Row,

        [Parameter(Mandatory = $true)]
        [string]$Column
    )
    
    process {
        # Initialize the download
        $initDownloadResponse = Send-DataverseRequest `
            -Name 'InitializeFileBlocksDownload' `
            -TargetTable $Table `
            -TargetRow $Row `
            -Parameters @{
            FileAttributeName = $Column; 
        }
    
        [long]$offset = 0
        [long]$blockSizeDownload = 4 * 1024 * 1024

        # If chunking is not supported, chunk size will be full size of the file.
        if (-not $initDownloadResponse.IsChunkingSupported) {
            $blockSizeDownload = $initDownloadResponse.FileSizeInBytes
        }

        # File size may be smaller than defined block size
        if ($initDownloadResponse.FileSizeInBytes -lt $blockSizeDownload) {
            $blockSizeDownload = $initDownloadResponse.FileSizeInBytes
        }

        # Download Chunk until $initDownloadResponse.FileSizeInBytes is reached
        $remaining = $initDownloadResponse.FileSizeInBytes
        $filebytes = New-Object -TypeName byte[] -Args $initDownloadResponse.FileSizeInByte

        while ($remaining -gt 0) {
            $downloadResponse = Send-DataverseRequest -Name 'DownloadBlock' -Parameters @{
                BlockLength           = $blockSizeDownload;
                FileContinuationToken = $initDownloadResponse.FileContinuationToken;
                Offset                = $offset;
            }

            Write-Progress `
                -Id 2321484 `
                -Activity $MyInvocation.MyCommand.Name `
                -Status "$($initDownloadResponse.FileSizeInByte - $remaining) of $($initDownloadResponse.FileSizeInByte)" `
                -PercentComplete (($initDownloadResponse.FileSizeInByte - $remaining) / $initDownloadResponse.FileSizeInByte) * 100

            [System.Buffer]::BlockCopy($downloadResponse.Data, 0, $filebytes, $offset, [System.Buffer]::ByteLength($downloadResponse.Data))

            $remaining -= $blockSizeDownload
            $offset += $blockSizeDownload
        }

        Write-Progress -Id 2321484 -Completed
        Write-Output -InputObject $filebytes
    }
}

# .EXTERNALHELP AMSoftware.Dataverse.PowerShell.psm1-help.xml
function Import-DataverseFile {
    [CmdletBinding()]
    [OutputType([guid])]
    param(
        [Parameter(Mandatory = $true)]
        [string]$Table,

        [Parameter(Mandatory = $true)]
        [Alias('Id')]
        [guid]$Row,

        [Parameter(Mandatory = $true)]
        [string]$Column,

        [Parameter(Mandatory = $true)]
        [System.IO.FileInfo]$File
    )

    process {
        # Initialize the upload
        $initUploadResponse = Send-DataverseRequest `
            -Name 'InitializeFileBlocksUpload' `
            -TargetTable $Table `
            -TargetRow $Row `
            -Parameters @{
            FileAttributeName = $Column;
            FileName          = $File.Name;
        }

        $blockSize = 4 * 1024 * 1024
        $buffer = New-Object -TypeName byte[] -Args $blockSize
        $bytesRead = 0
        $blockIds = @()

        try {
            $uploadFileStream = $File.OpenRead()

            # While there is unread data from the file
            while (($bytesRead = $uploadFileStream.Read($buffer, 0, $buffer.Length)) -gt 0) {
                if ($bytesRead -lt $buffer.Length) {
                    [System.Array]::Resize($buffer, $bytesRead)
                }

                $blockId = [System.Convert]::ToBase64String([guid]::NewGuid().ToByteArray())
                $blockIds.Add($blockId);

                Send-DataverseRequest -Name 'UploadBlock' -Parameters @{
                    BlockData             = $buffer;
                    BlockId               = $blockId;
                    FileContinuationToken = $initUploadResponse.FileContinuationToken;
                } | Out-Null
            }

            # Commit the upload
            $commitUploadResponse = Send-DataverseRequest -Name 'CommitFileBlocksUpload' -Parameters @{
                BlockList             = $blockIds.ToArray();
                FileContinuationToken = $initUploadResponse.FileContinuationToken;
                FileName              = $File.Name;
                MimeType              = 'application/octet-stream';
            }
        }
        finally {
            $uploadFileStream.Dispose();
        }

        Write-Output $commitUploadResponse.FileId
    }
}

New-Variable -Name DataverseSession
New-Variable -Name DataverseClient

[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvmoney', [Microsoft.Xrm.Sdk.Money])
[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvchoicevalue', [Microsoft.Xrm.Sdk.OptionSetValue])
[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvrow', [Microsoft.Xrm.Sdk.Entity])
[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvreference', [Microsoft.Xrm.Sdk.EntityReference])
[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvlabel', [Microsoft.Xrm.Sdk.Label])
[psobject].Assembly.GetType('System.Management.Automation.TypeAccelerators')::Add('dvchoiceoption', [Microsoft.Xrm.Sdk.Metadata.OptionMetadata])

Export-ModuleMember -Function '*' -Variable 'DataverseSession', 'DataverseClient'

Write-Host @'
PowerShell Module for Power Platform Dataverse
Copyright (C) 2024 AMSoftwareNL

This program comes with ABSOLUTELY NO WARRANTY;
This is free software, and you are welcome to redistribute it under certain conditions;
'@
