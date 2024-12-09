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
    [Cmdlet(VerbsCommon.Add, "DataverseChoice", DefaultParameterSetName = AddChoiceObjectParameterSet)]
    [OutputType(typeof(OptionSetMetadataBase))]
    public sealed class AddChoiceCommand : RequestCmdletBase
    {
        private const string AddChoiceObjectParameterSet = "AddChoiceObject";
        private const string AddNewChoiceParameterSet = "AddNewChoice";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = AddChoiceObjectParameterSet)]
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

        public override void Execute()
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
                DisplayName = new Label(DisplayName, Session.Current.LanguageId),
                Description = Description == null ? null : new Label(Description, Session.Current.LanguageId),
                ExternalTypeName = ExternalName,
            };
            choiceMetadata.Options.AddRange(Options);

            return choiceMetadata;
        }
    }
}
