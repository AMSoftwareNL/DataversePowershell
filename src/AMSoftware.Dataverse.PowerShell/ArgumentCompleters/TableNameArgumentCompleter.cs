using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;

namespace AMSoftware.Dataverse.PowerShell.ArgumentCompleters
{
    public sealed class TableNameArgumentCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName, string parameterName, string wordToComplete, CommandAst commandAst, IDictionary fakeBoundParameters)
        {
            if (fakeBoundParameters == null) throw new ArgumentNullException("fakeBoundParameters");

            IEnumerable<EntityMetadata> entitiesMetadatas = null;
            try
            {
                entitiesMetadatas = Session.Current.Client.GetAllEntityMetadata(false, EntityFilters.Entity);
            }
            catch { }

            if (entitiesMetadatas != null)
            {
                return from entityMetadata in entitiesMetadatas
                       where entityMetadata.LogicalName.StartsWith(wordToComplete.Trim('\'', '"'), StringComparison.InvariantCultureIgnoreCase)
                       orderby entityMetadata.LogicalName
                       select new CompletionResult($"'{entityMetadata.LogicalName}'", entityMetadata.LogicalName, CompletionResultType.Text, entityMetadata.LogicalName);
            }

            return null;
        }
    }
}
