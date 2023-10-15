using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class CmdletBase : PSCmdlet
    {
        protected virtual void BeginExecution()
        {
        }

        protected virtual void Execute()
        {
        }

        protected virtual void EndExecution()
        {
        }

        protected override void BeginProcessing()
        {
            if (string.IsNullOrEmpty(base.ParameterSetName))
            {
                this.WriteVerboseWithTimestamp(string.Format("{0} begin processing without ParameterSet.", base.GetType().Name));
            }
            else
            {
                this.WriteVerboseWithTimestamp(string.Format("{0} begin processing with ParameterSet '{1}'.", base.GetType().Name, base.ParameterSetName));
            }
            base.BeginProcessing();
            this.BeginExecution();
        }

        protected override void ProcessRecord()
        {
            try
            {
                this.WriteVerboseWithTimestamp(string.Format("{0} process record.", base.GetType().Name));

                base.ProcessRecord();
                this.Execute();
            }
            catch (PipelineStoppedException)
            {
            }
            catch (Exception ex)
            {
                this.WriteExceptionError(ex);
            }
        }

        protected override void EndProcessing()
        {
            this.WriteVerboseWithTimestamp(string.Format("{0} end processing.", base.GetType().Name));
            this.EndExecution();
            base.EndProcessing();
        }

        protected void WriteDebugWithTimestamp(string message)
        {
            this.WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected void WriteDebugWithTimestamp(string message, params object[] args)
        {
            this.WriteDebug(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        protected void WriteErrorWithTimestamp(string message)
        {
            this.WriteError(new ErrorRecord(new Exception(string.Format("{0:T} - {1}", DateTime.Now, message)), string.Empty, ErrorCategory.NotSpecified, null));
        }

        protected void WriteVerboseWithTimestamp(string message)
        {
            this.WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected void WriteVerboseWithTimestamp(string message, params object[] args)
        {
            this.WriteVerbose(string.Format("{0:T} - {1}", DateTime.Now, string.Format(message, args)));
        }

        protected void WriteWarningWithTimestamp(string message)
        {
            this.WriteWarning(string.Format("{0:T} - {1}", DateTime.Now, message));
        }

        protected virtual void WriteExceptionError(Exception ex)
        {
            this.WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
        }
    }
}
