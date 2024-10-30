using Microsoft.Xrm.Sdk;
using System;
using System.ComponentModel;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class MoneyConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (sourceValue.GetType() == typeof(decimal)) return true;

            DecimalConverter dc = new DecimalConverter();
            return dc.CanConvertFrom(sourceValue.GetType());
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return false;
            if (destinationType == typeof(decimal)) return true;

            DecimalConverter dc = new DecimalConverter();
            return dc.CanConvertTo(destinationType);
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;
            if (sourceValue.GetType() == typeof(decimal)) return new Money((decimal)sourceValue);

            DecimalConverter dc = new DecimalConverter();
            return new Money((decimal)dc.ConvertFrom(sourceValue));
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) throw new NotSupportedException();

            if (sourceValue is Money moneyValue)
            {
                if (destinationType == typeof(decimal)) return moneyValue.Value;
                else
                {
                    DecimalConverter dc = new DecimalConverter();
                    return dc.ConvertTo(moneyValue.Value, destinationType);
                }
            }

            return new NotSupportedException();
        }
    }
}
