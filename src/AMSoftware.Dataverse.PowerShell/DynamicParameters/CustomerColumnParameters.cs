using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class CustomerColumnParameters : ColumnTypeParametersBase
    {
        internal CustomerColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string AccountRelationshipName { get; set; }

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string ContactRelationshipName { get; set; }

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
