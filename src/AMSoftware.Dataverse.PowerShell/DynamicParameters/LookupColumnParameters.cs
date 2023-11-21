using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class LookupColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string[] Targets { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            //TODO: Lookup is attribute with relationship(s). Not going to work like this.

            var result = new LookupAttributeMetadata()
            {
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
        }
    }
}
