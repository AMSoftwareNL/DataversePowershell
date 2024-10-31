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
