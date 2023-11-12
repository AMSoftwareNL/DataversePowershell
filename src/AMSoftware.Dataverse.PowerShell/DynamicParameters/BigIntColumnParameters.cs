using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class BigIntColumnParameters : ColumnTypeParametersBase
    {
        internal BigIntColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new BigIntAttributeMetadata();

            return result;
        }
    }
}
