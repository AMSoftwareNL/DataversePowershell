using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRow", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
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

        private OptionalRequestParameters _optionalRequestParameters;
        public object GetDynamicParameters()
        {
            _optionalRequestParameters = new OptionalRequestParameters(this);
            return _optionalRequestParameters;
        }

        protected override void Execute()
        {
            EntityReference rowReference = new EntityReference(Table, Id);

            if (ShouldProcess($"{Table}: {Id}"))
            {
                OrganizationRequest request = new DeleteRequest()
                {
                    Target = rowReference
                };
                _optionalRequestParameters.UseOptionalParameters(request);

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
}
