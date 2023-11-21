using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class ImageColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IsPrimaryImage { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 10240)]
        [ValidateRange(1, 30720)]
        public int MaxSizeInKB { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter CanStoreFullImage { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new ImageAttributeMetadata()
            {
                IsPrimaryImage = IsPrimaryImage.ToBool(),
                CanStoreFullImage = CanStoreFullImage.ToBool(),
                MaxSizeInKB = 10240
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as ImageAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;
        }
    }
}
