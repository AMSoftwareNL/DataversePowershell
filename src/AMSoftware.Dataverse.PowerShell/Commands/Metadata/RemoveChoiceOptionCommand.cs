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
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseChoiceOption", DefaultParameterSetName = RemoveGlobalChoiceOptionParameterSet, ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
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
        [Alias("OptionSet", "OptionSetLogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateRange(1, int.MaxValue)]
        public int Value { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        public override void Execute()
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
            var request = new DeleteOptionValueRequest()
            {
                OptionSetName = Name,
                Value = Value
            };

            if (Force || ShouldProcess("DeleteOptionValue", $"{Name} ({Value})"))
            {
                var _ = ExecuteOrganizationRequest<DeleteOptionValueResponse>(request);
            }
        }

        private void RemoveAttributeChoice()
        {
            var request = new DeleteOptionValueRequest()
            {
                EntityLogicalName = Table,
                AttributeLogicalName = Column,
                Value = Value
            };

            if (Force || ShouldProcess("DeleteOptionValue", $"{Table} {Column} ({Value})"))
            {
                var _ = ExecuteOrganizationRequest<DeleteOptionValueResponse>(request);
            }
        }
    }
}
