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
