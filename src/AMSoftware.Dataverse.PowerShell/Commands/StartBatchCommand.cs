using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsLifecycle.Start, "DataverseBatch")]
    [OutputType(typeof(Guid))]
    public sealed class StartBatchCommand : CmdletBase
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
                Name = $"Start-DataverseRows {Session.Current.Client.OAuthUserId} {DateTime.UtcNow:s}";

            bool returnResults = true;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(ReturnResults)))
                returnResults = ReturnResults.ToBool();

            bool continueOnError = false;
            if (MyInvocation.BoundParameters.ContainsKey(nameof(ContinueOnError)))
                continueOnError = ContinueOnError.ToBool();

            Session.Current.Client.CreateBatchOperationRequest(Name, ReturnResults, ContinueOnError);
        }

        protected override void Execute()
        {
        }
    }
}
