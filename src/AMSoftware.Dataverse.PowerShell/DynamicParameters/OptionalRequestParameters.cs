using Microsoft.Xrm.Sdk;
using System.Diagnostics;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    /// <summary>
    /// Use optional parameters
    /// https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters?tabs=sdk
    /// </summary>

    public class OptionalRequestParameters
    {
        protected readonly PSCmdlet _cmdletContext;

        internal OptionalRequestParameters(PSCmdlet cmdletContext)
        {
            _cmdletContext = cmdletContext;
        }

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

        internal void UseOptionalParameters(OrganizationRequest request)
        {
            if (request == null) return;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Solution)))
            {
                request.Parameters.Add("SolutionUniqueName", Solution);
            }
            else if (!string.IsNullOrEmpty(Session.Current.ActiveSolution))
            {
                request.Parameters.Add("SolutionUniqueName", Session.Current.ActiveSolution);
            }

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(SharedTag)))
                request.Parameters.Add("tag", SharedTag);

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Partition)))
                request.Parameters.Add("partitionId", Partition);

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(FailOnDuplicateDetection)))
                request.Parameters.Add("SuppressDuplicateDetection", !FailOnDuplicateDetection.ToBool());

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(BypassSynchronousLogic)))
                request.Parameters.Add("BypassCustomPluginExecution", BypassSynchronousLogic.ToBool());

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(BypassPowerAutomateFlows)))
                request.Parameters.Add("SuppressCallbackRegistrationExpanderJob", BypassPowerAutomateFlows.ToBool());
        }
    }
}
