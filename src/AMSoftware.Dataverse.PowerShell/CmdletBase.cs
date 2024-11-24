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
using System;
using System.Management.Automation;
using System.ServiceModel;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class CmdletBase : PSCmdlet
    {
        public virtual void Execute()
        {
            WriteVerboseWithTimestamp("Invoked CmdletBase Execute");

            WriteObject("WriteObject From Base");
        }

        protected override void BeginProcessing()
        {
            if (string.IsNullOrEmpty(ParameterSetName))
            {
                WriteVerboseWithTimestamp(string.Format("{0} begin processing without ParameterSet.", GetType().Name));
            }
            else
            {
                WriteVerboseWithTimestamp(string.Format("{0} begin processing with ParameterSet '{1}'.", GetType().Name, ParameterSetName));
            }
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            try
            {
                WriteVerboseWithTimestamp(string.Format("{0} process record.", GetType().Name));

                base.ProcessRecord();

                Execute();
            }
            catch (FaultException<OrganizationServiceFault> ex) when (!IsTerminatingError(ex))
            {
                WriteExceptionError(ex);
            }
            catch (Exception ex) when (!IsTerminatingError(ex))
            {
                WriteExceptionError(ex);

                if (ex.InnerException != null && ex.InnerException is FaultException<OrganizationServiceFault> fault)
                {
                    WriteExceptionError(fault);
                }
            }
        }

        protected override void EndProcessing()
        {
            WriteVerboseWithTimestamp(string.Format("{0} end processing.", GetType().Name));

            base.EndProcessing();
        }

        protected new void WriteObject(object sendToPipeline)
        {
            //FlushDebugMessages();
            //SanitizeOutput(sendToPipeline);
            base.WriteObject(sendToPipeline);
        }

        protected new void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            //FlushDebugMessages();
            //SanitizeOutput(sendToPipeline);
            base.WriteObject(sendToPipeline, enumerateCollection);
        }

        protected bool IsTerminatingError(Exception ex)
        {
            var pipelineStoppedEx = ex as PipelineStoppedException;
            if (pipelineStoppedEx != null && pipelineStoppedEx.InnerException == null)
            {
                return true;
            }

            return false;
        }

        protected void WriteDebugWithTimestamp(string message)
        {
            WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected void WriteDebugWithTimestamp(string message, params object[] args)
        {
            WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        protected void WriteErrorWithTimestamp(string message)
        {
            WriteError(new ErrorRecord(new Exception(string.Format("{0:T} - {1}", DateTime.Now, message)), string.Empty, ErrorCategory.NotSpecified, null));
        }

        protected void WriteVerboseWithTimestamp(string message)
        {
            WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected void WriteVerboseWithTimestamp(string message, params object[] args)
        {
            WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        protected void WriteWarningWithTimestamp(string message)
        {
            WriteWarning(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected virtual void WriteExceptionError(Exception ex)
        {
            WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
        }
    }
}
