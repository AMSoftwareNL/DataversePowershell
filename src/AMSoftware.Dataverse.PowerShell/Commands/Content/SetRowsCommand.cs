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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Set, "DataverseRows")]
    [OutputType(typeof(EntityReference))]
    public sealed class SetRowsCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [PSDefaultValue(Value = ConcurrencyBehavior.Default)]
        public ConcurrencyBehavior Behavior { get; set; }

        public override void Execute()
        {
            var request = new UpdateMultipleRequest()
            {
                Targets = new EntityCollection(InputObject.ToList())
            };
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Behavior)))
            {
                request.ConcurrencyBehavior = Behavior;
            }

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<UpdateMultipleResponse>(request);

                for (int i = 0; i < InputObject.Length; i++)
                {
                    WriteObject(InputObject[i].ToEntityReference());
                }
            }
        }
    }
}
