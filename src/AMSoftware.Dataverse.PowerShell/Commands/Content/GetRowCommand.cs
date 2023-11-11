using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Get, "DataverseRow")]
    [OutputType(typeof(Entity))]
    public sealed class GetRowCommand : RequestCmdletBase
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
        public Hashtable Key { get; set; }

        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Columns { get; set; }

        private ColumnSet _columnset;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            _columnset = BuildColumnSet(Columns);
        }

        protected override void Execute()
        {
            RetrieveRequest request = null;

            switch (ParameterSetName)
            {
                case RetrieveWithIdParameterSet:
                    request = RetrieveSingleRowRequest(new EntityReference(Table, Id), _columnset);
                    
                    break;
                case RetrieveWithKeyParameterSet:
                    KeyAttributeCollection keysCollection = new KeyAttributeCollection();

                    foreach (var keyName in Key.Keys)
                    {
                        keysCollection.Add((string)keyName, Key[keyName]);
                    }

                    request = RetrieveSingleRowRequest(new EntityReference(Table, keysCollection), _columnset);
                    
                    break;
            }

            RequestParameters.UseOptionalParameters(request);
            RetrieveResponse response = (RetrieveResponse)Session.Current.Client.ExecuteOrganizationRequest(
                        request, MyInvocation.MyCommand.Name);

            WriteObject(response.Entity);

        }

        private static RetrieveRequest RetrieveSingleRowRequest(EntityReference reference, ColumnSet columns)
        {
            RetrieveRequest request = new RetrieveRequest()
            {
                Target = reference,
                ColumnSet = columns
            };

            return request;
        }

        private static ColumnSet BuildColumnSet(string[] columns)
        {
            if (columns == null || columns.Length == 0) return new ColumnSet(true);
            else return new ColumnSet(columns);
        }
    }
}
