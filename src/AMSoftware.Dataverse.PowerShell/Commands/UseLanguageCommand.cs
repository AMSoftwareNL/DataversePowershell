using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsOther.Use, "DataverseLanguage")]
    [OutputType(typeof(Session))]
    public sealed class UseLanguage : CmdletBase
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        public int Language { get; set; }

        protected override void Execute()
        {
            var installedLanguagesResponse = (RetrieveAvailableLanguagesResponse)Session.Current.Client.ExecuteOrganizationRequest(
                new RetrieveAvailableLanguagesRequest(), MyInvocation.MyCommand.Name);

            if (installedLanguagesResponse.LocaleIds.Any(languagePack => languagePack == Language))
            {
                var request = new UpsertRequest()
                {
                    Target = new Microsoft.Xrm.Sdk.Entity("usersettings", Session.Current.UserId)
                    {
                        Attributes = {
                            {  "languageid", Language }
                        }
                    }
                };

                Session.Current.Client.ExecuteOrganizationRequest(request, MyInvocation.MyCommand.Name);
                Session.Current.Refresh();

                WriteObject(Session.Current);
            } else
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
