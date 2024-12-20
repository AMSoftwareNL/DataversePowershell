﻿/* 
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
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Get, "DataverseTable", DefaultParameterSetName = GetTablesByFilterParameterSet)]
    [OutputType(typeof(EntityMetadata))]
    public sealed class GetTableCommand : RequestCmdletBase
    {
        private const string GetTableByIdParameterSet = "GetTableById";
        private const string GetTableByEtcParameterSet = "GetTableByEtc";
        private const string GetTablesByFilterParameterSet = "GetTablesByFilter";

        [Parameter(Mandatory = true, ParameterSetName = GetTableByIdParameterSet, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("MetadataId")]
        [ValidateNotNull]
        public Guid Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = GetTableByEtcParameterSet)]
        [Alias("ObjectTypeCode", "EntityTypeCode")]
        [ValidateNotNull]
        public int TypeCode { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        [Alias("Include", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Name { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        [ValidateNotNullOrEmpty]
        [SupportsWildcards]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Exclude { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        [ValidateNotNullOrEmpty]
        public TableType Type { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Custom { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Unmanaged { get; set; }

        [Parameter(ParameterSetName = GetTablesByFilterParameterSet)]
        public SwitchParameter Intersects { get; set; }

        IEnumerable<EntityMetadata> _entitiesMetadata;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            switch (ParameterSetName)
            {
                case GetTableByEtcParameterSet:
                case GetTablesByFilterParameterSet:
                    var request = new RetrieveAllEntitiesRequest()
                    {
                        EntityFilters = EntityFilters.Entity,
                        RetrieveAsIfPublished = true
                    };

                    var response = ExecuteOrganizationRequest<RetrieveAllEntitiesResponse>(request);

                    _entitiesMetadata = response.EntityMetadata.AsEnumerable();

                    break;
            }
        }

        public override void Execute()
        {
            switch (ParameterSetName)
            {
                case GetTableByIdParameterSet:
                    var getByIdRequest = new RetrieveEntityRequest()
                    {
                        EntityFilters = EntityFilters.Entity,
                        MetadataId = Id,
                        RetrieveAsIfPublished = true
                    };

                    var getByIdResponse = ExecuteOrganizationRequest<RetrieveEntityResponse>(getByIdRequest);

                    WriteObject(getByIdResponse.EntityMetadata);

                    break;
                case GetTableByEtcParameterSet:
                    WriteObject(_entitiesMetadata.FirstOrDefault(e => e.ObjectTypeCode == TypeCode));
                    break;
                case GetTablesByFilterParameterSet:
                    IEnumerable<EntityMetadata> result = _entitiesMetadata.ToList();

                    if (!string.IsNullOrWhiteSpace(Name))
                    {
                        WildcardPattern includePattern = new WildcardPattern(Name, WildcardOptions.IgnoreCase);
                        result = result.Where(e => includePattern.IsMatch(e.LogicalName));
                    }
                    if (!string.IsNullOrWhiteSpace(Exclude))
                    {
                        WildcardPattern excludePattern = new WildcardPattern(Exclude, WildcardOptions.IgnoreCase);
                        result = result.Where(e => !excludePattern.IsMatch(e.LogicalName));
                    }

                    if (Custom.IsPresent) result = result.Where(e => e.IsCustomEntity == Custom.ToBool());
                    if (Unmanaged.IsPresent) result = result.Where(e => e.IsManaged == !Unmanaged.ToBool());
                    if (Intersects.IsPresent) result = result.Where(e => e.IsIntersect == Intersects.ToBool());

                    if (MyInvocation.BoundParameters.ContainsKey(nameof(Type)))
                    {
                        switch (Type)
                        {
                            case TableType.Standard:
                                result = result.Where(e => e.TableType == TableTypeCode.Standard.ToString());
                                break;
                            case TableType.Activity:
                                result = result.Where(e => e.IsActivity == true);
                                break;
                            case TableType.Virtual:
                                result = result.Where(e => e.TableType == TableTypeCode.Virtual.ToString());
                                break;
                            case TableType.Datasource:
                                result = result.Where(e => e.DataProviderId != null);
                                break;
                            case TableType.Elastic:
                                result = result.Where(e => e.TableType == TableTypeCode.Elastic.ToString());
                                break;
                        }
                    }

                    WriteObject(result.OrderBy(e => e.LogicalName).ToList(), true);

                    break;
            }
        }
    }
}
