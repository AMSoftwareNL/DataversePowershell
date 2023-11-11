using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRow", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    [OutputType(typeof(EntityReference))]
    public sealed class RemoveRowCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        protected override void Execute()
        {
            EntityReference rowReference = new EntityReference(Table, Id);

            if (ShouldProcess($"{Table}: {Id}"))
            {
                OrganizationRequest request = new DeleteRequest()
                {
                    Target = rowReference
                };

                if (UseBatch)
                {
                    AddOrganizationRequestToBatch(request);
                }
                else
                {
                    var response = ExecuteOrganizationRequest<DeleteResponse>(request);
                }
            }
        }
    }
}
