using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class DecimalColumnParameters : ColumnTypeParametersBase
    {
        internal DecimalColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

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

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new DecimalAttributeMetadata()
            {
                MinValue = (decimal)DecimalAttributeMetadata.MinSupportedValue,
                MaxValue = (decimal)DecimalAttributeMetadata.MaxSupportedValue,
                Precision = 2,
                ImeMode = ImeMode.Auto
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Precision)))
                result.Precision = Precision;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            return result;
        }
    }
}
