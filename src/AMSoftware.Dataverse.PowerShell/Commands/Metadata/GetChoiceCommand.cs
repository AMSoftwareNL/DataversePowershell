using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseChoice", DefaultParameterSetName = GetChoiceByNameParameterSet)]
    [OutputType(typeof(OptionSetMetadataBase))]
    public sealed class GetChoiceCommand : RequestCmdletBase
    {
        private const string GetChoiceByNameParameterSet = "GetChoiceByName";
        private const string GetChoiceByIdParameterSet = "GetChoiceById";

        [Parameter(Mandatory = true, ParameterSetName = GetChoiceByIdParameterSet, ValueFromPipeline = true)]
        [Alias("MetadataId")]
        [ValidateNotNull]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetChoiceByNameParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        [Alias("Include")]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetChoiceByNameParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Exclude { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetChoiceByNameParameterSet)]
        public SwitchParameter Custom { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = GetChoiceByNameParameterSet)]
        public SwitchParameter Unmanaged { get; set; }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case GetChoiceByIdParameterSet:
                    var retrieveByIdRequest = new RetrieveOptionSetRequest()
                    {
                        MetadataId = Id,
                        RetrieveAsIfPublished = true
                    };
                    var retrieveByIdResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(retrieveByIdRequest);

                    WriteObject(retrieveByIdResponse.OptionSetMetadata);

                    break;
                case GetChoiceByNameParameterSet:
                    var retrieveAllRequest = new RetrieveAllOptionSetsRequest()
                    {
                        RetrieveAsIfPublished = true
                    };
                    var retrieveAllResponse = ExecuteOrganizationRequest<RetrieveAllOptionSetsResponse>(retrieveAllRequest);

                    var result = retrieveAllResponse.OptionSetMetadata.AsEnumerable();

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Name)))
                    {
                        WildcardPattern includePattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);
                        result = result.Where(o => includePattern.IsMatch(o.Name));
                    }

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Exclude)))
                    {
                        WildcardPattern excludePattern = new WildcardPattern(Exclude, WildcardOptions.IgnoreCase);
                        result = result.Where(o => !excludePattern.IsMatch(o.Name));
                    }

                    if (Custom.IsPresent) result = result.Where(o => o.IsCustomOptionSet == true);
                    if (Unmanaged.IsPresent) result = result.Where(o => o.IsManaged == false);

                    WriteObject(result.OrderBy(o => o.Name).ToList(), true);

                    break;
            }
        }
    }
}
