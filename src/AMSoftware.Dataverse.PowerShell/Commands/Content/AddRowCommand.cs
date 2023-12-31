﻿using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client;
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

        [Parameter(Mandatory = true, ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Values { get; set; }

        [Parameter(ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        [Parameter(ParameterSetName = AddValuesParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Key { get; set; }

        protected override void Execute()
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
                    result.KeyAttributes.Add((string)keyAttribute, Key[keyAttribute]);
                }
            }

            foreach (var attributeName in Values.Keys)
            {
                result.Attributes.Add((string)attributeName, Values[attributeName]);
            }

            return result;
        }

        private OrganizationRequest BuildRequest(Entity source)
        {
            OrganizationRequest request;
            if (source.KeyAttributes != null && source.KeyAttributes.Count != 0 || source.Id != Guid.Empty)
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
