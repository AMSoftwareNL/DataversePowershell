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
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class BooleanColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = false)]
        public bool DefaultValue { get; set; }
        
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata FalseOption { get; set; }

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public OptionMetadata TrueOption { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new BooleanAttributeMetadata()
            {
                DefaultValue = false,
                OptionSet = new BooleanOptionSetMetadata(
                   new OptionMetadata(new Label("Yes", Session.Current.LanguageId), 1),
                   new OptionMetadata(new Label("No", Session.Current.LanguageId), 0))
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            BooleanAttributeMetadata result = attribute as BooleanAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(DefaultValue)))
                result.DefaultValue = DefaultValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(FalseOption)))
                result.OptionSet.FalseOption = FalseOption;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(TrueOption)))
                result.OptionSet.TrueOption = TrueOption;
        }
    }
}
