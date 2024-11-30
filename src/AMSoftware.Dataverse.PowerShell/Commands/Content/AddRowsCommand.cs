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
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRows")]
    [OutputType(typeof(EntityReference))]
    public sealed class AddRowsCommand : BatchCmdletBase
    {
        private readonly List<Entity> _rowsToProcess = [];

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Upsert { get; set; }

        public override void Execute()
        {
            _rowsToProcess.AddRange(InputObject);
        }

        protected override void EndProcessing()
        {
            var request = new OrganizationRequest(Upsert.ToBool() ? "UpsertMultiple" : "CreateMultiple");
            request.Parameters.Add("Targets", new EntityCollection(_rowsToProcess));

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<OrganizationResponse>(request);

                if (response is UpsertMultipleResponse upsertMultipleResponse)
                {
                    WriteObject(upsertMultipleResponse.Results.Select(r => r.Target), true);
                }

                if (response is CreateMultipleResponse createMultipleResponse)
                {
                    for (int i = 0; i < createMultipleResponse.Ids.Length; i++)
                    {
                        WriteObject(new EntityReference(_rowsToProcess[i].LogicalName, createMultipleResponse.Ids[i]));
                    }
                }
            }

            base.EndProcessing();
        }
    }
}
