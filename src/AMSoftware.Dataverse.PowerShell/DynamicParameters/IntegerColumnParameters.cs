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
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class IntegerColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerFormat.None)]
        public IntegerFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerAttributeMetadata.MinSupportedValue)]
        [ValidateRange(IntegerAttributeMetadata.MinSupportedValue, IntegerAttributeMetadata.MaxSupportedValue)]
        public int MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = IntegerAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(IntegerAttributeMetadata.MinSupportedValue, IntegerAttributeMetadata.MaxSupportedValue)]
        public int MaxValue { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new IntegerAttributeMetadata()
            {
                Format = IntegerFormat.None,
                MinValue = IntegerAttributeMetadata.MinSupportedValue,
                MaxValue = IntegerAttributeMetadata.MaxSupportedValue
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as IntegerAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;
        }
    }
}
