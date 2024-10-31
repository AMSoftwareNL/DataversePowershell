/* 
PowerShell Module for Power Platform Dataverse
Copyright(C) 2024  AMSoftwareNL

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
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
