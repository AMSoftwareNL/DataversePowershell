using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseTableKey")]
    [OutputType(typeof(EntityKeyMetadata))]
    public sealed class AddTableKeyCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
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
                DisplayName = new Label(DisplayName, Session.Current.LanguageId),
                KeyAttributes = Columns
            };

            var createRequest = new CreateEntityKeyRequest
            {
                EntityName = Table,
                EntityKey = key
            };

            var createResponse = ExecuteOrganizationRequest<CreateEntityKeyResponse>(createRequest);

            var keyMetadataId = createResponse.EntityKeyId;

            var getByIdRequest = new RetrieveEntityKeyRequest()
            {
                MetadataId = keyMetadataId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveEntityKeyResponse>(getByIdRequest);

            WriteObject(getByIdResponse.EntityKeyMetadata);
        }
    }
}
