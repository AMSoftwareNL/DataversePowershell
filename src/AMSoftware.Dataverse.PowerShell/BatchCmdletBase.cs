using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class BatchCmdletBase : RequestCmdletBase
    {
        [Parameter]
        [ValidateNotNullOrEmpty]
        public Guid BatchId { get; set; }

        protected RequestBatch Batch { get; private set; }

        protected bool UseBatch
        {
            get
            {
                return MyInvocation.BoundParameters.ContainsKey(nameof(BatchId));
            }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (UseBatch)
            {
                // TODO: Use own implementation to support transactions
                // TODO: Use own implementation to support batches with 1000+ requests (auto paging/chunking)
                Batch = Session.Current.Client.GetBatchById(BatchId);
            }
        }

        protected void AddOrganizationRequestToBatch(OrganizationRequest request)
        {
            Batch.BatchItems.Add(new BatchItemOrganizationRequest()
            {
                Request = request
            });
        }
    }
}
