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
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class LabelConverter : PSTypeConverter
    {
        private const int FallbackLanguageId = 1033; //English

        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (sourceValue.GetType() == typeof(string)) return true;

            var sc = TypeDescriptor.GetConverter(destinationType);
            return sc.CanConvertFrom(sourceValue.GetType());
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            int language = FallbackLanguageId;
            try
            {
                language = Session.Current.LanguageId;
            }
            catch { }

            if (sourceValue == null) return null;
            if (sourceValue.GetType() == typeof(string)) return new Label((string)sourceValue, language);

            var sc = TypeDescriptor.GetConverter(destinationType);
            return new Label((string)sc.ConvertFrom(sourceValue), language);
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (sourceValue == null) return true;
            if (destinationType == typeof(string)) return true;

            var sc = TypeDescriptor.GetConverter(destinationType);
            return sc.CanConvertTo(destinationType);
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue == null) return null;

            if (sourceValue is Label labelValue)
            {
                int language = FallbackLanguageId;
                try
                {
                    language = Session.Current.LanguageId;
                }
                catch { }


                LocalizedLabel languageLabel = 
                    labelValue.UserLocalizedLabel ??
                    labelValue.LocalizedLabels.SingleOrDefault(l => l.LanguageCode == language);
                    
                if (languageLabel != null)
                {
                    if (destinationType == typeof(string)) return languageLabel.Label;
                    else
                    {
                        var sc = TypeDescriptor.GetConverter(destinationType);
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
