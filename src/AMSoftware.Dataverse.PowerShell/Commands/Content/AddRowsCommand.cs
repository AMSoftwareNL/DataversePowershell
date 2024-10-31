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
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Rows", "Entities")]
        [ValidateNotNullOrEmpty]
        public Entity[] InputObject { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Upsert { get; set; }

        protected override void Execute()
        {
            if (Upsert.ToBool())
            {
                var request = new UpsertMultipleRequest()
                {
                    Targets = new EntityCollection(InputObject.ToList())
                };

                if (UseBatch)
                {
                    AddOrganizationRequestToBatch(request);
                }
                else
                {
                    var response = ExecuteOrganizationRequest<UpsertMultipleResponse>(request);

                    for (int i = 0; i < response.Results.Length; i++)
                    {
                        WriteObject(response.Results[i].Target);
                    }
                }
            } else
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
}
