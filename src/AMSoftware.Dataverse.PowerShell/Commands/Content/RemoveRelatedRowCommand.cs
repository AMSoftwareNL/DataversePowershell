using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Remove, "DataverseRelatedRow", DefaultParameterSetName = RemoveSingleRelatedRowParameterSet)]
    public sealed class RemoveRelatedRowCommand : BatchCmdletBase, IDynamicParameters
    {
        private const string RemoveSingleRelatedRowParameterSet = "RemoveSingleRelatedRow";
        private const string RemoveCollectionRelatedRowsParameterSet = "RemoveCollectionRelatedRows";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string TargetTable { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid TargetRow { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RemoveSingleRelatedRowParameterSet)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RemoveSingleRelatedRowParameterSet)]
        [Alias("Id")]
        [ValidateNotNullOrEmpty]
        public Guid RelatedRow { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RemoveCollectionRelatedRowsParameterSet)]
        [ValidateNotNullOrEmpty]
        public EntityReference[] Rows { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Relationship { get; set; }

        private OptionalRequestParameters _optionalRequestParameters;
        public object GetDynamicParameters()
        {
            _optionalRequestParameters = new OptionalRequestParameters(this);
            return _optionalRequestParameters;
        }

        protected override void Execute()
        {
            EntityReferenceCollection relatedRows = new EntityReferenceCollection();

            switch(ParameterSetName)
            {
                case RemoveSingleRelatedRowParameterSet:
                    relatedRows.Add(new EntityReference(RelatedTable, RelatedRow));
                    break;

                case RemoveCollectionRelatedRowsParameterSet:
                    relatedRows.AddRange(Rows);
                    break;
            } 

            var request = new DisassociateRequest()
            {
                Target = new EntityReference(TargetTable, TargetRow),
                RelatedEntities = relatedRows,
                Relationship = new Relationship(Relationship)
            };
            _optionalRequestParameters.UseOptionalParameters(request);

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = (DisassociateResponse)Session.Current.Client.ExecuteOrganizationRequest(
                    request, MyInvocation.MyCommand.Name);
            }
        }
    }
}
