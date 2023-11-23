using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseRelationship", DefaultParameterSetName = GetRelationshipForTableParameterSet)]
    [OutputType(typeof(RelationshipMetadataBase))]
    public sealed class GetRelationshipCommand : RequestCmdletBase
    {
        private const string GetRelationshipByNameParameterSet = "GetRelationshipByName";
        private const string GetRelationshipForTableParameterSet = "GetRelationshipForTable";
        private const string GetRelationshipByIdParameterSet = "GetRelationshipById";

        [Parameter(Mandatory = true, ParameterSetName = GetRelationshipByIdParameterSet, ValueFromPipeline = true)]
        [Alias("MetadataId")]
        [ValidateNotNull]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetRelationshipByNameParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [Alias("SchemaName")]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetRelationshipForTableParameterSet)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        public RelationshipType Type { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Include { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Exclude { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        public SwitchParameter Custom { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetRelationshipForTableParameterSet)]
        public SwitchParameter Unmanaged { get; set; }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case GetRelationshipByIdParameterSet:
                    var retrieveByIdRequest = new RetrieveRelationshipRequest()
                    {
                        MetadataId = Id
                    };
                    var retrieveByIdResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(retrieveByIdRequest);

                    WriteObject(retrieveByIdResponse.RelationshipMetadata);

                    break;
                case GetRelationshipByNameParameterSet:
                    var retrieveByNameRequest = new RetrieveRelationshipRequest()
                    {
                        Name = Name
                    };
                    var retrieveByNameResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(retrieveByNameRequest);

                    WriteObject(retrieveByNameResponse.RelationshipMetadata);

                    break;
                case GetRelationshipForTableParameterSet:
                    var getTableRequest = new RetrieveEntityRequest()
                    {
                        EntityFilters = EntityFilters.Relationships,
                        LogicalName = Table,
                        RetrieveAsIfPublished = true
                    };
                    var getTableResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getTableRequest);

                    var relationships = new List<RelationshipMetadataBase>();
                    if (getTableResponse.EntityMetadata.OneToManyRelationships != null)
                        relationships.AddRange(getTableResponse.EntityMetadata.OneToManyRelationships);
                    if (getTableResponse.EntityMetadata.ManyToOneRelationships != null)
                        relationships.AddRange(getTableResponse.EntityMetadata.ManyToOneRelationships);
                    if (getTableResponse.EntityMetadata.ManyToManyRelationships != null)
                        relationships.AddRange(getTableResponse.EntityMetadata.ManyToManyRelationships);

                    var result = relationships.AsEnumerable();

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(RelatedTable)))
                    {
                        result = result.Where(r =>
                            r is OneToManyRelationshipMetadata oneToManyRelationship && (
                                oneToManyRelationship.ReferencedEntity.Equals(RelatedTable, StringComparison.OrdinalIgnoreCase) ||
                                oneToManyRelationship.ReferencingEntity.Equals(RelatedTable, StringComparison.OrdinalIgnoreCase)));

                        result = result.Where(r =>
                            r is ManyToManyRelationshipMetadata manyToManyRelationship && (
                                manyToManyRelationship.Entity1LogicalName.Equals(RelatedTable, StringComparison.OrdinalIgnoreCase) ||
                                manyToManyRelationship.Entity2LogicalName.Equals(RelatedTable, StringComparison.OrdinalIgnoreCase)));
                    }

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Include)))
                    {
                        WildcardPattern includePattern = new WildcardPattern(Include, WildcardOptions.IgnoreCase);
                        result = result.Where(r => includePattern.IsMatch(r.SchemaName));
                    }

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Exclude)))
                    {
                        WildcardPattern excludePattern = new WildcardPattern(Exclude, WildcardOptions.IgnoreCase);
                        result = result.Where(r => !excludePattern.IsMatch(r.SchemaName));
                    }

                    if (Custom.IsPresent) result = result.Where(r => r.IsCustomRelationship == true);
                    if (Unmanaged.IsPresent) result = result.Where(r => r.IsManaged == false);

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Type)))
                        result = result.Where(r => r.RelationshipType == Type);

                    WriteObject(result.OrderBy(r => r.SchemaName).ToList(), true);

                    break;
            }
        }
    }
}
