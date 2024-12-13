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
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Concurrent;
using System.Management.Automation;
using System.ServiceModel;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class CmdletBase : PSCmdlet
    {
        private ConcurrentQueue<PSLogEntry> LogMessages { get; set; } = new ConcurrentQueue<PSLogEntry>();

        public virtual void Execute()
        {
            // Do nothing
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

            SetupLogIntercept();

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
            TeardownLogIntercept();
            FlushLogMessages();

            WriteVerboseWithTimestamp(string.Format("{0} end processing.", GetType().Name));

            base.EndProcessing();
        }

        private void SetupLogIntercept()
        {
            MSALIdentityLogger.Instance.AddContext((entry) =>
            {
                LogMessages.Enqueue(entry);
            });
            ServiceClientLogger.Instance.AddContext((entry) =>
            {
                LogMessages.Enqueue(entry);
            });
        }

        private static void TeardownLogIntercept()
        {
            MSALIdentityLogger.Instance.RemoveContext();
            ServiceClientLogger.Instance.RemoveContext();
        }

        private void FlushLogMessages()
        {
            while (LogMessages.TryDequeue(out PSLogEntry entry))
            {
                switch (entry.LogLevel)
                {
                    case LogLevel.Warning:
                        base.WriteWarning(entry.Message);
                        break;
                    case LogLevel.Debug:
                        base.WriteDebug(entry.Message);
                        break;
                    case LogLevel.Trace:
                        base.WriteVerbose(entry.Message);
                        break;
                }
            }
        }

        protected new void ThrowTerminatingError(ErrorRecord errorRecord)
        {
            FlushLogMessages();
            base.ThrowTerminatingError(errorRecord);
        }

        protected new void WriteCommandDetail(string text)
        {
            FlushLogMessages();
            base.WriteCommandDetail(text);
        }

        protected new void WriteDebug(string text)
        {
            FlushLogMessages();
            base.WriteDebug(text);
        }

        protected new void WriteError(ErrorRecord errorRecord)
        {
            FlushLogMessages();
            base.WriteError(errorRecord);
        }

        protected new void WriteInformation(object messageData, string[] tags)
        {
            FlushLogMessages();
            base.WriteInformation(messageData, tags);
        }

        protected new void WriteInformation(InformationRecord informationRecord)
        {
            FlushLogMessages();
            base.WriteInformation(informationRecord);
        }

        protected new void WriteObject(object sendToPipeline, bool enumerateCollection)
        {
            FlushLogMessages();
            base.WriteObject(sendToPipeline, enumerateCollection);
        }

        protected new void WriteObject(object sendToPipeline)
        {
            FlushLogMessages();
            base.WriteObject(sendToPipeline);
        }

        protected new void WriteProgress(ProgressRecord progressRecord)
        {
            FlushLogMessages();
            base.WriteProgress(progressRecord);
        }

        protected new void WriteVerbose(string text)
        {
            FlushLogMessages();
            base.WriteVerbose(text);
        }

        protected new void WriteWarning(string text)
        {
            FlushLogMessages();
            base.WriteWarning(text);
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
