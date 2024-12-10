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
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell
{
    internal sealed class InteractiveAuthenticator
    {
        private const string Scope = "user_impersonation";

        private readonly IPublicClientApplication _publicClientApplication;

        internal InteractiveAuthenticator(string publicClientId)
        {
            _publicClientApplication = PublicClientApplicationBuilder
                .Create(publicClientId)
                .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                .WithDefaultRedirectUri()
                .WithClientName("AMSoftware Dataverse PowerShell")
                .WithClientVersion("0.6.0")
                .WithLogging(MSALIdentityLogger.Instance.Log, LogLevel.Always, false, false)
                .Build();
        }

        public Task<string> AcquireEnvironmentTokenAsync(string environmentUrl)
        {
            string[] authenticationScopes = BuildScopeFromUrl(environmentUrl);
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
