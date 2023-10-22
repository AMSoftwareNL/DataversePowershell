using Microsoft.Xrm.Sdk;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/power-apps/developer/data-platform/bypass-custom-business-logic?tabs=sdk
    /// Bypass Custom Plugin Requires: prvBypassCustomPlugins (148a9eaf-d0c4-4196-9852-c3a38e35f6a1)
    /// request.Parameters.Add("BypassCustomPluginExecution", true);
    /// request.Parameters.Add("SuppressCallbackRegistrationExpanderJob", true);
    /// </summary>

    public sealed class BypassLogicParameters
    {
        [Parameter]
        public SwitchParameter BypassSynchronousLogic { get; set; }

        [Parameter]
        public SwitchParameter BypassPowerAutomateFlows { get; set; }

        internal void ApplyBypass(OrganizationRequest request)
        {
            if (request == null) return;

            if (BypassSynchronousLogic.ToBool())
            {
                request.Parameters.Add("BypassCustomPluginExecution", true);
            }

            if (BypassPowerAutomateFlows.ToBool())
            {
                request.Parameters.Add("SuppressCallbackRegistrationExpanderJob", true);
            }
        }
    }
}
