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
using System.Collections.Generic;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRelatedRow", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Low)]
    public sealed class RemoveRelatedRowCommand : BatchCmdletBase
    {
        private readonly List<EntityReference> _relatedRows = [];

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string TargetTable { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid TargetRow { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Id")]
        [ValidateNotNullOrEmpty]
        public Guid RelatedRow { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Relationship { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        public override void Execute()
        {
            if (Force || ShouldProcess("Disassociate", $"{TargetTable} {TargetRow} < - > {RelatedTable} {RelatedRow}"))
            {
                _relatedRows.Add(new EntityReference(RelatedTable, RelatedRow));
            }
        }

        protected override void EndProcessing()
        {
            var request = new DisassociateRequest()
            {
                Target = new EntityReference(TargetTable, TargetRow),
                RelatedEntities = new EntityReferenceCollection(_relatedRows),
                Relationship = new Relationship(Relationship)
            };

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var _ = ExecuteOrganizationRequest<DisassociateResponse>(request);
            }
            base.EndProcessing();
        }
    }
}
