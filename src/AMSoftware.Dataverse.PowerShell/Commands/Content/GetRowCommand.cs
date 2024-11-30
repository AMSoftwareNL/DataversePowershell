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

        public override void Execute()
        {
            RetrieveRequest request = null;

            switch (ParameterSetName)
            {
                case RetrieveWithIdParameterSet:
                    request = RetrieveSingleRowRequest(new EntityReference(Table, Id), _columnset);
                    break;
                case RetrieveWithKeyParameterSet:
                    var keysCollection = new KeyAttributeCollection();

                    foreach (var keyName in Key.Keys)
                    {
                        var keyValue = Key[keyName];

                        if (keyValue is PSObject psValue)
                            keysCollection.Add((string)keyName, psValue.ImmediateBaseObject);
                        else
                            keysCollection.Add((string)keyName, keyValue);
                    }

                    request = RetrieveSingleRowRequest(new EntityReference(Table, keysCollection), _columnset);

                    break;
            }

            RetrieveResponse response = ExecuteOrganizationRequest<RetrieveResponse>(request);

            WriteObject(response.Entity);
        }

        private static RetrieveRequest RetrieveSingleRowRequest(EntityReference reference, ColumnSet columns)
        {
            var request = new RetrieveRequest()
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
