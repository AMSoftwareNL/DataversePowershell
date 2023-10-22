using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRow")]
    [OutputType(typeof(EntityReference))]
    public sealed class RemoveRowCommand : CmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        protected override void Execute()
        {
            EntityReference rowReference = new EntityReference(Table, Id);

            OrganizationRequest request = new DeleteRequest() { 
                Target = rowReference
            };
            var response = Session.Current.Client.ExecuteOrganizationRequest(request, MyInvocation.MyCommand.Name);
        }
    }
}
