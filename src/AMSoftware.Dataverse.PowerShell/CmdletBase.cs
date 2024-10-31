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
        protected abstract void Execute();

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
            catch (FaultException<OrganizationServiceFault> ex)
            {
                this.WriteExceptionError(ex);
            }
            catch (Exception ex)
            {
                this.WriteExceptionError(ex);
                
                if (ex.InnerException != null)
                {
                    FaultException<OrganizationServiceFault> fault = ex.InnerException as FaultException<OrganizationServiceFault>;
                    if (fault != null)
                    {
                        this.WriteExceptionError(fault);
                    }
                }
            }
        }

        protected override void EndProcessing()
        {
            this.WriteVerboseWithTimestamp(string.Format("{0} end processing.", base.GetType().Name));
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
