using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using AMSoftware.Dataverse.PowerShell.DynamicParameters;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Set, "DataverseColumn")]
    [OutputType(typeof(AttributeMetadata))]
    public sealed class SetColumnCommand : RequestCmdletBase, IDynamicParameters
    {
        private const string SetColumnByInputObjectParameterset = "SetColumnByInputObject";
        private const string SetColumnByParametersParameterset = "SetColumnByParameters";

        [Parameter(Mandatory = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = SetColumnByInputObjectParameterset)]
        [ValidateNotNullOrEmpty]
        public AttributeMetadata InputObject { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = SetColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string Description { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetColumnByParametersParameterset)]
        [ValidateNotNullOrEmpty]
        public string ExternalName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetColumnByParametersParameterset)]
        public SwitchParameter Searchable { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = SetColumnByParametersParameterset)]
        public SwitchParameter Auditing { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter MergeLabels { get; set; }

        private ColumnTypeParametersBase _dynamicContext = null;
        public object GetDynamicParameters()
        {
            if (ParameterSetName == SetColumnByParametersParameterset)
            {
                var attributeMetadata = Session.Current.Client.GetEntityAttributeMetadataForAttribute(Table, Name);

                _dynamicContext = ColumnTypeParametersBase.Create(attributeMetadata);
            }

            return _dynamicContext;
        }

        protected override void Execute()
        {
            AttributeMetadata attributeMetadata = InputObject;

            if (ParameterSetName == SetColumnByParametersParameterset)
            {
                attributeMetadata = Session.Current.Client.GetEntityAttributeMetadataForAttribute(Table, Name);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(DisplayName)))
                    attributeMetadata.DisplayName = new Label(DisplayName, Session.Current.LanguageId);

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Description)))
                    attributeMetadata.Description = Description == null ? null : new Label(Description, Session.Current.LanguageId);
                
                if (MyInvocation.BoundParameters.ContainsKey(nameof(ExternalName)))
                    attributeMetadata.ExternalName = ExternalName;

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Searchable)))
                    attributeMetadata.IsGlobalFilterEnabled = new BooleanManagedProperty(Searchable.ToBool());

                if (MyInvocation.BoundParameters.ContainsKey(nameof(Auditing)))
                    attributeMetadata.IsAuditEnabled = new BooleanManagedProperty(Auditing.ToBool());

                if (_dynamicContext != null)
                {
                    _dynamicContext.ApplyParameters(this, ref attributeMetadata);
                }
            }

            var updateRequest = new UpdateAttributeRequest()
            {
                EntityName = Table,
                Attribute = attributeMetadata,
                MergeLabels = MergeLabels.ToBool()
            };
            var updateResponse = ExecuteOrganizationRequest<UpdateAttributeResponse>(updateRequest);

            var getMetadataRequest = new RetrieveAttributeRequest()
            {
                EntityLogicalName = Table,
                LogicalName = attributeMetadata.LogicalName,
                RetrieveAsIfPublished = true
            };
            var getMetadataResponse = ExecuteOrganizationRequest<RetrieveAttributeResponse>(getMetadataRequest);

            WriteObject(getMetadataResponse.AttributeMetadata);
        }
    }
}
