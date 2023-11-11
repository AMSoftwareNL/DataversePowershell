using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class RequestCmdletBase : CmdletBase, IDynamicParameters
    {
        protected OptionalRequestParameters RequestParameters { get; private set; }

        public object GetDynamicParameters()
        {
            RequestParameters = new OptionalRequestParameters(this);
            return RequestParameters;
        }
    }
}
