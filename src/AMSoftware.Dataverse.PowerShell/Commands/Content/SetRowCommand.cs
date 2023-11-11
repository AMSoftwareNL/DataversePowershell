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

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SetObjectParameterSet, ValueFromPipeline = true)]
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

        protected override void Execute()
        {
            Entity newEntity = InputObject;
            if (ParameterSetName == SetValuesParameterSet) newEntity = BuildEntityFromValues();

            OrganizationRequest request = new UpdateRequest()
            {
                Target = newEntity
            };

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
                result.Attributes.Add((string)attributeName, Values[attributeName]);
            }

            return result;
        }
    }
}
