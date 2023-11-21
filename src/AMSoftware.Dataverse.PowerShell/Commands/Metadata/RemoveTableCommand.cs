using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseTable", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveTableCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName", "TableName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Name { get; set; }

        protected override void Execute()
        {
            var entityLogicalName = Name;

            if (ShouldProcess(entityLogicalName))
            {
                OrganizationRequest request = new DeleteEntityRequest()
                {
                    LogicalName = entityLogicalName
                };

                DeleteEntityResponse response = ExecuteOrganizationRequest<DeleteEntityResponse>(request);
            }
        }
    }
}
