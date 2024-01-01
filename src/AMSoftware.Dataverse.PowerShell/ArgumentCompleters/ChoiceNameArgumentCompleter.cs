using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace AMSoftware.Dataverse.PowerShell.ArgumentCompleters
{
    public sealed class ChoiceNameArgumentCompleter : IArgumentCompleter
    {
        private readonly IEnumerable<OptionSetMetadataBase> _choicesMetadatas;

        public ChoiceNameArgumentCompleter()
        {
            try
            {
                var retrieveAllRequest = new RetrieveAllOptionSetsRequest()
                {
                    RetrieveAsIfPublished = true
                };
                var retrieveAllResponse = (RetrieveAllOptionSetsResponse)Session.Current.Client.ExecuteOrganizationRequest(retrieveAllRequest);

                _choicesMetadatas = retrieveAllResponse.OptionSetMetadata;
            }
            catch { }
        }

        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            if (fakeBoundParameters == null) throw new ArgumentNullException("fakeBoundParameters");

            if (_choicesMetadatas != null)
            {
                return from choiceMetadata in _choicesMetadatas
                       where choiceMetadata.Name.StartsWith(wordToComplete.Trim('\'', '"'), StringComparison.InvariantCultureIgnoreCase)
                       orderby choiceMetadata.Name
                       select new CompletionResult($"'{choiceMetadata.Name}'", choiceMetadata.Name, CompletionResultType.Text, choiceMetadata.Name);
            }

            return null;
        }
    }
}
