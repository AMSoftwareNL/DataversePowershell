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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseChoice", DefaultParameterSetName = SetChoiceObjectParameterSet)]
    [OutputType(typeof(OptionSetMetadataBase))]
    public sealed class SetChoiceCommand : RequestCmdletBase
    {
        private const string SetChoiceObjectParameterSet = "SetChoiceObject";
        private const string SetChoiceParameterSet = "SetChoice";

        [Parameter(Mandatory = true, ParameterSetName = SetChoiceObjectParameterSet)]
        [Alias("Choice", "OptionSet")]
        [ValidateNotNullOrEmpty]
        public OptionSetMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter MergeLabels { get; set; }

        protected override void Execute()
        {
            OptionSetMetadata choiceMetadata = null;

            switch (ParameterSetName)
            {
                case SetChoiceParameterSet:
                    choiceMetadata = BuildChoiceMetadata();
                    break;
                case SetChoiceObjectParameterSet:
                    choiceMetadata = InputObject;
                    break;
            }

            var updateRequest = new UpdateOptionSetRequest()
            {
                OptionSet = choiceMetadata,
                MergeLabels = MergeLabels.ToBool()
            };

            var updateResponse = ExecuteOrganizationRequest<UpdateOptionSetResponse>(updateRequest);

            var getByIdRequest = new RetrieveOptionSetRequest()
            {
                Name = choiceMetadata.Name,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(getByIdRequest);

            WriteObject(getByIdResponse.OptionSetMetadata);
        }

        private OptionSetMetadata BuildChoiceMetadata()
        {
            var choiceMetadata = new OptionSetMetadata()
            {
                Name = Name
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(DisplayName)))
                choiceMetadata.DisplayName = new Label(DisplayName, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                choiceMetadata.Description = Description == null ? null : new Label(Description, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ExternalName)))
                choiceMetadata.ExternalTypeName = ExternalName;

            return choiceMetadata;
        }
    }
}
