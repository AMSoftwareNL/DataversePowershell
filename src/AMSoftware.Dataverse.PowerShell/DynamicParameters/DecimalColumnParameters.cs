using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class DecimalColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DecimalAttributeMetadata.MinSupportedValue)]
        [ValidateRange(DecimalAttributeMetadata.MinSupportedValue, DecimalAttributeMetadata.MaxSupportedValue)]
        public decimal MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DecimalAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(DecimalAttributeMetadata.MinSupportedValue, DecimalAttributeMetadata.MaxSupportedValue)]
        public decimal MaxValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 2)]
        [ValidateRange(DecimalAttributeMetadata.MinSupportedPrecision, DecimalAttributeMetadata.MaxSupportedPrecision)]
        public int Precision { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new DecimalAttributeMetadata()
            {
                MinValue = (decimal)DecimalAttributeMetadata.MinSupportedValue,
                MaxValue = (decimal)DecimalAttributeMetadata.MaxSupportedValue,
                Precision = 2,
                ImeMode = ImeMode.Auto
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as DecimalAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Precision)))
                result.Precision = Precision;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;
        }
    }
}
