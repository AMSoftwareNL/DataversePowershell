﻿/* 
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
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseChoiceOption", DefaultParameterSetName = SetGlobalChoiceOptionParameterSet)]
    public sealed class SetChoiceOptionCommand : RequestCmdletBase
    {
        private const string SetAttributeChoiceOptionParameterSet = "SetAttributeChoiceOption";
        private const string SetGlobalChoiceOptionParameterSet = "SetGlobalChoiceOption";

        [Parameter(Mandatory = true, ParameterSetName = SetAttributeChoiceOptionParameterSet)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetAttributeChoiceOptionParameterSet)]
        [Alias("AttributeLogicalName")]
        [ValidateNotNullOrEmpty]
        public string Column { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetGlobalChoiceOptionParameterSet)]
        [Alias("OptionSet", "OptionSetLogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateRange(1, int.MaxValue)]
        public int Value { get; set; }

        [Parameter(Mandatory = false)]
        public string Label { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        public override void Execute()
        {
            switch (ParameterSetName)
            {
                case SetGlobalChoiceOptionParameterSet:
                    UpdateGlobalChoice();
                    break;
                case SetAttributeChoiceOptionParameterSet:
                    UpdateAttributeChoice();
                    break;
            }
        }

        private void UpdateGlobalChoice()
        {
            var retrieveRequest = new RetrieveOptionSetRequest()
            {
                Name = Name,
                RetrieveAsIfPublished = true
            };
            var retrieveResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(retrieveRequest);

            var optionset = retrieveResponse.OptionSetMetadata as OptionSetMetadata;
            var option = optionset.Options.SingleOrDefault(o => o.Value == Value);

            if (option == null) {
                var request = new InsertOptionValueRequest()
                {
                    OptionSetName = Name,
                    Value = Value
                };

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Label)))
                    request.Label = string.IsNullOrWhiteSpace(Label) ? null : new Label(Label, Session.Current.LanguageId);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                    request.Description = string.IsNullOrWhiteSpace(Description) ? null : new Label(Description, Session.Current.LanguageId);

                var _ = ExecuteOrganizationRequest<InsertOptionValueResponse>(request);
             }
            else
            {
                var request = new UpdateOptionValueRequest()
                {
                    OptionSetName = Name,
                    Value = Value,
                    MergeLabels = true
                };

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Label)))
                    request.Label = string.IsNullOrWhiteSpace(Label) ? null : new Label(Label, Session.Current.LanguageId);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                    request.Description = string.IsNullOrWhiteSpace(Description) ? null : new Label(Description, Session.Current.LanguageId);

                var _ = ExecuteOrganizationRequest<UpdateOptionValueResponse>(request);
            }
        }

        private void UpdateAttributeChoice()
        {
            var retrieveRequest = new RetrieveAttributeRequest()
            {
                EntityLogicalName = Table,
                LogicalName = Column,
                RetrieveAsIfPublished = true
            };
            var retrieveResponse = ExecuteOrganizationRequest<RetrieveAttributeResponse>(retrieveRequest);

            var attribute = retrieveResponse.AttributeMetadata as EnumAttributeMetadata;
            var option = attribute.OptionSet.Options.SingleOrDefault(o => o.Value == Value);

            if (option == null) {
                var request = new InsertOptionValueRequest()
                {
                    EntityLogicalName = Table,
                    AttributeLogicalName = Column,
                    Value = Value,
                };

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Label)))
                    request.Label = string.IsNullOrWhiteSpace(Label) ? null : new Label(Label, Session.Current.LanguageId);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                    request.Description = string.IsNullOrWhiteSpace(Description) ? null : new Label(Description, Session.Current.LanguageId);

                var _ = ExecuteOrganizationRequest<InsertOptionValueResponse>(request);
             }
            else
            {
                var request = new UpdateOptionValueRequest()
                {
                    EntityLogicalName = Table,
                    AttributeLogicalName = Column,
                    Value = Value,
                    MergeLabels = true
                };

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Label)))
                    request.Label = string.IsNullOrWhiteSpace(Label) ? null : new Label(Label, Session.Current.LanguageId);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                    request.Description = string.IsNullOrWhiteSpace(Description) ? null : new Label(Description, Session.Current.LanguageId);

                var _ = ExecuteOrganizationRequest<UpdateOptionValueResponse>(request);
            }
        }
    }
}
