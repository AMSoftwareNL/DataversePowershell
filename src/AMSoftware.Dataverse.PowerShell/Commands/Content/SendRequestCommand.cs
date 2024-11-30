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
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommunications.Send, "DataverseRequest", DefaultParameterSetName = MessageParameterSet)]
    [OutputType(typeof(ParameterCollection))]
    public sealed class SendRequestCommand : BatchCmdletBase
    {
        private const string FunctionParameterSet = "Function";
        private const string MessageParameterSet = "Message";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Request")]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public Hashtable Parameters { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = FunctionParameterSet, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string TargetTable { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = FunctionParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [Alias("Id")]
        public Guid TargetRow { get; set; }

        public override void Execute()
        {
            var request = new OrganizationRequest(Name);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Parameters)))
            {
                foreach (var parameterName in Parameters.Keys)
                {
                    var parameterValue = Parameters[parameterName];

                    if (parameterValue is PSObject psValue)
                        request.Parameters.Add((string)parameterName, psValue.ImmediateBaseObject);
                    else
                        request.Parameters.Add((string)parameterName, parameterValue);
                }
            }

            if (ParameterSetName == FunctionParameterSet)
            {
                request.Parameters.Add("Target", new EntityReference(TargetTable, TargetRow));
            }

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<OrganizationResponse>(request);

                WriteObject(response);
            }
        }
    }
}
