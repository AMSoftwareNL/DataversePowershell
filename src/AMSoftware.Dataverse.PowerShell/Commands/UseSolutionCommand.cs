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
