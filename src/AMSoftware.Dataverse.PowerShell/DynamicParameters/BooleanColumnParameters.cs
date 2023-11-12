using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class BooleanColumnParameters : ColumnTypeParametersBase
    {
        internal BooleanColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = false)]
        public bool DefaultValue { get; set; }
        
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata FalseOption { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata TrueOption { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new BooleanAttributeMetadata()
            {
               DefaultValue = false,
               OptionSet = new BooleanOptionSetMetadata(
                   new OptionMetadata(new Label("Yes", Session.Current.LanguageId), 1),
                   new OptionMetadata(new Label("No", Session.Current.LanguageId), 0))
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(DefaultValue)))
                result.DefaultValue = DefaultValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(FalseOption)))
                result.OptionSet.FalseOption = FalseOption;
                

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(TrueOption)))
                result.OptionSet.TrueOption = TrueOption;

            return result;
        }
    }
}
