using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class ImageColumnParameters : ColumnTypeParametersBase
    {
        internal ImageColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        public SwitchParameter IsPrimaryImage { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 10240)]
        [ValidateRange(1, 30720)]
        public int MaxSizeInKB { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter CanStoreFullImage { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new ImageAttributeMetadata()
            {
                IsPrimaryImage = IsPrimaryImage.ToBool(),
                CanStoreFullImage = CanStoreFullImage.ToBool(),
                MaxSizeInKB = 10240
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;

            return result;
        }
    }
}
