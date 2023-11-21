using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class MemoColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = StringFormat.Text)]
        public StringFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 2000)]
        [ValidateRange(MemoAttributeMetadata.MinSupportedLength, MemoAttributeMetadata.MaxSupportedLength)]
        public int MaxLength { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new MemoAttributeMetadata()
            {
                Format = StringFormat.Text,
                ImeMode = ImeMode.Auto,
                MaxLength = 2000
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as MemoAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxLength)))
                result.MaxLength = MaxLength;
        }
    }
}
