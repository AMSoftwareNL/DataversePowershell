using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class MultiSelectPicklistColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata[] Options { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string GlobalOptionsetName { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new MultiSelectPicklistAttributeMetadata()
            {
                OptionSet = new OptionSetMetadata
                {
                    IsGlobal = false,
                    OptionSetType = OptionSetType.Picklist
                }
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as MultiSelectPicklistAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Options)))
                result.OptionSet.Options.AddRange(Options);

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(GlobalOptionsetName)))
                result.OptionSet = new OptionSetMetadata
                {
                    IsGlobal = true,
                    Name = GlobalOptionsetName
                };
        }
    }
}
