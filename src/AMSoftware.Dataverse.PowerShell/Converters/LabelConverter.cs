using Microsoft.Xrm.Sdk;
using System;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class LabelConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (sourceValue.GetType() == typeof(string)) return true;

            StringConverter sc = new StringConverter();
            return sc.CanConvertFrom(sourceValue.GetType());
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;
            if (sourceValue.GetType() == typeof(string)) return new Label((string)sourceValue, Session.Current.LanguageId);

            StringConverter sc = new StringConverter();
            return new Label((string)sc.ConvertFrom(sourceValue), Session.Current.LanguageId);
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (destinationType == typeof(string)) return true;

            StringConverter sc = new StringConverter();
            return sc.CanConvertTo(destinationType);
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;

            if (sourceValue is Label labelValue)
            {
                LocalizedLabel languageLabel = labelValue.LocalizedLabels.SingleOrDefault(l => l.LanguageCode == Session.Current.LanguageId);
                if (languageLabel != null)
                {
                    if (destinationType == typeof(string)) return languageLabel.Label;
                    else
                    {
                        StringConverter sc = new StringConverter();
                        return sc.ConvertTo(languageLabel.Label, destinationType);
                    }
                }
                else
                {
                    return null;
                }
            }

            return new NotSupportedException();
        }
    }
}
