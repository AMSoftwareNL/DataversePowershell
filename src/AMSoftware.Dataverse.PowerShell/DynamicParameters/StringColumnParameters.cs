using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class StringColumnParameters : ColumnTypeParametersBase
    {
        internal StringColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = StringFormat.Text)]
        public StringFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 100)]
        [ValidateRange(StringAttributeMetadata.MinSupportedLength, StringAttributeMetadata.MaxSupportedLength)]
        public int MaxLength { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new StringAttributeMetadata()
            {
                Format = StringFormat.Text,
                ImeMode = ImeMode.Auto,
                MaxLength = 100
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
