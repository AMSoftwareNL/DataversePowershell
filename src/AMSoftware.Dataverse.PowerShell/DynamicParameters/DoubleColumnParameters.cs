using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class DoubleColumnParameters : ColumnTypeParametersBase
    {
        internal DoubleColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 0)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedValue, DoubleAttributeMetadata.MaxSupportedValue)]
        public double MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DecimalAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedValue, DoubleAttributeMetadata.MaxSupportedValue)]
        public double MaxValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 2)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedPrecision, DoubleAttributeMetadata.MaxSupportedPrecision)]
        public int Precision { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new DoubleAttributeMetadata()
            {
                
                MinValue = 0,
                MaxValue = DoubleAttributeMetadata.MaxSupportedValue,
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
