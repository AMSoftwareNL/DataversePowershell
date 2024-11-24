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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseRelationship", DefaultParameterSetName = SetManyToOneRelationshipParameterSet)]
    [OutputType(typeof(RelationshipMetadataBase))]
    public sealed class SetRelationshipCommand : RequestCmdletBase
    {
        private const string SetManyToManyRelationshipParameterSet = "SetManyToManyRelationship";
        private const string SetManyToOneRelationshipParameterSet = "SetManyToOneRelationship";
        private const string SetRelationshipByInputObjectParameterset = "SetRelationshipByInputObject";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = SetRelationshipByInputObjectParameterset)]
        [ValidateNotNullOrEmpty]
        public RelationshipMetadataBase InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = SetManyToOneRelationshipParameterSet)]
        [Alias("SchemaName", "Name")]
        [ValidateNotNullOrEmpty]
        public string Relationship { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = SetManyToOneRelationshipParameterSet)]
        public AssociatedMenuConfiguration MenuConfiguration { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetManyToManyRelationshipParameterSet)]
        public AssociatedMenuConfiguration RelatedMenuConfiguration { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetManyToOneRelationshipParameterSet)]
        public CascadeConfiguration Behavior { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter MergeLabels { get; set; }

        public override void Execute()
        {
            RelationshipMetadataBase relationshipMetadata = InputObject;

            if (ParameterSetName == SetManyToManyRelationshipParameterSet ||
                ParameterSetName == SetManyToOneRelationshipParameterSet)
            {
                var retrieveByNameRequest = new RetrieveRelationshipRequest()
                {
                    Name = Relationship
                };
                var retrieveByNameResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(retrieveByNameRequest);
                relationshipMetadata = retrieveByNameResponse.RelationshipMetadata;
            }

            switch (ParameterSetName)
            {
                case SetManyToManyRelationshipParameterSet:
                    var manyToManyRelationshipMetadata = relationshipMetadata as ManyToManyRelationshipMetadata;
                    if (MyInvocation.BoundParameters.ContainsKey(nameof(MenuConfiguration)))
                        manyToManyRelationshipMetadata.Entity1AssociatedMenuConfiguration = MenuConfiguration;

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(RelatedMenuConfiguration)))
                        manyToManyRelationshipMetadata.Entity2AssociatedMenuConfiguration = RelatedMenuConfiguration;

                    break;
                case SetManyToOneRelationshipParameterSet:
                    var manyToOneRelationshipMetadata = relationshipMetadata as OneToManyRelationshipMetadata;
                    if (MyInvocation.BoundParameters.ContainsKey(nameof(MenuConfiguration)))
                        manyToOneRelationshipMetadata.AssociatedMenuConfiguration = MenuConfiguration;

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Behavior)))
                        manyToOneRelationshipMetadata.CascadeConfiguration = Behavior;

                    break;
                default:
                    break;
            }

            var updateRequest = new UpdateRelationshipRequest()
            {
                Relationship = relationshipMetadata,
                MergeLabels = MergeLabels.ToBool()
            };
            var updateResponse = ExecuteOrganizationRequest<UpdateRelationshipResponse>(updateRequest);

            var getMetadataRequest = new RetrieveRelationshipRequest()
            {
                Name = relationshipMetadata.SchemaName,
                RetrieveAsIfPublished = true
            };
            var getMetadataResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(getMetadataRequest);

            WriteObject(getMetadataResponse.RelationshipMetadata);
        }
    }
}
