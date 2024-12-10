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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRows", DefaultParameterSetName = AddObjectParameterSet)]
    [OutputType(typeof(EntityReference))]
    public sealed class AddRowsCommand : BatchCmdletBase
    {
        private const string AddObjectParameterSet = "AddObject";
        private const string AddValuesParameterSet = "AddValues";

        private readonly List<Entity> _rowsToProcess = [];

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = AddObjectParameterSet)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddObjectParameterSet)]
        public SwitchParameter Upsert { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddValuesParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Hashtable Values { get; set; }

        public override void Execute()
        {
            switch (ParameterSetName)
            {
                case AddObjectParameterSet:
                    _rowsToProcess.AddRange(InputObject);
                    break;
                case AddValuesParameterSet:
                    _rowsToProcess.Add(BuildEntityFromValues());
                    break;
            }
        }

        protected override void EndProcessing()
        {
            var entityName = _rowsToProcess.FirstOrDefault()?.LogicalName ?? Table;
            var targetCollection = new EntityCollection(_rowsToProcess)
            {
                EntityName = entityName
            };

            OrganizationRequest request;
            if (Upsert.ToBool())
            {
                request = new UpsertMultipleRequest()
                {
                    Targets = targetCollection
                };
            }
            else
            {
                request = new CreateMultipleRequest()
                {
                    Targets = targetCollection
                };
            }

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                if (Upsert.ToBool())
                {
                    var response = ExecuteOrganizationRequest<UpsertMultipleResponse>(request);
                    WriteObject(response.Results.Select(r => r.Target), true);
                }
                else
                {
                    var response = ExecuteOrganizationRequest<CreateMultipleResponse>(request);
                    WriteObject(response.Ids.Select(id => new EntityReference(entityName, id)), true);
                }
            }

            base.EndProcessing();
        }

        private Entity BuildEntityFromValues()
        {
            var result = new Entity(Table);

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
