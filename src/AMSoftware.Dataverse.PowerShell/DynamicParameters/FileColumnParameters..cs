using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class FileColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 32768)]
        [ValidateRange(1, 131072)]
        public int MaxSizeInKB { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new FileAttributeMetadata()
            {
                MaxSizeInKB = 32768
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as FileAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;
        }
    }
}
