using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    public abstract class PropertyAdapterBase<T> : PSPropertyAdapter
    {
        public override Collection<PSAdaptedProperty> GetProperties(object baseObject)
        {
            if (baseObject is T internalObject)
            {
                return GetAdaptedProperties(internalObject);
            }

            return null;
        }

        public override PSAdaptedProperty GetProperty(object baseObject, string propertyName)
        {
            if (baseObject is T internalObject)
            {
                var properties = GetAdaptedProperties(internalObject);
                PSAdaptedProperty property = properties.SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
                if (property != null)
                {
                    return property;
                }
            }

            return null;
        }

        public override string GetPropertyTypeName(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty.Tag != null)
            {
                if (adaptedProperty.Tag is IAdaptedPropertyHandler<T> propHandler) return propHandler.TypeName;
            }

            return typeof(object).FullName;
        }

        public override object GetPropertyValue(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty.BaseObject is T internalObject && adaptedProperty.Tag != null && IsGettable(adaptedProperty))
            {
                if (adaptedProperty.Tag is IAdaptedPropertyHandler<T> propHandler)
                {
                    return propHandler.GetValue(internalObject);
                }
            }

            return null;
        }

        public override bool IsGettable(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty.Tag != null)
            {
                if (adaptedProperty.Tag is IAdaptedPropertyHandler<T> propHandler)
                {
                    return propHandler.IsGettable;
                }
            }
            return false;
        }

        public override bool IsSettable(PSAdaptedProperty adaptedProperty)
        {
            if (adaptedProperty.Tag != null)
            {
                if (adaptedProperty.Tag is IAdaptedPropertyHandler<T> propHandler)
                {
                    return propHandler.IsSettable;
                }
            }
            return false;
        }

        public override void SetPropertyValue(PSAdaptedProperty adaptedProperty, object value)
        {
            if (adaptedProperty.BaseObject is T internalObject && adaptedProperty.Tag != null && IsSettable(adaptedProperty))
            {
                if (adaptedProperty.Tag is IAdaptedPropertyHandler<T> propHandler)
                {
                    object internalValue = value;
                    if (value is PSObject objectValue)
                    {
                        internalValue = objectValue.BaseObject;
                    }

                    propHandler.SetValue(internalObject, internalValue);
                }
            }
        }

        protected abstract Collection<PSAdaptedProperty> GetAdaptedProperties(T internalObject);
    }
}
