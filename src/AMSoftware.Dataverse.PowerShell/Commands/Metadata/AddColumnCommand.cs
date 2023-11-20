using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Diagnostics;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseColumn")]
    [OutputType(typeof(AttributeMetadata))]
    public abstract class AddColumnCommand : RequestCmdletBase, IDynamicParameters
    {
        [Parameter(Mandatory = true)]
        public ColumnType Type { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipelineByPropertyName = true)]
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

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ColumnRequiredLevel.Required)]
        public ColumnRequiredLevel Required { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Searchable { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Auditing { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter ColumnSecurity { get; set; }

        private ColumnTypeParametersBase _dynamicContext;
        public object GetDynamicParameters()
        {
            _dynamicContext = ColumnTypeParametersBase.Create(this);

            return _dynamicContext;
        }

        protected override void Execute()
        {
            AttributeMetadata attributeMetadata = _dynamicContext.BuildAttributeMetadata();

            attributeMetadata.LogicalName = Name;
            attributeMetadata.SchemaName = Name;
            attributeMetadata.DisplayName = new Label(DisplayName, Session.Current.LanguageId);
            attributeMetadata.Description = Description == null ? null : new Label(Description, Session.Current.LanguageId);
            attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.SystemRequired);
            attributeMetadata.ExternalName = ExternalName;
            attributeMetadata.IsGlobalFilterEnabled = new BooleanManagedProperty(Searchable.ToBool());
            attributeMetadata.IsAuditEnabled = new BooleanManagedProperty(Auditing.ToBool());
            attributeMetadata.IsSecured = ColumnSecurity.ToBool();

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Required)))
                attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)Required);

            var createRequest = new CreateAttributeRequest()
            {
                EntityName = Table,
                Attribute = attributeMetadata
            };

            var createResponse = ExecuteOrganizationRequest<CreateAttributeResponse>(createRequest);

            var getByIdRequest = new RetrieveAttributeRequest()
            {
                EntityLogicalName = Table,
                MetadataId = createResponse.AttributeId,
                RetrieveAsIfPublished = true
            };
            var getByIdResponse = ExecuteOrganizationRequest<RetrieveAttributeResponse>(getByIdRequest);

            WriteObject(getByIdResponse.AttributeMetadata);
        }
    }
}
