﻿using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Management.Automation;
using System.Security;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommunications.Connect, "DataverseEnvironment", DefaultParameterSetName = InteractiveParameterSet)]
    [OutputType(typeof(ServiceClient))]
    public sealed class ConnectEnvironmentCommand : CmdletBase
    {
        private const string InteractiveParameterSet = "Interactive";
        private const string ConnectionstringParameterSet = "Connectionstring";
        private const string ClientSecretParameterSet = "ClientSecret";
        private const string ServiceClientParameterSet = "ServiceClient";

        [Parameter(Mandatory = true, ParameterSetName = ServiceClientParameterSet)]
        [ValidateNotNullOrEmpty]
        public ServiceClient ServiceClient { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ConnectionstringParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Connectionstring { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = InteractiveParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = ClientSecretParameterSet)]
        public Uri EnvironmentUrl { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = InteractiveParameterSet)]
        [ValidateNotNullOrEmpty]
        public string TenantId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ClientSecretParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = InteractiveParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ClientId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ClientSecretParameterSet)]
        [ValidateNotNull]
        public SecureString ClientSecret { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = InteractiveParameterSet)]
        public SwitchParameter UseDeviceCode { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            ServiceClient client = null;
            switch (ParameterSetName)
            {
                case ConnectionstringParameterSet:
                    client = new ServiceClient(Connectionstring);
                    break;
                case ClientSecretParameterSet:
                    client = new ServiceClient(EnvironmentUrl, ClientId, ClientSecret, true);
                    break;
                case InteractiveParameterSet:
                    InteractiveAuthenticator authenticator = new InteractiveAuthenticator(TenantId, ClientId, UseDeviceCode.ToBool());
                    client = new ServiceClient(EnvironmentUrl, authenticator.AcquireEnvironmentTokenAsync, true);
                    break;
                case ServiceClientParameterSet:
                    client = ServiceClient;
                    break;
                default:
                    break;
            }

            if (client != null && client.IsReady)
            {
                Session.Create(client);

                this.SessionState.PSVariable.Set(new PSVariable("DataverseSession", Session.Current));
                this.SessionState.PSVariable.Set(new PSVariable("DataverseClient", Session.Current.Client));
            }
            else
            {
                if (client?.LastException == null) throw new Exception();
                else throw client.LastException;
            }
        }

        protected override void Execute()
        {
        }

        protected override void EndProcessing()
        {
            WriteObject(Session.Current.Client);

            base.EndProcessing();
        }
    }
}
