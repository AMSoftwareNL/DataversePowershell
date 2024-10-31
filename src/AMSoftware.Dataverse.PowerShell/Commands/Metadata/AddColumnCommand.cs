using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseColumn")]
    [OutputType(typeof(AttributeMetadata))]
    public sealed class AddColumnCommand : RequestCmdletBase, IDynamicParameters
    {
        private const string AddColumnByInputObjectParameterset = "AddColumnByInputObject";
        private const string AddColumnByParametersParameterset = "AddColumnByParameters";

        [Parameter(Mandatory = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = AddColumnByInputObjectParameterset)]
        [ValidateNotNullOrEmpty]
        public AttributeMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddColumnByParametersParameterset)]
        public ColumnType Type { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        [PSDefaultValue(Value = ColumnRequiredLevel.Required)]
        public ColumnRequiredLevel Required { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        public SwitchParameter Searchable { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        public SwitchParameter Auditing { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        public SwitchParameter ColumnSecurity { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddColumnByParametersParameterset)]
        public ColumnSourceType Source { get; set; }

        private ColumnTypeParametersBase _dynamicContext = null;
        public object GetDynamicParameters()
        {
            if (ParameterSetName == AddColumnByParametersParameterset)
            {
                _dynamicContext = ColumnTypeParametersBase.Create(Type);
            }

            return _dynamicContext;
        }

        protected override void Execute()
        {
            AttributeMetadata attributeMetadata = InputObject;

            if (ParameterSetName == AddColumnByParametersParameterset)
            {
                attributeMetadata = _dynamicContext.CreateAttributeMetadata();

                attributeMetadata.LogicalName = Name;
                attributeMetadata.SchemaName = Name;
                attributeMetadata.DisplayName = new Label(DisplayName, Session.Current.LanguageId);
                attributeMetadata.Description = Description == null ? null : new Label(Description, Session.Current.LanguageId);
                attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.SystemRequired);
                attributeMetadata.ExternalName = ExternalName;
                attributeMetadata.IsValidForAdvancedFind = new BooleanManagedProperty(Searchable.ToBool());
                attributeMetadata.IsAuditEnabled = new BooleanManagedProperty(Auditing.ToBool());
                attributeMetadata.IsSecured = ColumnSecurity.ToBool();

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Required)))
                    attributeMetadata.RequiredLevel = new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)Required);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Source)))
                    attributeMetadata.SourceType = (int)Source;

                _dynamicContext.ApplyParameters(this, ref attributeMetadata);
            }

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
