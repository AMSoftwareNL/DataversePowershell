using Microsoft.Xrm.Sdk.Messages;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRelationship")]
    public sealed class RemoveRelationshipCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("SchemaName")]
        public string Name { get; set; }

        protected override void Execute()
        {
            var request = new DeleteRelationshipRequest()
            {
                Name = Name
            };
            var response = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(request);
        }
    }
}
