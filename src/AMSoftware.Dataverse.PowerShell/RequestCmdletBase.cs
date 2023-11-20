using Microsoft.Xrm.Sdk;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class RequestCmdletBase : CmdletBase
    {
        /// Use optional parameters
        /// https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters?tabs=sdk

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string Solution { get; set; }

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string SharedTag { get; set; }

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string Partition { get; set; }

        [Parameter]
        public SwitchParameter FailOnDuplicateDetection { get; set; }

        [Parameter]
        public SwitchParameter BypassSynchronousLogic { get; set; }

        [Parameter]
        public SwitchParameter BypassPowerAutomateFlows { get; set; }

        protected virtual TResponse ExecuteOrganizationRequest<TResponse>(OrganizationRequest request) where TResponse : OrganizationResponse
        {
            UseOptionalParameters(request);

            var response = (TResponse)Session.Current.Client.ExecuteOrganizationRequest(
                    request, MyInvocation.MyCommand.Name);

            if (response == null)
            {
                throw Session.Current.Client.LastException;
            }

            return response;
        }

        private void UseOptionalParameters(OrganizationRequest request)
        {
            if (request == null) return;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Solution)))
            {
                request.Parameters.Add("SolutionUniqueName", Solution);
            }
            else if (!string.IsNullOrEmpty(Session.Current.ActiveSolution))
            {
                request.Parameters.Add("SolutionUniqueName", Session.Current.ActiveSolution);
            }

            if (MyInvocation.BoundParameters.ContainsKey(nameof(SharedTag)))
                request.Parameters.Add("tag", SharedTag);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Partition)))
                request.Parameters.Add("partitionId", Partition);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(FailOnDuplicateDetection)))
                request.Parameters.Add("SuppressDuplicateDetection", !FailOnDuplicateDetection.ToBool());

            if (MyInvocation.BoundParameters.ContainsKey(nameof(BypassSynchronousLogic)))
                request.Parameters.Add("BypassCustomPluginExecution", BypassSynchronousLogic.ToBool());

            if (MyInvocation.BoundParameters.ContainsKey(nameof(BypassPowerAutomateFlows)))
                request.Parameters.Add("SuppressCallbackRegistrationExpanderJob", BypassPowerAutomateFlows.ToBool());
        }
    }
}
