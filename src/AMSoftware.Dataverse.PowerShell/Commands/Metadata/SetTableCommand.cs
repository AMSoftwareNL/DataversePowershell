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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseTable")]
    [OutputType(typeof(EntityMetadata))]
    public sealed class SetTableCommand : RequestCmdletBase
    {
        private const string SetTableObjectParameterSet = "SetTableObject";
        private const string SetTableParameterSet = "SetTable";

        [Parameter(Mandatory = true, ParameterSetName = SetTableObjectParameterSet)]
        [Alias("Entity")]
        [ValidateNotNullOrEmpty]
        public EntityMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetTableParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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

        public override void Execute()
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
                MergeLabels = true
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(HasAttachments)))
                updateRequest.HasNotes = HasAttachments.ToBool();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(IsActivityParty)))
                updateRequest.HasActivities = IsActivityParty.ToBool();

            var updateResponse = ExecuteOrganizationRequest<UpdateEntityResponse>(updateRequest);

            var getByNameRequest = new RetrieveEntityRequest()
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = Name,
                RetrieveAsIfPublished = true
            };
            var getByNameResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getByNameRequest);

            WriteObject(getByNameResponse.EntityMetadata);
        }

        private EntityMetadata BuildEntityMetadata()
        {
            var result = new EntityMetadata()
            {
                LogicalName = Name
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DisplayName)))
                result.DisplayName = new Label(DisplayName, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(PluralName)))
                result.DisplayCollectionName = new Label(PluralName, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                result.Description = Description == null ? null : new Label(Description, Session.Current.LanguageId);

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
