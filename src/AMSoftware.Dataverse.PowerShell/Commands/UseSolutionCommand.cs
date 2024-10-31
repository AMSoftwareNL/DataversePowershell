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
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsOther.Use, "DataverseSolution")]
    [OutputType(typeof(Session))]
    public sealed class UseSolutionCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        protected override void Execute()
        {
            if (TryGetValidSolutionName(Name, out string solutionUniqueName))
            {
                Session.Current.ActiveSolution = solutionUniqueName;
                WriteObject(Session.Current);
            }
            else
            {
                WriteError(new ErrorRecord(
                        new ArgumentException($"No unmanaged solution found with unique name '{Name}'."),
                        ErrorCode.UnknownUnmanagedSolution,
                        ErrorCategory.InvalidArgument,
                        Name));
            }
        }

        private bool TryGetValidSolutionName(string name, out string solutionUniqueName)
        {
            var query = new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet("uniquename", "ismanaged"),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("uniquename", ConditionOperator.Equal, name),
                        new ConditionExpression("ismanaged", ConditionOperator.Equal, false)
                    }
                }
            };

            var response = ExecuteOrganizationRequest<RetrieveMultipleResponse>(
                new RetrieveMultipleRequest()
                {
                    Query = query
                });

            if (response.EntityCollection != null && response.EntityCollection.Entities != null && response.EntityCollection.Entities.Count == 1)
            {
                solutionUniqueName = response.EntityCollection.Entities[0].GetAttributeValue<string>("uniquename");
                return true;
            }
            else
            {
                solutionUniqueName = null;
                return false;
            }
        }
    }
}
