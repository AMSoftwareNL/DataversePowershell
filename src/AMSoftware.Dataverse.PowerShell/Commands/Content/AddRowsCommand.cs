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
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRows")]
    [OutputType(typeof(EntityReference))]
    public sealed class AddRowsCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Upsert { get; set; }

        public override void Execute()
        {
            if (Upsert.ToBool())
            {
                var request = new UpsertMultipleRequest()
                {
                    Targets = new EntityCollection(InputObject.ToList())
                };

                if (UseBatch)
                {
                    AddOrganizationRequestToBatch(request);
                }
                else
                {
                    var response = ExecuteOrganizationRequest<UpsertMultipleResponse>(request);

                    for (int i = 0; i < response.Results.Length; i++)
                    {
                        WriteObject(response.Results[i].Target);
                    }
                }
            } else
            {
                var request = new CreateMultipleRequest()
                {
                    Targets = new EntityCollection(InputObject.ToList())
                };

                if (UseBatch)
                {
                    AddOrganizationRequestToBatch(request);
                }
                else
                {
                    var response = ExecuteOrganizationRequest<CreateMultipleResponse>(request);

                    for (int i = 0; i < response.Ids.Length; i++)
                    {
                        WriteObject(new EntityReference(InputObject[i].LogicalName, response.Ids[i]));
                    }
                }
            }
        }
    }
}
