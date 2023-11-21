using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class BigIntColumnParameters : ColumnTypeParametersBase
    {
        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new BigIntAttributeMetadata();

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
        }
    }
}
