﻿using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Add, "DataverseRelatedRow", DefaultParameterSetName = AddSingleRelatedRowParameterSet)]
    public sealed class AddRelatedRowCommand : BatchCmdletBase
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

            if (UseBatch)
            {
                AddOrganizationRequestToBatch(request);
            }
            else
            {
                var response = ExecuteOrganizationRequest<AssociateResponse>(request);
            }
        }
    }
}
