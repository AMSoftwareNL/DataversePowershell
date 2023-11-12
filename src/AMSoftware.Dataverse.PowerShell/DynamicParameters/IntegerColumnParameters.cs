using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class IntegerColumnParameters : ColumnTypeParametersBase
    {
        internal IntegerColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerFormat.None)]
        public IntegerFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerAttributeMetadata.MinSupportedValue)]
        [ValidateRange(IntegerAttributeMetadata.MinSupportedValue, IntegerAttributeMetadata.MaxSupportedValue)]
        public int MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(IntegerAttributeMetadata.MinSupportedValue, IntegerAttributeMetadata.MaxSupportedValue)]
        public int MaxValue { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new IntegerAttributeMetadata()
            {
                Format = IntegerFormat.None,
                MinValue = IntegerAttributeMetadata.MinSupportedValue,
                MaxValue = IntegerAttributeMetadata.MaxSupportedValue
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            return result;
        }
    }
}
