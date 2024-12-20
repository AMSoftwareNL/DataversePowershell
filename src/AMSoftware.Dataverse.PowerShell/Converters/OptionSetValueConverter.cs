﻿/* 
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
using System;
using System.ComponentModel;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class OptionSetValueConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (sourceValue.GetType() == typeof(int)) return true;

            var dc = TypeDescriptor.GetConverter(destinationType);
            return dc.CanConvertFrom(sourceValue.GetType());
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return false;
            if (destinationType == typeof(int)) return true;

            var dc = TypeDescriptor.GetConverter(destinationType);
            return dc.CanConvertTo(destinationType);
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;
            if (sourceValue.GetType() == typeof(int)) return new OptionSetValue((int)sourceValue);

            var dc = TypeDescriptor.GetConverter(destinationType);
            return new OptionSetValue((int)dc.ConvertFrom(sourceValue));
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) throw new NotSupportedException();

            if (sourceValue is OptionSetValue optionsetValue)
            {
                if (destinationType == typeof(int)) return optionsetValue.Value;
                else
                {
                    var dc = TypeDescriptor.GetConverter(destinationType);
                    return dc.ConvertTo(optionsetValue.Value, destinationType);
                }
            }

            return new NotSupportedException();
        }
    }
}
