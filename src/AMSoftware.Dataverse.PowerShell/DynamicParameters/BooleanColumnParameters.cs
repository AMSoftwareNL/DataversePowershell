using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class BooleanColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = false)]
        public bool DefaultValue { get; set; }
        
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata FalseOption { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata TrueOption { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new BooleanAttributeMetadata()
            {
                DefaultValue = false,
                OptionSet = new BooleanOptionSetMetadata(
                   new OptionMetadata(new Label("Yes", Session.Current.LanguageId), 1),
                   new OptionMetadata(new Label("No", Session.Current.LanguageId), 0))
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            BooleanAttributeMetadata result = attribute as BooleanAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(DefaultValue)))
                result.DefaultValue = DefaultValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(FalseOption)))
                result.OptionSet.FalseOption = FalseOption;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(TrueOption)))
                result.OptionSet.TrueOption = TrueOption;
        }
    }
}
