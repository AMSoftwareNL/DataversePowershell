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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseTableKey")]
    [OutputType(typeof(EntityKeyMetadata))]
    public sealed class AddTableKeyCommand : RequestCmdletBase
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true)]
        [Alias("Attributes")]
        [ValidateNotNullOrEmpty]
        [ValidateCount(1, int.MaxValue)]
        public string[] Columns { get; set; }

        public override void Execute()
        {
            EntityKeyMetadata key = new EntityKeyMetadata()
            {
                LogicalName = Name,
                SchemaName = Name,
                DisplayName = new Label(DisplayName, Session.Current.LanguageId),
                KeyAttributes = Columns
            };

            var createRequest = new CreateEntityKeyRequest
            {
                EntityName = Table,
                EntityKey = key
            };

            var createResponse = ExecuteOrganizationRequest<CreateEntityKeyResponse>(createRequest);

            var keyMetadataId = createResponse.EntityKeyId;

            var getByIdRequest = new RetrieveEntityKeyRequest()
            {
                MetadataId = keyMetadataId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveEntityKeyResponse>(getByIdRequest);

            WriteObject(getByIdResponse.EntityKeyMetadata);
        }
    }
}
