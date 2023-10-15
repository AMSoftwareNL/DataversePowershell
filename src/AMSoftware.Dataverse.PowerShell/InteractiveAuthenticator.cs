using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace AMSoftware.Dataverse.PowerShell
{
    internal class InteractiveAuthenticator
    {
        private const string Scope = "user_impersonation";
        private const string ExampleClientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";

        private readonly IPublicClientApplication _publicClientApplication;
        private readonly bool _useDeviceCode;

        internal InteractiveAuthenticator(string tenantId, string publicClientId = null, bool useDeviceCode = false)
        {
            _useDeviceCode = useDeviceCode;

            _publicClientApplication = PublicClientApplicationBuilder
                .Create(publicClientId ?? ExampleClientId)
                .WithAuthority(AadAuthorityAudience.AzureAdMyOrg)
                .WithTenantId(tenantId)
                .WithDefaultRedirectUri()
                .WithClientName("AMSoftware Dataverse PowerShell")
                .Build();
        }

        public Task<string> AcquireEnvironmentTokenAsync(string environmentUrl)
        {
            string[] authenticationScopes = InteractiveAuthenticator.BuildScopeFromUrl(environmentUrl);
            var authresult = AcquireEnvironmentTokenAsync(authenticationScopes).GetAwaiter().GetResult();

            return Task.FromResult(authresult.AccessToken);
        }

        private async Task<AuthenticationResult> AcquireEnvironmentTokenAsync(string[] authenticationScopes)
        {
            try
            {
                var accounts = await _publicClientApplication.GetAccountsAsync();

                return await _publicClientApplication
                    .AcquireTokenSilent(authenticationScopes, accounts.FirstOrDefault())
                    .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
                if (_useDeviceCode)
                {
                    return await _publicClientApplication
                        .AcquireTokenWithDeviceCode(authenticationScopes,
                        deviceCodeResult =>
                        {
                            var defaultConsoleColor = Console.ForegroundColor;

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(deviceCodeResult.Message);

                            Console.ForegroundColor = defaultConsoleColor;

                            return Task.FromResult(0);
                        })
                        .ExecuteAsync();
                }
                else
                {
                    return await _publicClientApplication
                        .AcquireTokenInteractive(authenticationScopes)
                        .ExecuteAsync();
                }
            }
        }

        private static string[] BuildScopeFromUrl(string environmentUrl)
        {
            return BuildScopeFromUrl(new Uri(environmentUrl));
        }

        private static string[] BuildScopeFromUrl(Uri environmentUri)
        {
            string scopeAuthority = environmentUri.GetLeftPart(UriPartial.Authority);
            return new string[] { $"{scopeAuthority}/{Scope}" };
        }
    }
}
