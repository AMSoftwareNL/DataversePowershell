using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Set, "DataverseRows")]
    [OutputType(typeof(EntityReference))]
    public sealed class SetRowsCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [PSDefaultValue(Value = ConcurrencyBehavior.Default)]
        public ConcurrencyBehavior Behavior { get; set; }

        protected override void Execute()
        {
            var request = new UpdateMultipleRequest()
            {
                Targets = new EntityCollection(InputObject.ToList())
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
                var response = ExecuteOrganizationRequest<UpdateMultipleResponse>(request);

                for (int i = 0; i < InputObject.Length; i++)
                {
                    WriteObject(InputObject[i].ToEntityReference());
                }
            }
        }
    }
}
