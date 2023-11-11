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

        protected override void Execute()
        {
            var request = new OrganizationRequest(Name);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Parameters)))
            {
                foreach (var parameterName in Parameters.Keys)
                {
                    request.Parameters.Add((string)parameterName, Parameters[parameterName]);
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
