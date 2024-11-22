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
    public sealed class ManagedPropertyConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (destinationType == typeof(BooleanManagedProperty) && sourceValue is bool) return true;
            if (destinationType == typeof(AttributeRequiredLevelManagedProperty) && sourceValue is AttributeRequiredLevel) return true;

            var converter = TypeDescriptor.GetConverter(sourceValue);
            return converter.CanConvertFrom(sourceValue.GetType());
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return false;
            if (destinationType == typeof(bool) && sourceValue is BooleanManagedProperty) return true;
            if (destinationType == typeof(AttributeRequiredLevel) && sourceValue is AttributeRequiredLevelManagedProperty) return true;

            var dc = TypeDescriptor.GetConverter(destinationType);
            return dc.CanConvertTo(destinationType);
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;

            if (destinationType == typeof(BooleanManagedProperty))
            {
                if (sourceValue is bool) return new BooleanManagedProperty((bool)sourceValue);
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(bool));
                    return new BooleanManagedProperty((bool)converter.ConvertFrom(sourceValue));
                }
            }

            if (destinationType == typeof(AttributeRequiredLevelManagedProperty))
            {
                if (sourceValue is AttributeRequiredLevel) return new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)sourceValue);
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(AttributeRequiredLevel));
                    return new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)converter.ConvertFrom(sourceValue));
                }
            }

            throw new NotSupportedException();
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) throw new NotSupportedException();

            if (sourceValue is BooleanManagedProperty booleanValue)
            {
                if (destinationType == typeof(bool)) return booleanValue.Value;
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(bool));
                    return converter.ConvertTo(booleanValue.Value, destinationType);
                }
            }

            if (sourceValue is AttributeRequiredLevelManagedProperty attributeValue)
            {
                if (destinationType == typeof(AttributeRequiredLevel)) return attributeValue.Value;
                else
                {
                    var converter = TypeDescriptor.GetConverter(typeof(AttributeRequiredLevel));
                    return converter.ConvertTo(attributeValue.Value, destinationType);
                }
            }

            return new NotSupportedException();
        }
    }
}
