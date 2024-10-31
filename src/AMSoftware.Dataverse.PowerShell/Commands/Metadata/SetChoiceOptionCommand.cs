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
        [Alias("Name", "OptionSetLogicalName")]
        [ValidateNotNullOrEmpty]
        public string OptionSet { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateRange(1, int.MaxValue)]
        public int Value { get; set; }

        [Parameter(Mandatory = false)]
        public string Label { get; set; }

        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        public string ExternalValue { get; set; }

        [Parameter(Mandatory = false)]
        public string Color { get; set; }

        protected override void Execute()
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
                Name = OptionSet,
                RetrieveAsIfPublished = true
            };
            var retrieveResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(retrieveRequest);

            var optionset = retrieveResponse.OptionSetMetadata as OptionSetMetadata;

            ApplyChoiceOption(optionset);

            var updateRequest = new UpdateOptionSetRequest()
            {
                OptionSet = optionset,
                MergeLabels = true
            };

            var updateResponse = ExecuteOrganizationRequest<UpdateOptionSetResponse>(updateRequest);
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

            ApplyChoiceOption(attribute.OptionSet);

            var updateRequest = new UpdateAttributeRequest()
            {
                EntityName = Table,
                Attribute = attribute,
                MergeLabels = true,
            };

            var updateResponse = ExecuteOrganizationRequest<UpdateAttributeResponse>(updateRequest);
        }

        private void ApplyChoiceOption(OptionSetMetadata optionSet)
        {
            OptionMetadata option = default;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Value)))
                option = optionSet.Options.SingleOrDefault(o => o.Value == Value);

            if (option == null)
                option = new OptionMetadata();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Value)))
                option.Value = Value;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Label)))
                option.Label = string.IsNullOrWhiteSpace(Label) ? null : new Label(Label, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                option.Description = string.IsNullOrWhiteSpace(Description) ? null : new Label(Description, Session.Current.LanguageId);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ExternalValue)))
                option.ExternalValue = ExternalValue;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Color)))
                option.Color = Color;
        }
    }
}
