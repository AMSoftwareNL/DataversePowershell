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
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Request, "DataverseBatch")]
    [OutputType(typeof(Guid))]
    public sealed class RequestBatchCommand : CmdletBase
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        [PSDefaultValue(Value = true)]
        public SwitchParameter ReturnResults { get; set; }

        [Parameter]
        [PSDefaultValue(Value = false)]
        public SwitchParameter ContinueOnError { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (string.IsNullOrWhiteSpace(Name))
                Name = $"Request-DataverseBatch {Session.Current.UserId} {DateTime.UtcNow:s}";

            bool returnResults = true;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(ReturnResults)))
                returnResults = ReturnResults.ToBool();

            bool continueOnError = false;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(ContinueOnError)))
                continueOnError = ContinueOnError.ToBool();

            Guid batchId = Session.Current.Client.CreateBatchOperationRequest(Name, ReturnResults, ContinueOnError);

            if (batchId == Guid.Empty)
            {
                WriteError(new ErrorRecord(
                        new ArgumentException($"Batch initialization faulted'."),
                        ErrorCode.FaultedBatchInitialization,
                        ErrorCategory.InvalidOperation, Session.Current.Client));
            }
            else
            {
                WriteObject(batchId);
            }
        }

        public override void Execute()
        {
        }
    }
}
