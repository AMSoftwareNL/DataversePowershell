using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseTableKey", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveTableKeyCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("EntityLogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        protected override void Execute()
        {
            if (ShouldProcess($"{Table}: {Name}"))
            {
                var request = new DeleteEntityKeyRequest()
                {
                    EntityLogicalName = Table,
                    Name = Name
                };

                var response = ExecuteOrganizationRequest<DeleteEntityKeyResponse>(request);
            }
        }
    }
}
