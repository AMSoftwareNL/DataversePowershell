using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRelatedRow", DefaultParameterSetName = AddSingleRelatedRowParameterSet)]
    public sealed class AddRelatedRowCommand : BatchCmdletBase, IDynamicParameters
    {
        private const string AddSingleRelatedRowParameterSet = "AddSingleRelatedRow";
        private const string AddCollectionRelatedRowsParameterSet = "AddCollectionRelatedRows";

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string TargetTable { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public Guid TargetRow { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = AddSingleRelatedRowParameterSet)]
        [Alias("LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = AddSingleRelatedRowParameterSet)]
        [Alias("Id")]
        [ValidateNotNullOrEmpty]
        public Guid RelatedRow { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddCollectionRelatedRowsParameterSet)]
        [ValidateNotNullOrEmpty]
        public EntityReference[] Rows { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Relationship { get; set; }

        private BypassLogicParameters _bypassContext;
        public object GetDynamicParameters()
        {
            _bypassContext = new BypassLogicParameters();
            return _bypassContext;
        }

        protected override void Execute()
        {
            EntityReferenceCollection relatedRows = new EntityReferenceCollection();

            switch(ParameterSetName)
            {
                case AddSingleRelatedRowParameterSet:
                    relatedRows.Add(new EntityReference(RelatedTable, RelatedRow));
                    break;

                case AddCollectionRelatedRowsParameterSet:
                    relatedRows.AddRange(Rows);
                    break;
            } 

            var request = new AssociateRequest()
            {
                Target = new EntityReference(TargetTable, TargetRow),
                RelatedEntities = relatedRows,
                Relationship = new Relationship(Relationship)
            };
            _bypassContext.ApplyBypass(request);

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = (AssociateResponse)Session.Current.Client.ExecuteOrganizationRequest(
                    request, MyInvocation.MyCommand.Name);
            }
        }
    }
}
