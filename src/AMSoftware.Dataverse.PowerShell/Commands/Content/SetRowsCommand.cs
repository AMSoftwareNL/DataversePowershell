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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Set, "DataverseRows", DefaultParameterSetName = SetObjectParameterSet)]
    [OutputType(typeof(EntityReference))]
    public sealed class SetRowsCommand : BatchCmdletBase
    {
        private const string SetObjectParameterSet = "SetObject";
        private const string SetValuesParameterSet = "SetValues";

        private List<Entity> _rowsToProcess = [];

        [Parameter(Mandatory = true, ParameterSetName = SetObjectParameterSet, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetValuesParameterSet, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetValuesParameterSet, ValueFromPipelineByPropertyName = true)]
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
            switch (ParameterSetName)
            {
                case SetObjectParameterSet:
                    _rowsToProcess.AddRange(InputObject);
                    break;
                case SetValuesParameterSet:
                    _rowsToProcess.Add(BuildEntityFromValues());
                    break;
            }
        }

        protected override void EndProcessing()
        {
            var request = new UpdateMultipleRequest()
            {
                Targets = new EntityCollection(_rowsToProcess) {
                    EntityName = _rowsToProcess.FirstOrDefault()?.LogicalName ?? Table
                }
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
                var _ = ExecuteOrganizationRequest<UpdateMultipleResponse>(request);

                WriteObject(_rowsToProcess.Select(r=> r.ToEntityReference()), true);
            }

            base.EndProcessing();
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
