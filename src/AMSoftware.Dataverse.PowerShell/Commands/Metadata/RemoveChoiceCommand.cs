using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Remove, "DataverseChoice", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
    public sealed class RemoveChoiceCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        protected override void Execute()
        {
            var choiceName = Name;

            if (ShouldProcess(choiceName))
            {
                OrganizationRequest request = new DeleteOptionSetRequest()
                {
                    Name = choiceName
                };

                var response = ExecuteOrganizationRequest<DeleteOptionSetResponse>(request);
            }
        }
    }
}
