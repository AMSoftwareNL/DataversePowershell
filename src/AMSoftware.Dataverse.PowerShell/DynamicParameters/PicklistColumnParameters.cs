using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class PicklistColumnParameters : ColumnTypeParametersBase
    {
        internal PicklistColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        public int DefaultValue { get; set; }
        
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata[] Options { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string GlobalOptionsetName { get; set; }


        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new PicklistAttributeMetadata()
            {
                OptionSet = new OptionSetMetadata
                {
                    IsGlobal = false,
                    OptionSetType = OptionSetType.Picklist
                }
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(DefaultValue)))
                result.DefaultFormValue = DefaultValue;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Options)))
                result.OptionSet.Options.AddRange(Options);

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(GlobalOptionsetName)))
                result.OptionSet = new OptionSetMetadata
                {
                    IsGlobal = true,
                    Name = GlobalOptionsetName
                };

            return result;
        }
    }
}
