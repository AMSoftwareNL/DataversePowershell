using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseTable")]
    [OutputType(typeof(EntityMetadata))]
    public sealed class SetTableCommand : CmdletBase
    {
        private const string SetTableObjectParameterSet = "SetTableObject";
        private const string SetTableParameterSet = "SetTable";

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SetTableObjectParameterSet)]
        [Alias("Entity")]
        [ValidateNotNullOrEmpty]
        public EntityMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SetTableParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string PluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalPluralName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        public SwitchParameter HasAttachments { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        public SwitchParameter IsActivityParty { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        public SwitchParameter TrackChanges { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataProviderId { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetTableParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid DataSourceId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter MergeLabels { get; set; }

        protected override void Execute()
        {
            EntityMetadata entityMetadata = null;

            switch (ParameterSetName)
            {
                case SetTableObjectParameterSet:
                    entityMetadata = InputObject;
                    break;
                case SetTableParameterSet:
                    entityMetadata = BuildEntityMetadata();
                    break;
            }

            var updateRequest = new UpdateEntityRequest()
            {
                Entity = entityMetadata,
                SolutionUniqueName = null,
                MergeLabels = MergeLabels.ToBool()
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(HasAttachments)))
                updateRequest.HasNotes = HasAttachments.ToBool();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(IsActivityParty)))
                updateRequest.HasActivities = IsActivityParty.ToBool();

            var updateResponse = (UpdateEntityResponse)Session.Current.Client.ExecuteOrganizationRequest(
                updateRequest, MyInvocation.MyCommand.Name);

            var getByNameRequest = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = Name,
                RetrieveAsIfPublished = true
            };
            var getByNameResponse = (RetrieveEntityResponse)Session.Current.Client.ExecuteOrganizationRequest(
                getByNameRequest, MyInvocation.MyCommand.Name);

            WriteObject(getByNameResponse.EntityMetadata);
        }

        private EntityMetadata BuildEntityMetadata()
        {
            var result = new EntityMetadata()
            {
                LogicalName = Name
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DisplayName)))
                result.DisplayName = new Label(DisplayName, 1033);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(PluralName)))
                result.DisplayCollectionName = new Label(PluralName, 1033);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                result.Description = Description == null ? null : new Label(Description, 1033);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ExternalName)))
                result.ExternalName = ExternalName;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ExternalPluralName)))
                result.ExternalCollectionName = ExternalPluralName;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(TrackChanges)))
                result.ChangeTrackingEnabled = TrackChanges.ToBool();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DataProviderId)))
                result.DataProviderId = DataProviderId;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DataSourceId)))
                result.DataSourceId = DataSourceId;

            return result;
        }
    }
}
