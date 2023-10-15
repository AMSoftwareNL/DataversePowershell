using Microsoft.Xrm.Sdk;
using System;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class ReadonlyCollectionPropertyHandler<TObject, TKey, TValue> : IAdaptedPropertyHandler<TObject>
    {
        private readonly TKey _key;
        private readonly PropertyInfo _collectionPropertyInfo;

        public ReadonlyCollectionPropertyHandler(PropertyInfo collectionPropertyInfo, TKey key)
        {
            _collectionPropertyInfo = collectionPropertyInfo;
            _key = key;
        }

        public virtual string TypeName
        {
            get { return typeof(TValue).FullName; }
        }

        public virtual bool IsSettable
        {
            get { return false; }
        }

        public virtual bool IsGettable
        {
            get { return true; }
        }

        public virtual object GetValue(TObject baseObject)
        {
            DataCollection<TKey, TValue> collection = _collectionPropertyInfo.GetValue(baseObject) as DataCollection<TKey, TValue>;
            if (collection != null)
            {
                collection.TryGetValue(_key, out TValue result);
                return result;
            }

            return null;
        }

        public virtual void SetValue(TObject baseObject, object value)
        {
            throw new NotSupportedException();
        }
    }
}
