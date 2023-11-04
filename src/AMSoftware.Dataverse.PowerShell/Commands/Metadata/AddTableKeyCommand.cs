using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseTableKey")]
    [OutputType(typeof(EntityKeyMetadata))]
    public sealed class AddTableKeyCommand : CmdletBase
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Attributes")]
        [ValidateNotNullOrEmpty]
        [ValidateCount(1, int.MaxValue)]
        public string[] Columns { get; set; }

        protected override void Execute()
        {
            EntityKeyMetadata key = new EntityKeyMetadata()
            {
                LogicalName = Name,
                SchemaName = Name,
                DisplayName = new Label(DisplayName, 1033),
                KeyAttributes = Columns
            };

            var createRequest = new CreateEntityKeyRequest
            {
                EntityName = Table,
                EntityKey = key,
                SolutionUniqueName = null
            };

            var createResponse = (CreateEntityKeyResponse)Session.Current.Client.ExecuteOrganizationRequest(
                createRequest, MyInvocation.MyCommand.Name);

            var keyMetadataId = createResponse.EntityKeyId;

            var getByIdRequest = new RetrieveEntityKeyRequest()
            {
                MetadataId = keyMetadataId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = (RetrieveEntityKeyResponse)Session.Current.Client.ExecuteOrganizationRequest(
                getByIdRequest, MyInvocation.MyCommand.Name);

            WriteObject(getByIdResponse.EntityKeyMetadata);
        }
    }
}
