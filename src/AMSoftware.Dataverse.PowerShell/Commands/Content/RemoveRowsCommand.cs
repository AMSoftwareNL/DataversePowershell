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
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRows", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveRowsCommand : BatchCmdletBase
    {
        private readonly List<EntityReference> _rowsToProcess = [];

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public Guid[] Id { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        public override void Execute()
        {
            foreach (var rowId in Id)
            {
                if (Force || ShouldProcess("Delete", $"{Table} {rowId}"))
                {
                    _rowsToProcess.Add(new EntityReference(Table, rowId));
                }
            }
        }

        protected override void EndProcessing()
        {
            var request = new OrganizationRequest("DeleteMultiple")
            {
                Parameters = {
                    {
                        "Targets", new EntityReferenceCollection(_rowsToProcess)
                    }
                }
            };

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var _ = ExecuteOrganizationRequest<OrganizationResponse>(request);
            }

            base.EndProcessing();
        }
    }
}
