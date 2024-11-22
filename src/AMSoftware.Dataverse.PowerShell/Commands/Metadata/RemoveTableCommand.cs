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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseTable", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveTableCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName", "TableName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Name { get; set; }

        [Parameter()]
        public SwitchParameter Force {get; set; }

        protected override void Execute()
        {
            var entityLogicalName = Name;

            OrganizationRequest request = new DeleteEntityRequest()
            {
                LogicalName = entityLogicalName
            };

            if (Force.ToBool() && !MyInvocation.BoundParameters.ContainsKey("Confirm"))
            {
                MyInvocation.BoundParameters["Confirm"] = "None";
            }

            if (ShouldProcess("DeleteEntity", entityLogicalName))
            {
                DeleteEntityResponse response = ExecuteOrganizationRequest<DeleteEntityResponse>(request);
            }
        }
    }
}
