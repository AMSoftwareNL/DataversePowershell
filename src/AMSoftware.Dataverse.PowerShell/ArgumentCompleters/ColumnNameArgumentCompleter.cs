using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace AMSoftware.Dataverse.PowerShell.ArgumentCompleters
{
    public sealed class ColumnNameArgumentCompleter : IArgumentCompleter
    {
        private readonly string[] _filterParameters = new string[] { "Table", "LogicalName", "EntityLogicalName" };

        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            if (fakeBoundParameters == null) throw new ArgumentNullException("fakeBoundParameters");

            IEnumerable<AttributeMetadata> attributeMetadatas = null;
            foreach (string filterParameter in _filterParameters)
            {
                if (fakeBoundParameters.Contains(filterParameter))
                {
                    try
                    {
                        attributeMetadatas = Session.Current.Client.GetAllAttributesForEntity(fakeBoundParameters[filterParameter] as string);
                        break;
                    }
                    catch { }
                }
            }

            if (attributeMetadatas != null)
            {
                return from attributeMetadata in attributeMetadatas
                       where attributeMetadata.LogicalName.StartsWith(wordToComplete.Trim('\'', '"'), StringComparison.InvariantCultureIgnoreCase)
                       orderby attributeMetadata.LogicalName
                       select new CompletionResult($"'{attributeMetadata.LogicalName}'", attributeMetadata.LogicalName, CompletionResultType.Text, attributeMetadata.LogicalName));
            }

            return null;
        }
    }
}
