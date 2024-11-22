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
using AMSoftware.Dataverse.PowerShell.Converters;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell
{
    public static class ToStringCodeMethods
    {
        public static string Label(PSObject instance)
        {
            var converter = new LabelConverter();

            if (converter.CanConvertTo(instance, typeof(string)))
            {
                return converter.ConvertTo(instance.ImmediateBaseObject, typeof(string), null, true) as string;
            }

            return instance?.ImmediateBaseObject.ToString();
        }

        public static string Money(PSObject instance)
        {
            var converter = new MoneyConverter();

            if (converter.CanConvertTo(instance, typeof(string)))
            {
                return converter.ConvertTo(instance.ImmediateBaseObject, typeof(string), null, true) as string;
            }

            return instance?.ImmediateBaseObject.ToString();
        }

        public static string OptionSetValue(PSObject instance)
        {
            var converter = new OptionSetValueConverter();

            if (converter.CanConvertTo(instance, typeof(string)))
            {
                return converter.ConvertTo(instance.ImmediateBaseObject, typeof(string), null, true) as string;
            }

            return instance?.ImmediateBaseObject.ToString();
        }

        public static string ManagedProperty(PSObject instance)
        {
            var converter = new ManagedPropertyConverter();

            if (converter.CanConvertTo(instance, typeof(string)))
            {
                return converter.ConvertTo(instance.ImmediateBaseObject, typeof(string), null, true) as string;
            }

            return instance?.ImmediateBaseObject.ToString();
        }

        public static string StringConstant(PSObject instance)
        {
            var converter = new StringConstantConverter();

            if (converter.CanConvertTo(instance, typeof(string)))
            {
                return converter.ConvertTo(instance.ImmediateBaseObject, typeof(string), null, true) as string;
            }

            return instance?.ImmediateBaseObject.ToString();
        }
    }
}
