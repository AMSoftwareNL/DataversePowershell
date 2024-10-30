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

            Int32Converter dc = new Int32Converter();
            return dc.CanConvertFrom(sourceValue.GetType());
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return false;
            if (destinationType == typeof(int)) return true;

            Int32Converter dc = new Int32Converter();
            return dc.CanConvertTo(destinationType);
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;
            if (sourceValue.GetType() == typeof(int)) return new OptionSetValue((int)sourceValue);

            Int32Converter dc = new Int32Converter();
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
                    Int32Converter dc = new Int32Converter();
                    return dc.ConvertTo(optionsetValue.Value, destinationType);
                }
            }

            return new NotSupportedException();
        }
    }
}
