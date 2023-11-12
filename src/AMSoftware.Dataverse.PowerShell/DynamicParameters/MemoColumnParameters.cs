using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class MemoColumnParameters : ColumnTypeParametersBase
    {
        internal MemoColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

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

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new MemoAttributeMetadata()
            {
                Format = StringFormat.Text,
                ImeMode = ImeMode.Auto,
                MaxLength = 2000
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxLength)))
                result.MaxLength = MaxLength;

            return result;
        }
    }
}
