New-Variable -Name DataverseSession
New-Variable -Name DataverseClient

[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvmoney', [Microsoft.Xrm.Sdk.Money])
[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvchoicevalue', [Microsoft.Xrm.Sdk.OptionSetValue])
[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvrow', [Microsoft.Xrm.Sdk.Entity])
[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvreference', [Microsoft.Xrm.Sdk.EntityReference])
[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvlabel', [Microsoft.Xrm.Sdk.Label])
[psobject].Assembly.GetType("System.Management.Automation.TypeAccelerators")::Add('dvchoiceoption', [Microsoft.Xrm.Sdk.Metadata.OptionMetadata])

Export-ModuleMember -Function '*' -Variable 'DataverseSession','DataverseClient'