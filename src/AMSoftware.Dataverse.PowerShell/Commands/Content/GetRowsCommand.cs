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
using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Xml;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Get, "DataverseRows", DefaultParameterSetName = RetrieveWithQueryParameterSet)]
    [OutputType(typeof(Entity))]
    public sealed class GetRowsCommand : RequestCmdletBase
    {
        private const string RetrieveWithFetchXmlParameterSet = "RetrieveWithFetchXml";
        private const string RetrieveWithAttributeQueryParameterSet = "RetrieveWithAttributeQuery";
        private const string RetrieveWithQueryParameterSet = "RetrieveWithQuery";
        private const string RetrieveWithBatchParameterSet = "RetrieveWithBatch";

        private const int QueryPagesize = 5000;
        private const int QueryBatchsize = 1000;

        private readonly List<Guid> _rowsToProcess = [];

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithQueryParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithBatchParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, ParameterSetName = RetrieveWithBatchParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid[] Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [ValidateNotNullOrEmpty]
        public Hashtable Query { get; set; }

        [Parameter(ValueFromRemainingArguments = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(ValueFromRemainingArguments = true, ParameterSetName = RetrieveWithQueryParameterSet)]
        [Parameter(ValueFromRemainingArguments = true, ParameterSetName = RetrieveWithBatchParameterSet)]
        public string[] Columns { get; set; }

        [Parameter(ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(ParameterSetName = RetrieveWithQueryParameterSet)]
        public Hashtable Sort { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithFetchXmlParameterSet, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public XmlDocument FetchXml { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = RetrieveWithFetchXmlParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = RetrieveWithQueryParameterSet)]
        [ValidateRange(1, QueryPagesize)]
        public int Top { get; set; }

        public override void Execute()
        {
            switch (ParameterSetName)
            {
                case RetrieveWithBatchParameterSet:
                    _rowsToProcess.AddRange(Id);
                    break;
            }
        }

        protected override void EndProcessing()
        {
            switch (ParameterSetName)
            {
                case RetrieveWithFetchXmlParameterSet:
                    ExecuteFetchXml(FetchXml);
                    break;
                case RetrieveWithAttributeQueryParameterSet:
                    QueryByAttribute attributeQuery = BuildAttributeQuery();
                    ExecuteAttributeQuery(attributeQuery);
                    break;
                case RetrieveWithQueryParameterSet:
                    QueryExpression tableQuery = BuildTableQuery();
                    ExecuteQuery(tableQuery);
                    break;
                case RetrieveWithBatchParameterSet:
                    ExecuteBatchRetrieve();
                    break;
            }

            base.EndProcessing();
        }

        private QueryExpression BuildTableQuery()
        {
            var tableQuery = new QueryExpression(Table)
            {
                ColumnSet = BuildColumnSet(Columns)
            };

            if (Sort != null)
            {
                foreach (var sortKey in Sort.Keys)
                {
                    tableQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                }
            }

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Top)))
                tableQuery.TopCount = Top;

            return tableQuery;
        }

        private QueryByAttribute BuildAttributeQuery()
        {
            var attributeQuery = new QueryByAttribute(Table)
            {
                ColumnSet = BuildColumnSet(Columns)
            };

            foreach (var attributeKey in Query.Keys)
            {
                var attributeValue = Query[attributeKey];
                if (attributeValue is PSObject psObjectValue)
                    attributeQuery.AddAttributeValue((string)attributeKey, psObjectValue.ImmediateBaseObject);
                else
                    attributeQuery.AddAttributeValue((string)attributeKey, attributeValue);
            }

            if (Sort != null)
            {
                foreach (var sortKey in Sort.Keys)
                {
                    attributeQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                }
            }

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Top)))
                attributeQuery.TopCount = Top;

            return attributeQuery;
        }

        private void ExecuteBatchRetrieve()
        {
            int pageCount = 0;
            Guid batchId = default;
            RequestBatch batch = default;

            for (int i = 0; i < _rowsToProcess.Count; i++)
            {
                if (pageCount == 0)
                {
                    var batchName = $"Get-DataverseRows {Session.Current.Client.OAuthUserId} {DateTime.UtcNow:s}";

                    WriteVerboseWithTimestamp("Create Batch: {0}", batchName);
                    batchId = Session.Current.Client.CreateBatchOperationRequest(batchName, true, true);

                    WriteVerboseWithTimestamp("BatchId: {0}", batchId);
                    batch = Session.Current.Client.GetBatchById(batchId);
                }

                WriteVerboseWithTimestamp("Add BatchItem: {0} ({1})", Table, _rowsToProcess[i]);

                batch.BatchItems.Add(new BatchItemOrganizationRequest()
                {
                    Request = new RetrieveRequest()
                    {
                        ColumnSet = BuildColumnSet(Columns),
                        Target = new EntityReference(Table, _rowsToProcess[i])
                    }
                });
                pageCount++;

                if (pageCount == QueryBatchsize || i == _rowsToProcess.Count - 1)
                {
                    try
                    {
                        WriteVerboseWithTimestamp("Execute Batch: {0}", batchId);
                        ExecuteBatch(batchId);
                    }
                    finally
                    {
                        WriteVerboseWithTimestamp("Release Batch: {0}", batchId);
                        Session.Current.Client.ReleaseBatchInfoById(batchId);
                        pageCount = 0;
                    }
                }
            }
        }

        private void ExecuteBatch(Guid batchId)
        {
            var batchResponse = Session.Current.Client.ExecuteBatch(batchId);

            foreach (var responseItem in batchResponse.Responses)
            {
                if (responseItem.Fault == null)
                {
                    var response = (RetrieveResponse)responseItem.Response;
                    WriteObject(response.Entity);
                }
                else
                {
                    WriteError(new ErrorRecord(
                        new Exception(responseItem.Fault.ToString()),
                        ErrorCode.BatchOperationResponseItemFaulted,
                        ErrorCategory.InvalidResult,
                        Session.Current.Client.GetBatchRequestAtPosition(batchId, responseItem.RequestIndex)));
                }
            }
        }

        private void ExecuteFetchXml(XmlDocument fetchXml)
        {
            int pageNumber = 1;
            bool moreRecords;
            string pagingCookie = null;

            do
            {
                XmlDocument localFetchXml = CreateFetchXmlWithPaging(fetchXml, pageNumber, pagingCookie);

                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = new FetchExpression(localFetchXml.OuterXml)
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                pageNumber += 1;
                pagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;
                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;

                WriteObject(pagedQueryResponse.EntityCollection.Entities, true);
            } while (moreRecords);
        }

        private void ExecuteAttributeQuery(QueryByAttribute query)
        {
            if (!MyInvocation.BoundParameters.ContainsKey(nameof(Top)))
            {
                query.PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = QueryPagesize
                };
            }

            bool moreRecords;
            do
            {
                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = query
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;
                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;

                WriteObject(pagedQueryResponse.EntityCollection.Entities, true);
            } while (moreRecords);
        }

        private void ExecuteQuery(QueryExpression query)
        {
            if (!MyInvocation.BoundParameters.ContainsKey(nameof(Top)))
            {
                query.PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = QueryPagesize
                };
            }

            bool moreRecords;
            do
            {
                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = query
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;
                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;

                WriteObject(pagedQueryResponse.EntityCollection.Entities, true);
            } while (moreRecords);
        }

        private static ColumnSet BuildColumnSet(string[] columns)
        {
            if (columns == null || columns.Length == 0) return new ColumnSet(true);
            else return new ColumnSet(columns);
        }

        private XmlDocument CreateFetchXmlWithPaging(in XmlDocument fetchXml, int pageNumber, string cookie = null)
        {
            XmlDocument pagedFetchXml = (XmlDocument)fetchXml.CloneNode(true);
            XmlAttributeCollection xmlAttributes = pagedFetchXml.DocumentElement.Attributes;

            if (xmlAttributes["top"] != null) return pagedFetchXml;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Top)))
            {
                XmlAttribute topAttribute = pagedFetchXml.CreateAttribute("top");
                topAttribute.Value = Top.ToString();
                pagedFetchXml.DocumentElement.Attributes.Append(topAttribute);

                return pagedFetchXml;
            }

            if (cookie != null)
            {
                XmlAttribute pagingAttr = pagedFetchXml.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                xmlAttributes.Append(pagingAttr);
            }

            XmlAttribute pageAttr = pagedFetchXml.CreateAttribute("page");
            pageAttr.Value = Convert.ToString(pageNumber);
            xmlAttributes.Append(pageAttr);

            XmlAttribute countAttr = pagedFetchXml.CreateAttribute("count");
            countAttr.Value = Convert.ToString(QueryPagesize);
            xmlAttributes.Append(countAttr);

            return pagedFetchXml;
        }
    }
}
