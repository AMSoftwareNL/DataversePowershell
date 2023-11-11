using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace AMSoftware.Dataverse.PowerShell
{
    internal sealed class Session
    {
        private static Session _currentSession;

        private Session(ServiceClient client)
        {
            InitializeSession(client);

            Client = client;
        }

        internal static void Create(ServiceClient client)
        {
            Current = new Session(client);
        }

        internal static Session Current
        {
            get
            {
                if (_currentSession == null)
                {
                    throw new Exception("Not connected to Dataverse Environment. Create a connection using Connect-DataverseEnvironment.");
                }
                return _currentSession;
            }
            private set
            {
                _currentSession = value;
            }
        }

        public ServiceClient Client { get; private set; }

        #region User Settings
        public Guid UserId { get; internal set; }

        public int? UserLanguageId { get; internal set; }

        public int? UserLocaleId { get; internal set; }

        public Guid BusinessUnitId { get; internal set; }
        #endregion

        #region Organization Settings
        public int OrganizationLanguageId { get; internal set; }

        public int OrganizationLocaleId { get; internal set; }

        public Guid SystemUserId { get; set; }
        #endregion

        public int LanguageId
        {
            get
            {
                return UserLanguageId ?? OrganizationLanguageId;
            }
        }

        public int LocaleId
        {
            get
            {
                return UserLocaleId ?? OrganizationLocaleId;
            }
        }

        public string ActiveSolution { get; set; }

        public void Refresh()
        {
            InitializeSession(Client);
        }

        private void InitializeSession(ServiceClient client)
        {
            var whoAmIResponse = (WhoAmIResponse)client.ExecuteOrganizationRequest(new WhoAmIRequest(), "Initialize Session - WhoAmI");
            BusinessUnitId = whoAmIResponse.BusinessUnitId;

            var usersettingsResponse = (RetrieveResponse)client.ExecuteOrganizationRequest(new RetrieveRequest()
            {
                Target = new EntityReference("usersettings", whoAmIResponse.UserId),
                ColumnSet = new ColumnSet("uilanguageid", "localeid")
            }, "Initialize Session - User Settings");
            UserLanguageId = usersettingsResponse.Entity.GetAttributeValue<int?>("uilanguageid");
            UserLocaleId = usersettingsResponse.Entity.GetAttributeValue<int?>("localeid");

            var organizationResponse = (RetrieveResponse)client.ExecuteOrganizationRequest(new RetrieveRequest()
            {
                Target = new EntityReference("organization", whoAmIResponse.OrganizationId),
                ColumnSet = new ColumnSet("languagecode", "localeid", "systemuserid")
            }, "Initialize Session - Organization");
            OrganizationLanguageId = organizationResponse.Entity.GetAttributeValue<int>("languagecode");
            OrganizationLocaleId = organizationResponse.Entity.GetAttributeValue<int>("localeid");
            SystemUserId = organizationResponse.Entity.GetAttributeValue<Guid>("systemuserid");
        }
    }
}
