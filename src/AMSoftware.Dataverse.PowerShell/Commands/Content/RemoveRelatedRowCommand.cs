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
    [Cmdlet(VerbsCommon.Remove, "DataverseRelatedRow", DefaultParameterSetName = RemoveSingleRelatedRowParameterSet, SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public sealed class RemoveRelatedRowCommand : BatchCmdletBase
    {
        private const string RemoveSingleRelatedRowParameterSet = "RemoveSingleRelatedRow";
        private const string RemoveCollectionRelatedRowsParameterSet = "RemoveCollectionRelatedRows";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string TargetTable { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid TargetRow { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RemoveSingleRelatedRowParameterSet)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RemoveSingleRelatedRowParameterSet)]
        [Alias("Id")]
        [ValidateNotNullOrEmpty]
        public Guid RelatedRow { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveCollectionRelatedRowsParameterSet)]
        [ValidateNotNullOrEmpty]
        public EntityReference[] Rows { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Relationship { get; set; }

        [Parameter()]
        public SwitchParameter Force { get; set; }

        protected override void Execute()
        {
            var relatedRows = new EntityReferenceCollection();

            switch (ParameterSetName)
            {
                case RemoveSingleRelatedRowParameterSet:
                    relatedRows.Add(new EntityReference(RelatedTable, RelatedRow));
                    break;

                case RemoveCollectionRelatedRowsParameterSet:
                    relatedRows.AddRange(Rows);
                    break;
            }

            var request = new DisassociateRequest()
            {
                Target = new EntityReference(TargetTable, TargetRow),
                RelatedEntities = relatedRows,
                Relationship = new Relationship(Relationship)
            };

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                if (Force || ShouldProcess("Disassociate", $"{TargetTable} {TargetRow}"))
                {
                    var _ = ExecuteOrganizationRequest<DisassociateResponse>(request);
                }
            }
        }
    }
}
