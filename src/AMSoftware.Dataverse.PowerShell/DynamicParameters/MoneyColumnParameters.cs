using AMSoftware.Dataverse.PowerShell.Commands;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class MoneyColumnParameters : ColumnTypeParametersBase
    {
        internal MoneyColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MinSupportedValue)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedValue, MoneyAttributeMetadata.MaxSupportedValue)]
        public double MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedValue, MoneyAttributeMetadata.MaxSupportedValue)]
        public double MaxValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MinSupportedPrecision)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedPrecision, MoneyAttributeMetadata.MaxSupportedPrecision)]
        public int Precision { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = CurrencyPrecisionSource.Currency)]
        public CurrencyPrecisionSource PrecisionSource { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new MoneyAttributeMetadata()
            {
                MinValue = MoneyAttributeMetadata.MinSupportedValue,
                MaxValue = MoneyAttributeMetadata.MaxSupportedValue,
                Precision = MoneyAttributeMetadata.MinSupportedPrecision,
                PrecisionSource = (int)CurrencyPrecisionSource.Currency,
                ImeMode = ImeMode.Auto
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Precision)))
                result.Precision = Precision;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(PrecisionSource)))
                result.PrecisionSource = (int)PrecisionSource;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            return result;
        }
    }
}
