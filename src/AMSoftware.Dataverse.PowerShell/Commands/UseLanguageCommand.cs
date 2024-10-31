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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsOther.Use, "DataverseLanguage")]
    [OutputType(typeof(Session))]
    public sealed class UseLanguage : RequestCmdletBase
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        public int Language { get; set; }

        protected override void Execute()
        {
            var installedLanguagesResponse = ExecuteOrganizationRequest<RetrieveAvailableLanguagesResponse>(
                new RetrieveAvailableLanguagesRequest());

            if (installedLanguagesResponse.LocaleIds.Any(languagePack => languagePack == Language))
            {
                var request = new UpsertRequest()
                {
                    Target = new Entity("usersettings", Session.Current.UserId)
                    {
                        Attributes = {
                            {  "languageid", Language }
                        }
                    }
                };

                ExecuteOrganizationRequest<OrganizationResponse>(request);
                Session.Current.Refresh();

                WriteObject(Session.Current);
            }
            else
            {
                WriteError(new ErrorRecord(
                        new ArgumentException($"Language {Language} is not installed."),
                        ErrorCode.LanguageNotInstalled,
                        ErrorCategory.InvalidArgument,
                        Language));
            }
        }
    }
}
