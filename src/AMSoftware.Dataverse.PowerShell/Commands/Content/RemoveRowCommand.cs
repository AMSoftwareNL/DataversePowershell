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
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRow", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveRowCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        public override void Execute()
        {
            var rowReference = new EntityReference(Table, Id);

            var request = new DeleteRequest()
            {
                Target = rowReference
            };

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                if (Force || ShouldProcess("Delete", $"{Table} {Id}"))
                {
                    var _ = ExecuteOrganizationRequest<DeleteResponse>(request);
                }
            }
        }
    }
}
