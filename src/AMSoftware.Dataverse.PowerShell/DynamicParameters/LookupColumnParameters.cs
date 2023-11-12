using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class LookupColumnParameters : ColumnTypeParametersBase
    {
        internal LookupColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string[] Targets { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            //TODO: Lookup is attribute with relationship(s). Not going to work like this.

            var result = new LookupAttributeMetadata()
            {
            };

            return result;
        }
    }
}
