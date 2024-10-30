using Microsoft.Xrm.Sdk;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class EntityConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            return false;
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (destinationType == typeof(Guid)) return true;
            if (destinationType == typeof(EntityReference)) return true;
            return false;
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            throw new NotSupportedException();
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue != null && sourceValue is Entity entityValue)
            {
                if (destinationType == typeof(Guid)) return entityValue.Id;
                if (destinationType == typeof(EntityReference)) return entityValue.ToEntityReference();
            }

            if (sourceValue != null && sourceValue is EntityReference entityReferenceValue && destinationType == typeof(Guid))
            {
                return entityReferenceValue.Id;
            }

            throw new NotSupportedException();
        }
    }
}
