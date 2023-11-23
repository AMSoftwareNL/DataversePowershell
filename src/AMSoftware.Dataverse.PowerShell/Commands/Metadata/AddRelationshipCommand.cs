using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseRelationhip")]
    [OutputType(typeof(ManyToManyRelationshipMetadata))]
    public sealed class AddRelationshipCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Intersect { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Searchable { get; set; }

        [Parameter(Mandatory = false)]
        public AssociatedMenuConfiguration MenuConfiguration { get; set; }

        [Parameter(Mandatory = false)]
        public AssociatedMenuConfiguration RelatedMenuConfiguration { get; set; }

        protected override void Execute()
        {
            var manyToManyRequest = BuildManyToManyRequest();
            var manyToManyResponse = ExecuteOrganizationRequest<CreateManyToManyResponse>(manyToManyRequest);

            var getByIdRequest = new RetrieveRelationshipRequest()
            {
                MetadataId = manyToManyResponse.ManyToManyRelationshipId
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(getByIdRequest);

            WriteObject(getByIdResponse.RelationshipMetadata);
        }

        private CreateManyToManyRequest BuildManyToManyRequest()
        {
            var request = new CreateManyToManyRequest()
            {
                IntersectEntitySchemaName = Intersect,
                ManyToManyRelationship = new ManyToManyRelationshipMetadata()
                {
                    SchemaName = Name,
                    Entity1LogicalName = Table,
                    Entity2LogicalName = RelatedTable,
                    IsValidForAdvancedFind = Searchable.ToBool()
                }
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(MenuConfiguration)))
                request.ManyToManyRelationship.Entity1AssociatedMenuConfiguration = MenuConfiguration;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(RelatedMenuConfiguration)))
                request.ManyToManyRelationship.Entity2AssociatedMenuConfiguration = RelatedMenuConfiguration;

            return request;
        }
    }
}
