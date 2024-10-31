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
    public sealed class DoubleColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 0)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedValue, DoubleAttributeMetadata.MaxSupportedValue)]
        public double MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DecimalAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedValue, DoubleAttributeMetadata.MaxSupportedValue)]
        public double MaxValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 2)]
        [ValidateRange(DoubleAttributeMetadata.MinSupportedPrecision, DoubleAttributeMetadata.MaxSupportedPrecision)]
        public int Precision { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new DoubleAttributeMetadata()
            {

                MinValue = 0,
                MaxValue = DoubleAttributeMetadata.MaxSupportedValue,
                Precision = 2,
                ImeMode = ImeMode.Auto
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as DoubleAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Precision)))
                result.Precision = Precision;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;
        }
    }
}
