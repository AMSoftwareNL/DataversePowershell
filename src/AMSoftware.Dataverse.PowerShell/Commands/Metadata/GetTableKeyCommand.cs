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
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseTableKey")]
    [OutputType(typeof(EntityKeyMetadata))]
    public sealed class GetTableKeyCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName", "EntityLogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter]
        [Alias("Include")]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Name { get; set; }

        [Parameter]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        public string Exclude { get; set; }

        [Parameter]
        public SwitchParameter ExcludeManaged { get; set; }

        [Parameter(ValueFromRemainingArguments = true)]
        [Alias("Attributes")]
        [ValidateNotNullOrEmpty]
        [ValidateCount(1, int.MaxValue)]
        public string[] Columns { get; set; }

        public override void Execute()
        {
            var getByIdRequest = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.All,
                LogicalName = Table,
                RetrieveAsIfPublished = true
            };

            var getByIdResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getByIdRequest);

            var keys = getByIdResponse.EntityMetadata.Keys.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                WildcardPattern includePattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);
                keys = keys.Where(a => includePattern.IsMatch(a.LogicalName));
            }
            if (!string.IsNullOrWhiteSpace(Exclude))
            {
                WildcardPattern excludePattern = new WildcardPattern(Exclude, WildcardOptions.IgnoreCase);
                keys = keys.Where(a => !excludePattern.IsMatch(a.LogicalName));
            }

            if (Columns != null && Columns.Length != 0)
            {
                keys = keys.Where(
                    a => a.KeyAttributes.Intersect(Columns, StringComparer.InvariantCultureIgnoreCase).Count() == Columns.Length);
            }

            keys = keys.OrderBy(k => k.LogicalName);

            WriteObject(keys, true);
        }
    }
}
