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
            if (fakeBoundParameters == null) throw new ArgumentNullException(nameof(fakeBoundParameters));

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
                       select new CompletionResult($"'{attributeMetadata.LogicalName}'", attributeMetadata.LogicalName, CompletionResultType.Text, attributeMetadata.LogicalName);
            }

            return null;
        }
    }
}
