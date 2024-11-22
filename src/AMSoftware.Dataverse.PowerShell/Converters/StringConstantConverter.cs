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
using System;
using System.ComponentModel;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class StringConstantConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            return false;
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (destinationType == typeof(string) && sourceValue is ConstantsBase<string>) return true;

            var dc = TypeDescriptor.GetConverter(typeof(string));
            return dc.CanConvertTo(destinationType);
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            throw new NotSupportedException();
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;

            if (sourceValue is ConstantsBase<string> constantValue)
            {
                if (destinationType == typeof(string)) return constantValue.Value;
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(string));
                    return converter.ConvertTo(constantValue.Value, destinationType);
                }
            }

            return new NotSupportedException();
        }
    }
}
