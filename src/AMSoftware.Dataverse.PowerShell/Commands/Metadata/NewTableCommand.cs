using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.New, "DataverseTable", DefaultParameterSetName = NewTableObjectParameterSet)]
    [OutputType(typeof(EntityMetadata))]
    public sealed class NewTableCommand : RequestCmdletBase
    {
        private const string NewTableObjectParameterSet = "NewTableObject";
        private const string NewStandardTableParameterSet = "NewStandardTable";
        private const string NewActivityTableParameterSet = "NewActivityTable";
        private const string NewElasticTableParameterSet = "NewElasticTable";
        private const string NewVirtualTableParameterSet = "NewVirtualTable";

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = NewTableObjectParameterSet)]
        [Alias("Entity")]
        [ValidateNotNullOrEmpty]
        public EntityMetadata TableInputObject { get; set; }

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = NewTableObjectParameterSet)]
        [Alias("Attribute", "PrimaryAttribute")]
        [ValidateNotNullOrEmpty]
        public StringAttributeMetadata ColumnInputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        public SwitchParameter Elastic { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        public SwitchParameter Virtual { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewActivityTableParameterSet)]
        public SwitchParameter Activity { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [Alias("SchemaName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewActivityTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string PluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewActivityTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        [PSDefaultValue(Value = TableOwnershipType.User)]
        public TableOwnershipType OwnershipType { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        public SwitchParameter HasAttachments { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        public SwitchParameter IsActivityParty { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewActivityTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        public SwitchParameter TrackChanges { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [Alias("PrimaryAttributeDisplayName")]
        [ValidateNotNullOrEmpty]
        public string ColumnDisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [Alias("PrimaryAttributeDescription")]
        [ValidateNotNullOrEmpty]
        public string ColumnDescription { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [Alias("PrimaryAttributeSchemaName", "PrimaryAttributeLogicalName")]
        [ValidateNotNullOrEmpty]
        public string ColumnName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [PSDefaultValue(Value = 100)]
        [Alias("PrimaryAttributeLength")]
        [ValidateRange(1, 4000)]
        public uint ColumnLength { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewStandardTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewElasticTableParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [PSDefaultValue(Value = ColumnRequiredLevel.Required)]
        [Alias("PrimaryAttributeRequirement")]
        public ColumnRequiredLevel ColumnRequirement { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewActivityTableParameterSet)]
        public SwitchParameter HideFromMenu { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalPluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [Alias("PrimaryAttributeExternalName")]
        [ValidateNotNullOrEmpty]
        public string ColumnExternalName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataProviderId { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = NewVirtualTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataSourceId { get; set; }

        protected override void Execute()
        {
            EntityMetadata entityMetadata = null;
            StringAttributeMetadata attributeMetadata = null;

            switch (ParameterSetName)
            {
                case NewStandardTableParameterSet:
                    BuildStandardEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case NewElasticTableParameterSet:
                    BuildElasticEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case NewVirtualTableParameterSet:
                    BuildVirtualEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case NewActivityTableParameterSet:
                    BuildActivityEntityMetadata(out entityMetadata, out attributeMetadata);
                    break;
                case NewTableObjectParameterSet:
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

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnRequirement)))
                attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)ColumnRequirement);
        }
    }

    public enum TableOwnershipType
    {
        User = 0x1,
        Organization = 0x8
    }

    public enum ColumnRequiredLevel
    {
        Optional = 0,
        Required = 1,
        Recommended = 3
    }
}
