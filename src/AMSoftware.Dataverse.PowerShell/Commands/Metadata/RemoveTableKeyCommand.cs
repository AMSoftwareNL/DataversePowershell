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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseTableKey", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveTableKeyCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("EntityLogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        protected override void Execute()
        {
            var request = new DeleteEntityKeyRequest()
            {
                EntityLogicalName = Table,
                Name = Name
            };

            if (Force.ToBool() && !MyInvocation.BoundParameters.ContainsKey("Confirm"))
            {
                MyInvocation.BoundParameters["Confirm"] = "None";
            }

            if (ShouldProcess("DeleteEntityKey", $"{Table} {Name}"))
            {
                var _ = ExecuteOrganizationRequest<DeleteEntityKeyResponse>(request);
            }
        }
    }
}
