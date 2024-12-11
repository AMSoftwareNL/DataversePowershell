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
using Microsoft.Xrm.Sdk;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class RequestCmdletBase : CmdletBase
    {
        /// Use optional parameters
        /// https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optional-parameters?tabs=sdk

        [Parameter(DontShow = true)]
        [ValidateNotNullOrEmpty]
        public string Solution { get; set; }

        [Parameter(DontShow = true)]
        [ValidateNotNullOrEmpty]
        public string SharedTag { get; set; }

        [Parameter(DontShow = true)]
        [ValidateNotNullOrEmpty]
        public string Partition { get; set; }

        [Parameter(DontShow = true)]
        public SwitchParameter FailOnDuplicateDetection { get; set; }

        [Parameter(DontShow = true)]
        public SwitchParameter BypassSynchronousLogic { get; set; }

        [Parameter(DontShow = true)]
        public SwitchParameter BypassPowerAutomateFlows { get; set; }

        protected virtual TResponse ExecuteOrganizationRequest<TResponse>(OrganizationRequest request) where TResponse : OrganizationResponse
        {
            UseOptionalParameters(request);
            LogForDebug(request);

            var response = (TResponse)Session.Current.Client.ExecuteOrganizationRequest(
                    request, MyInvocation.MyCommand.Name);

            if (response == null && Session.Current.Client.LastException != null)
            {
                throw Session.Current.Client.LastException;
            }

            LogForDebug(response);

            return response;
        }

        private void LogForDebug<T>(T graph)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var debugStringBuilder = new StringBuilder();

            using (var xmlWriter = XmlWriter.Create(debugStringBuilder, new XmlWriterSettings()
            {
                Indent = true
            })) {
                serializer.WriteObject(xmlWriter, graph);
            }

            WriteDebugWithTimestamp(graph.GetType().Name);
            WriteDebug(debugStringBuilder.ToString());
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
