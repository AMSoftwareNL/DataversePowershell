using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRow")]
    [OutputType(typeof(EntityReference))]
    public sealed class RemoveRowCommand : BatchCmdletBase, IDynamicParameters
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        private BypassLogicParameters _bypassContext;
        public object GetDynamicParameters()
        {
            _bypassContext = new BypassLogicParameters();
            return _bypassContext;
        }

        protected override void Execute()
        {
            EntityReference rowReference = new EntityReference(Table, Id);

            OrganizationRequest request = new DeleteRequest()
            {
                Target = rowReference
            };
            _bypassContext.ApplyBypass(request);

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = Session.Current.Client.ExecuteOrganizationRequest(request, MyInvocation.MyCommand.Name);
            }
        }
    }
}
