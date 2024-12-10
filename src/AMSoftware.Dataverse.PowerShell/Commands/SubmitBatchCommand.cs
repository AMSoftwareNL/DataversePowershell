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
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Xrm.Sdk;
using System.Collections.Immutable;
using Microsoft.Xrm.Sdk.Messages;
using System.ServiceModel;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Submit, "DataverseBatch")]
    public sealed class SubmitBatchCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true)]
        public Guid BatchId { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Transactional { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (Transactional.ToBool() == false)
                ExecuteBatch();
            else
                ExecuteTransaction();
        }

        protected override void EndProcessing()
        {
            Session.Current.Client.ReleaseBatchInfoById(BatchId);

            base.EndProcessing();
        }

        private void ExecuteBatch()
        {
            var response = Session.Current.Client.ExecuteBatch(BatchId);
            if (response != null)
            {
                WriteObject(response.Responses.Select(item => new PSObject(
                    new
                    {
                        Request = Session.Current.Client.GetBatchRequestAtPosition(BatchId, item.RequestIndex),
                        Response = item.Response,
                        Fault = item.Fault
                    })
                ).ToImmutableList(), true);
            }
            else
            {
                WriteError(new ErrorRecord(
                    new Exception("Batch request faulted"),
                    ErrorCode.FaultedBatchExecution,
                    ErrorCategory.InvalidResult,
                    BatchId));
            }
        }

        private void ExecuteTransaction()
        {
            var batch = Session.Current.Client.GetBatchById(BatchId);
            var requests = batch.BatchItems.Select(b => b.Request).ToList();

            var request = new ExecuteTransactionRequest();
            request.ReturnResponses = batch.BatchRequestSettings.ReturnResponses;
            request.Requests = new OrganizationRequestCollection();
            request.Requests.AddRange(requests);

            try
            {
                var response = ExecuteOrganizationRequest<ExecuteTransactionResponse>(request);

                if (response != null)
                {
                    WriteObject(response.Responses.Select((item, index) => new PSObject(
                    new
                    {
                        Request = requests[index],
                        Response = item
                    })
                ).ToImmutableList(), true);
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                int index = ((ExecuteTransactionFault)ex.Detail).FaultedRequestIndex + 1;

                WriteError(new ErrorRecord(
                   new Exception(ex.Detail.Message),
                   ErrorCode.FaultedTransactionExecution,
                   ErrorCategory.InvalidResult,
                   requests[index]));
            }
        }
    }
}
