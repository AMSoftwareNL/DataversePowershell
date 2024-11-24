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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Set, "DataverseRow", DefaultParameterSetName = SetObjectParameterSet)]
    [OutputType(typeof(EntityReference))]
    public sealed class SetRowCommand : BatchCmdletBase
    {
        private const string SetObjectParameterSet = "SetObject";
        private const string SetValuesParameterSet = "SetValues";

        [Parameter(Mandatory = true, ParameterSetName = SetObjectParameterSet, ValueFromPipeline = true)]
        [Alias("Row", "Entity")]
        [ValidateNotNullOrEmpty]
        public Entity InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Values { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [PSDefaultValue(Value = ConcurrencyBehavior.Default)]
        public ConcurrencyBehavior Behavior { get; set; }

        public override void Execute()
        {
            Entity newEntity = InputObject;
            if (ParameterSetName == SetValuesParameterSet) newEntity = BuildEntityFromValues();

            var request = new UpdateRequest()
            {
                Target = newEntity
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
                var response = ExecuteOrganizationRequest<UpdateResponse>(request);
                WriteObject(newEntity.ToEntityReference());
            }
        }

        private Entity BuildEntityFromValues()
        {
            var result = new Entity(Table, Id);

            foreach (var attributeName in Values.Keys)
            {
                var attributeValue = Values[attributeName];
                if (attributeValue is PSObject psValue)
                    result.Attributes.Add((string)attributeName, psValue.ImmediateBaseObject);
                else
                    result.Attributes.Add((string)attributeName, attributeValue);
            }

            return result;
        }
    }
}
