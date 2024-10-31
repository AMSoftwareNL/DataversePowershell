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

        public Guid SystemUserId { get; internal set; }
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
            UserId = whoAmIResponse.UserId;
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
