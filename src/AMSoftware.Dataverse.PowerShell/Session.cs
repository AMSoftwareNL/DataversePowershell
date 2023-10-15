using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Security;

namespace AMSoftware.Dataverse.PowerShell
{
    internal class Session
    {
        private static Session _currentSession;

        private Session(ServiceClient client)
        {
            Client = client;
        }

        internal static void Create(ServiceClient client)
        {
            Current = new Session(client);
        }

        internal static Session Current { 
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

        //public SessionOptions Options { get; set; }
    }
}
