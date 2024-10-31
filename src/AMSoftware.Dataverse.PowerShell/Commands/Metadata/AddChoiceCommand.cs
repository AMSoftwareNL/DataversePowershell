using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseChoice", DefaultParameterSetName = AddChoiceObjectParameterSet)]
    [OutputType(typeof(OptionSetMetadataBase))]
    public sealed class AddChoiceCommand : RequestCmdletBase
    {
        private const string AddChoiceObjectParameterSet = "AddChoiceObject";
        private const string AddNewChoiceParameterSet = "AddNewChoice";

        [Parameter(Mandatory = true, ParameterSetName = AddChoiceObjectParameterSet)]
        [Alias("Choice", "OptionSet")]
        [ValidateNotNullOrEmpty]
        public OptionSetMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddNewChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddNewChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddNewChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddNewChoiceParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddNewChoiceParameterSet, ValueFromRemainingArguments = true)]
        [ValidateNotNull]
        [ValidateCount(1, int.MaxValue)]
        public OptionMetadata[] Options { get; set; }

        protected override void Execute()
        {
            OptionSetMetadata choiceMetadata = null;

            switch (ParameterSetName)
            {
                case AddNewChoiceParameterSet:
                    choiceMetadata = BuildChoiceMetadata();
                    break;
                case AddChoiceObjectParameterSet:
                    choiceMetadata = InputObject;
                    break;
            }

            var createRequest = new CreateOptionSetRequest()
            {
                OptionSet = choiceMetadata
            };

            var createResponse = ExecuteOrganizationRequest<CreateOptionSetResponse>(createRequest);

            var getByIdRequest = new RetrieveOptionSetRequest()
            {
                MetadataId = createResponse.OptionSetId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(getByIdRequest);

            WriteObject(getByIdResponse.OptionSetMetadata);
        }

        private OptionSetMetadata BuildChoiceMetadata()
        {
            var choiceMetadata = new OptionSetMetadata()
            {
                Name = Name,
                OptionSetType = OptionSetType.Picklist,
                IsGlobal = true,
                Description = Description == null ? null : new Label(Description, Session.Current.LanguageId),
                ExternalTypeName = ExternalName
            };
            choiceMetadata.Options.AddRange(Options);

            return choiceMetadata;
        }
    }
}
