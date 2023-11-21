using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRows")]
    [OutputType(typeof(EntityReference))]
    public sealed class AddRowsCommand : BatchCmdletBase
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        protected override void Execute()
        {
            var request = new CreateMultipleRequest()
            {
                Targets = new EntityCollection(InputObject.ToList())
            };

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<CreateMultipleResponse>(request);

                for (int i = 0; i < response.Ids.Length; i++)
                {
                    WriteObject(new EntityReference(InputObject[i].LogicalName, response.Ids[i]));
                }
            }
        }
    }
}
