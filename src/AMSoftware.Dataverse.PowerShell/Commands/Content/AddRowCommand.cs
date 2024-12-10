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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRow", DefaultParameterSetName = AddObjectParameterSet)]
    [OutputType(typeof(EntityReference))]
    public sealed class AddRowCommand : BatchCmdletBase
    {
        private const string AddObjectParameterSet = "AddObject";
        private const string AddValuesParameterSet = "AddValues";

        [Parameter(Mandatory = true, ParameterSetName = AddObjectParameterSet, ValueFromPipeline = true)]
        [Alias("Row", "Entity")]
        [ValidateNotNullOrEmpty]
        public Entity InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddValuesParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Hashtable Values { get; set; }

        [Parameter(ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        [Parameter(ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Key { get; set; }

        public override void Execute()
        {
            Entity newEntity = InputObject;
            if (ParameterSetName == AddValuesParameterSet) newEntity = BuildEntityFromValues();

            OrganizationRequest request = BuildRequest(newEntity);

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<OrganizationResponse>(request);

                if (response is CreateResponse addObjectCreateResponse)
                    WriteObject(new EntityReference(newEntity.LogicalName, addObjectCreateResponse.id));
                else if (response is UpsertResponse addObjectUpsertResponse)
                    WriteObject(addObjectUpsertResponse.Target);
            }
        }

        private Entity BuildEntityFromValues()
        {
            var result = new Entity(Table);

            // Add Id for Upsert to Entity
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Id)))
                result.Id = Id;

            // Add Alternate Keys for Upsert to Entity
            if (MyInvocation.BoundParameters.ContainsKey(nameof(Key)))
            {
                foreach (var keyAttribute in Key.Keys)
                {
                    var keyValue = Key[keyAttribute];

                    if (keyValue is PSObject psValue)
                        result.KeyAttributes.Add((string)keyAttribute, psValue.ImmediateBaseObject);
                    else
                        result.KeyAttributes.Add((string)keyAttribute, keyValue);
                }
            }

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

        private OrganizationRequest BuildRequest(Entity source)
        {
            OrganizationRequest request;
            if ((source.KeyAttributes != null && source.KeyAttributes.Count != 0) || source.Id != Guid.Empty)
            {
                WriteVerboseWithTimestamp("Using Upsert Request");
                request = new UpsertRequest()
                {
                    Target = source
                };
            }
            else
            {
                WriteVerboseWithTimestamp("Using Create Request");
                request = new CreateRequest()
                {
                    Target = source
                };
            }

            return request;
        }
    }
}
