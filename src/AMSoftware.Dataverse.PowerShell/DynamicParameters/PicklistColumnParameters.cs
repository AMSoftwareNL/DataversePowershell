/* 
PowerShell Module for Power Platform Dataverse
Copyright(C) 2024  AMSoftwareNL

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class PicklistColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        public int DefaultValue { get; set; }
        
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata[] Options { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string GlobalOptionsetName { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new PicklistAttributeMetadata()
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
            var result = attribute as PicklistAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(DefaultValue)))
                result.DefaultFormValue = DefaultValue;

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
