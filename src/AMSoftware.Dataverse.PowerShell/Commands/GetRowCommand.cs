using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "DataverseRow")]
    [OutputType(typeof(Session))]
    public sealed class GetRowCommand : CmdletBase
    {
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Table { get; set; }

        [Parameter(Mandatory = false, Position = 2, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public Guid[] Id { get; set; }

        [Parameter(ValueFromRemainingArguments = true)]
        public string[] Columns { get; set; }


        private ColumnSet _columnset;

        protected override void BeginExecution()
        {
            _columnset = BuildColumnSet(Columns);
        }

        protected override void Execute()
        {
            if (Id == null || Id.Length == 0)
            {
                QueryExpression query = new QueryExpression(Table)
                {
                    ColumnSet = _columnset
                };

                query.PageInfo = new PagingInfo()
                {
                    Count = 1000,
                    ReturnTotalRecordCount = true,
                    PageNumber = 1
                };

                var hasMoreRecords = false;
                do
                {
                    EntityCollection result = Session.Current.Client.RetrieveMultiple(query);
                    
                    WriteObject(result.Entities, true);

                    query.PageInfo.PagingCookie = result.PagingCookie;
                    hasMoreRecords = result.MoreRecords;
                } while (hasMoreRecords);
            }
            else
            {
                foreach (Guid id in Id)
                {
                    WriteObject(Session.Current.Client.Retrieve(Table, id, _columnset));
                }
            }
        }

        private static ColumnSet BuildColumnSet(string[] columns)
        {
            if (columns == null || columns.Length == 0) return new ColumnSet(true);
            else return new ColumnSet(columns);
        }

    }
}
