using Microsoft.Xrm.Sdk;
using System;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class TableFieldPropertyHandler : IAdaptedPropertyHandler<Entity>
    {
        private readonly string _attributeName;

        public TableFieldPropertyHandler(string attributeName)
        {
            _attributeName = attributeName;
        }

        public virtual string TypeName
        {
            get { return typeof(object).FullName; }
        }

        public virtual bool IsSettable
        {
            get { return true; }
        }

        public virtual bool IsGettable
        {
            get { return true; }
        }

        public virtual object GetValue(Entity baseObject)
        {
            if (baseObject.Attributes.TryGetValue(_attributeName, out object result))
            {
                if (result is AliasedValue aliasResult)
                {
                    result = aliasResult.Value;
                }

                switch (result)
                {
                    case OptionSetValue o:
                        return o.Value;
                    case Money m:
                        return m.Value;
                    case EntityReference e:
                        return e.Id;
                    default:
                        return result;
                }
            }

            return null;
        }

        public virtual void SetValue(Entity baseObject, object value)
        {
            if (baseObject.Attributes.Contains(_attributeName))
            {
                baseObject.Attributes[_attributeName] = value;
            }
            else
            {
                baseObject.Attributes.Add(_attributeName, value);
            }
        }
    }
}
