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
using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseColumn", DefaultParameterSetName = GetColumnsByFilterParameterSet)]
    [OutputType(typeof(AttributeMetadata))]
    public sealed class GetColumnCommand : RequestCmdletBase
    {
        private const string GetColumnByIdParameterSet = "GetColumnById";
        private const string GetColumnsByFilterParameterSet = "GetColumnsByFilter";

        [Parameter(Mandatory = true, ParameterSetName = GetColumnByIdParameterSet, ValueFromPipeline = true)]
        [Alias("MetadataId")]
        [ValidateNotNull]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetColumnsByFilterParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(ParameterSetName = GetColumnsByFilterParameterSet)]
        [Alias("Include")]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Name { get; set; }

        [Parameter(ParameterSetName = GetColumnsByFilterParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Exclude { get; set; }

        [Parameter(ParameterSetName = GetColumnsByFilterParameterSet)]
        public ColumnType Type { get; set; }

        [Parameter(ParameterSetName = GetColumnsByFilterParameterSet)]
        public SwitchParameter Unmanaged { get; set; }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case GetColumnByIdParameterSet:
                    var getByIdRequest = new RetrieveAttributeRequest()
                    {
                        MetadataId = Id,
                        RetrieveAsIfPublished = true
                    };

                    var getByIdResponse = ExecuteOrganizationRequest<RetrieveAttributeResponse>(getByIdRequest);

                    WriteObject(getByIdResponse.AttributeMetadata);

                    break;
                case GetColumnsByFilterParameterSet:
                    var getAttributesRequest = new RetrieveEntityRequest()
                    {
                        LogicalName = Table,
                        EntityFilters = EntityFilters.Attributes,
                        RetrieveAsIfPublished = true
                    };
                    var getAttributesResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getAttributesRequest);

                    IEnumerable<AttributeMetadata> result = getAttributesResponse.EntityMetadata.Attributes;

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

                    if (Unmanaged.IsPresent) result = result.Where(a => a.IsManaged == false);
                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Type)))
                    {
                        result = result.Where(a => a.AttributeTypeName == $"{Type.ToString("G")}Type");
                    }

                    WriteObject(result.OrderBy(a => a.LogicalName).ToList(), true);

                    break;
            }
        }
    }
}
