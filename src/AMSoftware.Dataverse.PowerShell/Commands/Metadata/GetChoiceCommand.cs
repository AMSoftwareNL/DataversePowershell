/* 
PowerShell Module for Power Platform Dataverse
Copyright(C) 2024  AMSoftwareNL

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
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

        [Parameter(Mandatory = false, ParameterSetName = GetChoiceByNameParameterSet, ValueFromPipeline = true)]
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
