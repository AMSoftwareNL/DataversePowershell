using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseChoiceOption", DefaultParameterSetName = RemoveGlobalChoiceOptionParameterSet)]
    public sealed class RemoveChoiceOptionCommand : RequestCmdletBase
    {
        private const string RemoveAttributeChoiceOptionParameterSet = "RemoveAttributeChoiceOption";
        private const string RemoveGlobalChoiceOptionParameterSet = "RemoveGlobalChoiceOption";

        [Parameter(Mandatory = true, ParameterSetName = RemoveAttributeChoiceOptionParameterSet)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveAttributeChoiceOptionParameterSet)]
        [Alias("AttributeLogicalName")]
        [ValidateNotNullOrEmpty]
        public string Column { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveGlobalChoiceOptionParameterSet)]
        [Alias("Name", "OptionSetLogicalName")]
        [ValidateNotNullOrEmpty]
        public string OptionSet { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateRange(1, int.MaxValue)]
        public int Value { get; set; }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case RemoveGlobalChoiceOptionParameterSet:
                    RemoveGlobalChoice();
                    break;
                case RemoveAttributeChoiceOptionParameterSet:
                    RemoveAttributeChoice();
                    break;
            }
        }

        private void RemoveGlobalChoice()
        {
            var retrieveRequest = new RetrieveOptionSetRequest()
            {
                Name = OptionSet,
                RetrieveAsIfPublished = true
            };
            var retrieveResponse = ExecuteOrganizationRequest<RetrieveOptionSetResponse>(retrieveRequest);

            var optionset = retrieveResponse.OptionSetMetadata as OptionSetMetadata;

            RemoveChoiceOption(optionset);

            var updateRequest = new UpdateOptionSetRequest()
            {
                OptionSet = optionset,
                MergeLabels = true
            };

            var updateResponse = ExecuteOrganizationRequest<UpdateOptionSetResponse>(updateRequest);
        }

        private void RemoveAttributeChoice()
        {
            var retrieveRequest = new RetrieveAttributeRequest()
            {
                EntityLogicalName = Table,
                LogicalName = Column,
                RetrieveAsIfPublished = true
            };
            var retrieveResponse = ExecuteOrganizationRequest<RetrieveAttributeResponse>(retrieveRequest);

            var attribute = retrieveResponse.AttributeMetadata as EnumAttributeMetadata;

            RemoveChoiceOption(attribute.OptionSet);

            var updateRequest = new UpdateAttributeRequest()
            {
                EntityName = Table,
                Attribute = attribute,
                MergeLabels = true,
            };

            var updateResponse = ExecuteOrganizationRequest<UpdateAttributeResponse>(updateRequest);
        }

        private void RemoveChoiceOption(OptionSetMetadata optionSet)
        {
            var option = optionSet.Options.Single(o => o.Value == Value);
            optionSet.Options.Remove(option);
        }
    }
}
