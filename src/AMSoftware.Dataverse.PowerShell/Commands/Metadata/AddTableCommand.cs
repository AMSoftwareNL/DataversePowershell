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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseTable", DefaultParameterSetName = AddTableObjectParameterSet)]
    [OutputType(typeof(EntityMetadata))]
    public sealed class AddTableCommand : RequestCmdletBase
    {
        private const string AddTableObjectParameterSet = "AddTableObject";
        private const string AddStandardTableParameterSet = "AddStandardTable";
        private const string AddActivityTableParameterSet = "AddActivityTable";
        private const string AddElasticTableParameterSet = "AddElasticTable";
        private const string AddVirtualTableParameterSet = "AddVirtualTable";

        [Parameter(Mandatory = true, ParameterSetName = AddTableObjectParameterSet)]
        [Alias("Entity")]
        [ValidateNotNullOrEmpty]
        public EntityMetadata TableInputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddTableObjectParameterSet)]
        [Alias("Attribute", "PrimaryAttribute")]
        [ValidateNotNullOrEmpty]
        public StringAttributeMetadata ColumnInputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        public SwitchParameter Elastic { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        public SwitchParameter Virtual { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddActivityTableParameterSet)]
        public SwitchParameter Activity { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [Alias("SchemaName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string PluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddActivityTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        [PSDefaultValue(Value = TableOwnershipType.User)]
        public TableOwnershipType OwnershipType { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        public SwitchParameter HasAttachments { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        public SwitchParameter IsActivityParty { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddActivityTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        public SwitchParameter TrackChanges { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [Alias("PrimaryAttributeDisplayName")]
        [ValidateNotNullOrEmpty]
        public string ColumnDisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [Alias("PrimaryAttributeDescription")]
        [ValidateNotNullOrEmpty]
        public string ColumnDescription { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [Alias("PrimaryAttributeSchemaName", "PrimaryAttributeLogicalName")]
        [ValidateNotNullOrEmpty]
        public string ColumnName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [PSDefaultValue(Value = 100)]
        [Alias("PrimaryAttributeLength")]
        [ValidateRange(1, 4000)]
        public uint ColumnLength { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [PSDefaultValue(Value = ColumnRequiredLevel.Required)]
        [Alias("PrimaryAttributeRequirement")]
        public ColumnRequiredLevel ColumnRequired { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddActivityTableParameterSet)]
        public SwitchParameter HideFromMenu { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalPluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [Alias("PrimaryAttributeExternalName")]
        [ValidateNotNullOrEmpty]
        public string ColumnExternalName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataProviderId { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataSourceId { get; set; }

        public override void Execute()
        {
            EntityMetadata entityMetadata = null;
            StringAttributeMetadata attributeMetadata = null;

            switch (ParameterSetName)
            {
                case AddStandardTableParameterSet:
                    BuildStandardEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case AddElasticTableParameterSet:
                    BuildElasticEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case AddVirtualTableParameterSet:
                    BuildVirtualEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case AddActivityTableParameterSet:
                    BuildActivityEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case AddTableObjectParameterSet:
                    entityMetadata = TableInputObject;
                    attributeMetadata = ColumnInputObject;
                    break;
            }

            var createRequest = new CreateEntityRequest()
            {
                Entity = entityMetadata,
                PrimaryAttribute = attributeMetadata,
                HasActivities = IsActivityParty.ToBool(),
                HasFeedback = false,
                HasNotes = HasAttachments.ToBool() || Activity.ToBool()
            };

            var createResponse = ExecuteOrganizationRequest<CreateEntityResponse>(createRequest);

            var getByIdRequest = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                MetadataId = createResponse.EntityId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getByIdRequest);

            WriteObject(getByIdResponse.EntityMetadata);
        }

        private void BuildElasticEntityMetadata(out EntityMetadata entityMetadata, out StringAttributeMetadata attributeMetadata)
        {
            BuildStandardEntityMetadata(out entityMetadata, out attributeMetadata);

            entityMetadata.TableType = "Elastic";
        }

        private void BuildActivityEntityMetadata(out EntityMetadata entityMetadata, out StringAttributeMetadata attributeMetadata)
        {
            BuildStandardEntityMetadata(out entityMetadata, out attributeMetadata);

            entityMetadata.TableType = "Activity";
            entityMetadata.IsActivity = true;
            entityMetadata.OwnershipType = OwnershipTypes.UserOwned;

            if (HideFromMenu.ToBool())
                entityMetadata.ActivityTypeMask = 0;

            attributeMetadata.LogicalName = "subject";
            attributeMetadata.SchemaName = "subject";
            attributeMetadata.DisplayName = new Label("Subject", Session.Current.LanguageId);
            attributeMetadata.MaxLength = 400;
            attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.SystemRequired);
        }

        private void BuildVirtualEntityMetadata(out EntityMetadata entityMetadata, out StringAttributeMetadata attributeMetadata)
        {
            BuildStandardEntityMetadata(out entityMetadata, out attributeMetadata);

            entityMetadata.TableType = "Virtual";
            entityMetadata.ExternalName = ExternalName;
            entityMetadata.ExternalCollectionName = ExternalPluralName;
            entityMetadata.OwnershipType = OwnershipTypes.OrganizationOwned;
            entityMetadata.DataProviderId = new Guid("7015A531-CC0D-4537-B5F2-C882A1EB65AD");  // Default for DataProvider = None

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DataProviderId)))
                entityMetadata.DataProviderId = DataProviderId;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DataSourceId)))
                entityMetadata.DataSourceId = DataSourceId;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnExternalName)))
                attributeMetadata.ExternalName = ColumnExternalName;
        }

        private void BuildStandardEntityMetadata(out EntityMetadata entityMetadata, out StringAttributeMetadata attributeMetadata)
        {
            entityMetadata = new EntityMetadata()
            {
                LogicalName = Name,
                SchemaName = Name,
                DisplayName = new Label(DisplayName, Session.Current.LanguageId),
                DisplayCollectionName = new Label(PluralName, Session.Current.LanguageId),
                Description = Description == null ? null : new Label(Description, Session.Current.LanguageId),
                TableType = "Standard"
            };

            entityMetadata.OwnershipType = OwnershipTypes.UserOwned;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(OwnershipType)))
                entityMetadata.OwnershipType = (OwnershipTypes)OwnershipType;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(TrackChanges)))
                entityMetadata.ChangeTrackingEnabled = TrackChanges.ToBool();

            attributeMetadata = new StringAttributeMetadata()
            {
                LogicalName = ColumnName,
                SchemaName = ColumnName,
                DisplayName = new Label(ColumnDisplayName, Session.Current.LanguageId),
                Description = Description == null ? null : new Label(ColumnDescription, Session.Current.LanguageId),
                MaxLength = 100,
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.SystemRequired)
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnLength)))
                attributeMetadata.MaxLength = (int)ColumnLength;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnRequired)))
                attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)ColumnRequired);
        }
    }
}
