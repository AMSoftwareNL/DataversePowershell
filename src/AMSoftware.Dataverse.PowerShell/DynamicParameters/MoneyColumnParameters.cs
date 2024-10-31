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
using AMSoftware.Dataverse.PowerShell.Commands;
using Microsoft.Xrm.Sdk.Metadata;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class MoneyColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MinSupportedValue)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedValue, MoneyAttributeMetadata.MaxSupportedValue)]
        public double MinValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MaxSupportedValue)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedValue, MoneyAttributeMetadata.MaxSupportedValue)]
        public double MaxValue { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = MoneyAttributeMetadata.MinSupportedPrecision)]
        [ValidateRange(MoneyAttributeMetadata.MinSupportedPrecision, MoneyAttributeMetadata.MaxSupportedPrecision)]
        public int Precision { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = CurrencyPrecisionSource.Currency)]
        public CurrencyPrecisionSource PrecisionSource { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new MoneyAttributeMetadata()
            {
                MinValue = MoneyAttributeMetadata.MinSupportedValue,
                MaxValue = MoneyAttributeMetadata.MaxSupportedValue,
                Precision = MoneyAttributeMetadata.MinSupportedPrecision,
                PrecisionSource = (int)CurrencyPrecisionSource.Currency,
                ImeMode = ImeMode.Auto
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as MoneyAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MinValue)))
                result.MinValue = MinValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxValue)))
                result.MaxValue = MaxValue;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Precision)))
                result.Precision = Precision;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(PrecisionSource)))
                result.PrecisionSource = (int)PrecisionSource;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;
        }
    }
}
