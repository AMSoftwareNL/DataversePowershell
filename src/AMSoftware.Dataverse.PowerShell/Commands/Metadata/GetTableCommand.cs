using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseTable", DefaultParameterSetName = GetTablesByFilterParameterSet)]
    [OutputType(typeof(EntityMetadata))]
    public sealed class GetTableCommand : CmdletBase
    {
        private const string GetTableByIdParameterSet = "GetTableById";
        private const string GetTableByEtcParameterSet = "GetTableByEtc";
        private const string GetTablesByFilterParameterSet = "GetTablesByFilter";

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = GetTableByIdParameterSet, ValueFromPipeline = true)]
        [Alias("MetadataId")]
        [ValidateNotNull]
        public Guid Id { get; set; }

        [Parameter(Position = 1, Mandatory = true, ParameterSetName = GetTableByEtcParameterSet)]
        [Alias("ObjectTypeCode", "EntityTypeCode")]
        [ValidateNotNull]
        public int TypeCode { get; set; }

        [Parameter(Position = 1, ParameterSetName = GetTablesByFilterParameterSet)]
        [Alias("Include", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Name { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Exclude { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        [ValidateNotNullOrEmpty]
        public TableType Type { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Custom { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Unmanaged { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Intersects { get; set; }

        IEnumerable<EntityMetadata> _entitiesMetadata;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            switch (ParameterSetName)
            {
                case GetTableByEtcParameterSet:
                case GetTablesByFilterParameterSet:
                    var request = new RetrieveAllEntitiesRequest()
                    {
                        EntityFilters = EntityFilters.Entity,
                        RetrieveAsIfPublished = true
                    };
                    var response = (RetrieveAllEntitiesResponse)Session.Current.Client.ExecuteOrganizationRequest(
                        request, MyInvocation.MyCommand.Name);

                    _entitiesMetadata = response.EntityMetadata.AsEnumerable();

                    break;
            }
        }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case GetTableByIdParameterSet:
                    var getByIdRequest = new RetrieveEntityRequest()
                    {
                        EntityFilters = EntityFilters.Entity,
                        MetadataId = Id,
                        RetrieveAsIfPublished = true
                    };
                    var getByIdResponse = (RetrieveEntityResponse)Session.Current.Client.ExecuteOrganizationRequest(
                        getByIdRequest, MyInvocation.MyCommand.Name);

                    WriteObject(getByIdResponse.EntityMetadata);

                    break;
                case GetTableByEtcParameterSet:
                    WriteObject(_entitiesMetadata.FirstOrDefault(e => e.ObjectTypeCode == TypeCode));
                    break;
                case GetTablesByFilterParameterSet:
                    IEnumerable<EntityMetadata> result = _entitiesMetadata.ToList();

                    if (!string.IsNullOrWhiteSpace(Name))
                    {
                        WildcardPattern includePattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);
                        result = result.Where(e => includePattern.IsMatch(e.LogicalName));
                    }
                    if (!string.IsNullOrWhiteSpace(Exclude))
                    {
                        WildcardPattern excludePattern = new WildcardPattern(Exclude, WildcardOptions.IgnoreCase);
                        result = result.Where(e => !excludePattern.IsMatch(e.LogicalName));
                    }

                    if (Custom.IsPresent) result = result.Where(e => e.IsCustomEntity == true);
                    if (Unmanaged.IsPresent) result = result.Where(e => e.IsManaged == false);
                    if (Intersects.IsPresent) result = result.Where(e => e.IsIntersect == true);

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Type)))
                    {
                        switch (Type)
                        {
                            case TableType.Standard:
                                result = result.Where(e => e.TableType == TableTypeCode.Standard.ToString());
                                break;
                            case TableType.Activity:
                                result = result.Where(e => e.IsActivity == true);
                                break;
                            case TableType.Virtual:
                                result = result.Where(e => e.TableType == TableTypeCode.Virtual.ToString());
                                break;
                            case TableType.Datasource:
                                result = result.Where(e => e.DataProviderId != null);
                                break;
                            case TableType.Elastic:
                                result = result.Where(e => e.TableType == TableTypeCode.Elastic.ToString());
                                break;
                        }
                    }

                    WriteObject(result.OrderBy(e => e.LogicalName).ToList(), true);

                    break;
            }
        }
    }

    public enum TableType
    {
        Standard,
        Activity,
        Virtual,
        Datasource,
        Elastic,
    }
}
