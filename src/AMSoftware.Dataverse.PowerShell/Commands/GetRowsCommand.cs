using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Management.Automation;
using System.Xml;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "DataverseRows", DefaultParameterSetName = RetrieveWithQueryParameterSet)]
    [OutputType(typeof(Entity))]
    public sealed class GetRowsCommand : CmdletBase
    {
        private const string RetrieveWithFetchXmlParameterSet = "RetrieveWithFetchXml";
        private const string RetrieveWithAttributeQueryParameterSet = "RetrieveWithAttributeQuery";
        private const string RetrieveWithQueryParameterSet = "RetrieveWithQuery";

        private const int QueryPagesize = 5000;

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithQueryParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Query { get; set; }

        [Parameter(ValueFromRemainingArguments = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(ValueFromRemainingArguments = true, ParameterSetName = RetrieveWithQueryParameterSet)]
        public string[] Columns { get; set; }

        [Parameter(ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(ParameterSetName = RetrieveWithQueryParameterSet)]
        public Hashtable Sort { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithFetchXmlParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public XmlDocument FetchXml { get; set; }

        private ColumnSet _columnSet;

        protected override void BeginExecution()
        {
            _columnSet = BuildColumnSet(Columns);
        }

        protected override void Execute()
        {
            switch (ParameterSetName)
            {
                case RetrieveWithFetchXmlParameterSet:
                    ExecuteFetchXml(FetchXml);
                    break;
                case RetrieveWithAttributeQueryParameterSet:
                    QueryByAttribute attributeQuery = new QueryByAttribute(Table)
                    {
                        ColumnSet = _columnSet
                    };


                    foreach (var attributeKey in Query.Keys)
                    {
                        attributeQuery.AddAttributeValue((string)attributeKey, Query[attributeKey]);
                    }

                    foreach (var sortKey in Sort.Keys)
                    {
                        attributeQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                    }

                    ExecuteAttributeQuery(attributeQuery);
                    break;
                case RetrieveWithQueryParameterSet:
                    QueryExpression tableQuery = new QueryExpression(Table)
                    {
                        ColumnSet = _columnSet
                    };

                    foreach (var sortKey in Sort.Keys)
                    {
                        tableQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                    }

                    ExecuteQuery(tableQuery);
                    break;
            }
        }

        private void ExecuteFetchXml(XmlDocument fetchXml)
        {
            int pageNumber = 1;
            bool moreRecords = false;
            string pagingCookie = null;

            do
            {
                XmlDocument localFetchXml = CreateFetchXmlWithPaging(fetchXml, pageNumber, pagingCookie);

                EntityCollection pagedQueryResult = Session.Current.Client.RetrieveMultiple(
                    new FetchExpression(localFetchXml.OuterXml));

                WriteObject(pagedQueryResult.Entities, true);

                pageNumber += 1;
                pagingCookie = pagedQueryResult.PagingCookie;
                moreRecords = pagedQueryResult.MoreRecords;
            } while (moreRecords);
        }

        private void ExecuteAttributeQuery(QueryByAttribute query)
        {
            query.PageInfo = new PagingInfo()
            {
                PageNumber = 1,
                Count = QueryPagesize
            };

            bool moreRecords = false;
            do
            {
                EntityCollection pagedQueryResult = Session.Current.Client.RetrieveMultiple(query);

                WriteObject(pagedQueryResult.Entities, true);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResult.PagingCookie;

                moreRecords = pagedQueryResult.MoreRecords;
            } while (moreRecords);
        }

        private void ExecuteQuery(QueryExpression query)
        {
            query.PageInfo = new PagingInfo()
            {
                PageNumber = 1,
                Count = QueryPagesize
            };

            bool moreRecords = false;
            do
            {
                EntityCollection pagedQueryResult = Session.Current.Client.RetrieveMultiple(query);

                WriteObject(pagedQueryResult.Entities, true);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResult.PagingCookie;

                moreRecords = pagedQueryResult.MoreRecords;
            } while (moreRecords);
        }

        private static ColumnSet BuildColumnSet(string[] columns)
        {
            if (columns == null || columns.Length == 0) return new ColumnSet(true);
            else return new ColumnSet(columns);
        }

        private static XmlDocument CreateFetchXmlWithPaging(in XmlDocument fetchXml, int pageNumber, string cookie = null)
        {
            XmlDocument pagedFetchXml = (XmlDocument)fetchXml.CloneNode(true);
            XmlAttributeCollection xmlAttributes = pagedFetchXml.DocumentElement.Attributes;

            if (cookie != null)
            {
                XmlAttribute pagingAttr = pagedFetchXml.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                xmlAttributes.Append(pagingAttr);
            }

            XmlAttribute pageAttr = pagedFetchXml.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(pageNumber);
            xmlAttributes.Append(pageAttr);

            XmlAttribute countAttr = pagedFetchXml.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(QueryPagesize);
            xmlAttributes.Append(countAttr);

            return pagedFetchXml;
        }
    }
}
