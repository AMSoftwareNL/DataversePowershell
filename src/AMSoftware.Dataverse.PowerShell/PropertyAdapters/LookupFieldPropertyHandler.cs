using Microsoft.Xrm.Sdk;
using System;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class LookupFieldPropertyHandler : IAdaptedPropertyHandler<Entity>
    {
        private readonly string _attributeName;

        public LookupFieldPropertyHandler(string attributeName)
        {
            _attributeName = attributeName;
        }

        public virtual string TypeName
        {
            get { return typeof(string).FullName; }
        }

        public virtual bool IsSettable
        {
            get { return false; }
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

                if (result != null && result is EntityReference entityRefResult)
                {
                    return entityRefResult.LogicalName;
                }
            }

            return null;
        }

        public virtual void SetValue(Entity baseObject, object value)
        {
            throw new NotSupportedException();
        }
    }
}
