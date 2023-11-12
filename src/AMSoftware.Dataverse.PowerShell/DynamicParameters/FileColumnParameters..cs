using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class FileColumnParameters : ColumnTypeParametersBase
    {
        internal FileColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 32768)]
        [ValidateRange(1, 131072)]
        public int MaxSizeInKB { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new FileAttributeMetadata()
            {
                MaxSizeInKB = 32768
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;

            return result;
        }
    }
}
