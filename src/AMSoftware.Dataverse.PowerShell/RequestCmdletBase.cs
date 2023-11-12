using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell
{
    public abstract class RequestCmdletBase : CmdletBase, IDynamicParameters
    {
        protected OptionalRequestParameters RequestParameters { get; private set; }

        public virtual object GetDynamicParameters()
        {
            RequestParameters = new OptionalRequestParameters(this);
            return RequestParameters;
        }

        protected virtual TResponse ExecuteOrganizationRequest<TResponse>(OrganizationRequest request) where TResponse : OrganizationResponse
        {
            RequestParameters.UseOptionalParameters(request);

            var response = (TResponse)Session.Current.Client.ExecuteOrganizationRequest(
                    request, MyInvocation.MyCommand.Name);

            if (response == null)
            {
                throw Session.Current.Client.LastException;
            }

            return response;
        }
    }
}
