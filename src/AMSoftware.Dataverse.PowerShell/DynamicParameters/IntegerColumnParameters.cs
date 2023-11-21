using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class IntegerColumnParameters : ColumnTypeParametersBase
    {
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

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new IntegerAttributeMetadata()
            {
                Format = IntegerFormat.None,
                MinValue = IntegerAttributeMetadata.MinSupportedValue,
                MaxValue = IntegerAttributeMetadata.MaxSupportedValue
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as IntegerAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;
        }
    }
}
