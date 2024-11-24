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
using AMSoftware.Dataverse.PowerShell.Commands.Metadata;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.Xml;

namespace AMSoftware.Dataverse.PowerShell.Commands.Content
{
    [Cmdlet(VerbsCommon.Get, "DataverseRows", DefaultParameterSetName = RetrieveWithQueryParameterSet, SupportsPaging = true)]
    [OutputType(typeof(Entity))]
    public sealed class GetRowsCommand : RequestCmdletBase
    {
        private const string RetrieveWithFetchXmlParameterSet = "RetrieveWithFetchXml";
        private const string RetrieveWithAttributeQueryParameterSet = "RetrieveWithAttributeQuery";
        private const string RetrieveWithQueryParameterSet = "RetrieveWithQuery";
        private const string RetrieveWithBatchParameterSet = "RetrieveWithBatch";

        private const int QueryPagesize = 5000;
        private const int QueryBatchsize = 1000;

        private int _topCount = 0;

        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithAttributeQueryParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithQueryParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = RetrieveWithBatchParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        [Alias("LogicalName")]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = RetrieveWithBatchParameterSet)]
        [ValidateNotNullOrEmpty]
        public Guid[] Id { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = RetrieveWithBatchParameterSet)]
        public SwitchParameter AsBatch { get; set; }

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

        private ColumnSet _columnSet;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            if (PagingParameters.Skip != 0 || PagingParameters.First != ulong.MaxValue)
            {
                switch (ParameterSetName)
                {
                    case RetrieveWithFetchXmlParameterSet:
                    case RetrieveWithAttributeQueryParameterSet:
                    case RetrieveWithQueryParameterSet:
                        if (PagingParameters.First == ulong.MaxValue) PagingParameters.First = QueryPagesize;

                        if (PagingParameters.Skip + PagingParameters.First > QueryPagesize)
                        {
                            throw new PSArgumentOutOfRangeException(
                                nameof(PagingParameters),
                                PagingParameters.First + PagingParameters.Skip,
                                $"Paging parameters only allowed when less than pagesize (={QueryPagesize})");
                        }
                        break;
                    case RetrieveWithBatchParameterSet:
                        if (PagingParameters.First == ulong.MaxValue) PagingParameters.First = QueryBatchsize;

                        if (PagingParameters.Skip + PagingParameters.First > QueryBatchsize)
                        {
                            throw new PSArgumentOutOfRangeException(
                                nameof(PagingParameters),
                                PagingParameters.First + PagingParameters.Skip,
                                $"Paging parameters only allowed when less than batchsize (={QueryBatchsize})");
                        }
                        break;
                }

                _topCount = (int)(PagingParameters.Skip + PagingParameters.First);
            }

            if (PagingParameters.IncludeTotalCount.ToBool() && _topCount == 0)
                throw new PSArgumentException("Total Count only allow in combination with Skip and/or First.", nameof(PagingParameters.IncludeTotalCount));

            _columnSet = BuildColumnSet(Columns);

        }

        public override void Execute()
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
        }

        private QueryExpression BuildTableQuery()
        {
            QueryExpression tableQuery = new QueryExpression(Table)
            {
                ColumnSet = _columnSet
            };

            if (Sort != null)
            {
                foreach (var sortKey in Sort.Keys)
                {
                    tableQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                }
            }

            if (_topCount != 0) tableQuery.TopCount = _topCount;

            return tableQuery;
        }

        private QueryByAttribute BuildAttributeQuery()
        {
            QueryByAttribute attributeQuery = new QueryByAttribute(Table)
            {
                ColumnSet = _columnSet
            };

            foreach (var attributeKey in Query.Keys)
            {
                attributeQuery.AddAttributeValue((string)attributeKey, Query[attributeKey]);
            }

            if (Sort != null)
            {
                foreach (var sortKey in Sort.Keys)
                {
                    attributeQuery.AddOrder((string)sortKey, (OrderType)Sort[sortKey]);
                }
            }

            if (_topCount != 0) attributeQuery.TopCount = _topCount;

            return attributeQuery;
        }

        private void ExecuteBatchRetrieve()
        {
            int pageCount = 0;
            Guid batchId = default;
            RequestBatch batch = default;

            // Apply paging to the input array.
            var pagedIds = Id.AsEnumerable();
            if (PagingParameters.Skip != 0)
                pagedIds = pagedIds.Skip((int)PagingParameters.Skip);

            if (PagingParameters.First != ulong.MaxValue)
                pagedIds = pagedIds.Take((int)PagingParameters.First);

            var rowIds = pagedIds.ToArray();
            for (int i = 0; i < rowIds.Length; i++)
            {
                if (pageCount == 0)
                {
                    var batchName = $"Get-DataverseRows {Session.Current.Client.OAuthUserId} {DateTime.UtcNow:s}";

                    WriteVerboseWithTimestamp("Create Batch: {0}", batchName);
                    batchId = Session.Current.Client.CreateBatchOperationRequest(batchName, true, true);

                    WriteVerboseWithTimestamp("BatchId: {0}", batchId);
                    batch = Session.Current.Client.GetBatchById(batchId);
                }

                WriteVerboseWithTimestamp("Add BatchItem: {0} ({1})", Table, rowIds[i]);

                batch.BatchItems.Add(new BatchItemOrganizationRequest()
                {
                    Request = new RetrieveRequest()
                    {
                        ColumnSet = _columnSet,
                        Target = new EntityReference(Table, rowIds[i])
                    }
                });
                pageCount++;

                if (pageCount == QueryBatchsize || i == rowIds.Length - 1)
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

            if (PagingParameters.IncludeTotalCount.ToBool())
                WriteObject(PagingParameters.NewTotalCount((ulong)batchResponse.Responses.Where(r => r.Fault == null).Count(), 1.0));

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
            bool moreRecords = false;
            string pagingCookie = null;

            do
            {
                XmlDocument localFetchXml = CreateFetchXmlWithPaging(fetchXml, pageNumber, pagingCookie);

                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = new FetchExpression(localFetchXml.OuterXml)
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                var output = pagedQueryResponse.EntityCollection.Entities.AsEnumerable();
                if (PagingParameters.Skip != 0)
                    output = output.Skip((int)PagingParameters.Skip);

                if (PagingParameters.IncludeTotalCount.ToBool())
                    WriteObject(PagingParameters.NewTotalCount((ulong)output.Count(), 1.0));

                WriteObject(output, true);

                pageNumber += 1;
                pagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;
                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;
            } while (moreRecords);
        }

        private void ExecuteAttributeQuery(QueryByAttribute query)
        {
            if (_topCount == 0)
            {
                query.PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = QueryPagesize
                };
            }

            bool moreRecords = false;
            do
            {
                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = query
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                var output = pagedQueryResponse.EntityCollection.Entities.AsEnumerable();
                if (PagingParameters.Skip != 0)
                    output = output.Skip((int)PagingParameters.Skip);

                if (PagingParameters.IncludeTotalCount.ToBool())
                    WriteObject(PagingParameters.NewTotalCount((ulong)output.Count(), 1.0));

                WriteObject(output, true);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;

                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;
            } while (moreRecords);
        }

        private void ExecuteQuery(QueryExpression query)
        {
            if (_topCount == 0)
            {
                query.PageInfo = new PagingInfo()
                {
                    PageNumber = 1,
                    Count = QueryPagesize
                };
            }

            bool moreRecords = false;
            do
            {
                var pagedQueryRequest = new RetrieveMultipleRequest()
                {
                    Query = query
                };

                var pagedQueryResponse = ExecuteOrganizationRequest<RetrieveMultipleResponse>(pagedQueryRequest);

                var output = pagedQueryResponse.EntityCollection.Entities.AsEnumerable();
                if (PagingParameters.Skip != 0)
                    output = output.Skip((int)PagingParameters.Skip);

                if (PagingParameters.IncludeTotalCount.ToBool())
                    WriteObject(PagingParameters.NewTotalCount((ulong)output.Count(), 1.0));

                WriteObject(output, true);

                query.PageInfo.PageNumber += 1;
                query.PageInfo.PagingCookie = pagedQueryResponse.EntityCollection.PagingCookie;

                moreRecords = pagedQueryResponse.EntityCollection.MoreRecords;
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

            if (_topCount != 0)
            {
                XmlAttribute topAttribute = pagedFetchXml.CreateAttribute("top");
                topAttribute.Value = _topCount.ToString();
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
