using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "DataverseRow")]
    [OutputType(typeof(Entity))]
    public sealed class GetRowCommand : CmdletBase
    {
        private const string RetrieveWithIdParameterSet = "RetrieveWithId";
        private const string RetrieveWithKeyParameterSet = "RetrieveWithKey";

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RetrieveWithIdParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithKeyParameterSet)]
        [ValidateNotNullOrEmpty]
        public PSObject Key { get; set; }

        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Columns { get; set; }

        private ColumnSet _columnset;

        protected override void BeginExecution()
        {
            _columnset = BuildColumnSet(Columns);
        }

        protected override void Execute()
        {
            OrganizationResponse response;

            switch (ParameterSetName)
            {
                case RetrieveWithIdParameterSet:
                    response = Session.Current.Client.ExecuteOrganizationRequest(
                        RetrieveSingleRowRequest(new EntityReference(Table, Id), _columnset),
                        "AMSoftware Dataverse PowerShell Get-DataverseRow");

                    WriteObject(response.Results["Entity"] as Entity);

                    break;
                case RetrieveWithKeyParameterSet:
                    KeyAttributeCollection keysCollection = new KeyAttributeCollection();
                    keysCollection.AddRange(from keyMember in Key.Properties
                                            select new KeyValuePair<string, object>(keyMember.Name, keyMember.Value));

                    response = Session.Current.Client.ExecuteOrganizationRequest(
                        RetrieveSingleRowRequest(new EntityReference(Table, keysCollection), _columnset),
                        "AMSoftware Dataverse PowerShell Get-DataverseRow");

                    WriteObject(response.Results["Entity"] as Entity);

                    break;
            }
        }

        private static OrganizationRequest RetrieveSingleRowRequest(EntityReference reference, ColumnSet columns)
        {
            OrganizationRequest request = new OrganizationRequest("Retrieve")
            {
                Parameters = new ParameterCollection()
            };
            request.Parameters.Add("Target", reference);
            request.Parameters.Add("ColumnSet", columns);

            return request;
        }

        private static ColumnSet BuildColumnSet(string[] columns)
        {
            if (columns == null || columns.Length == 0) return new ColumnSet(true);
            else return new ColumnSet(columns);
        }
    }
}
