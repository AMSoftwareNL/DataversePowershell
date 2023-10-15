using System;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class MemberTypePropertyHandler<T> : IAdaptedPropertyHandler<T>
    {
        private readonly PropertyInfo _propertyInfo;

        public MemberTypePropertyHandler(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public virtual string TypeName
        {
            get { return _propertyInfo.PropertyType.FullName; }
        }

        public virtual bool IsSettable
        {
            get { return _propertyInfo.CanWrite; }
        }

        public virtual bool IsGettable
        {
            get { return _propertyInfo.CanRead; }
        }

        public virtual object GetValue(T baseObject)
        {
            return _propertyInfo.GetMethod.Invoke(baseObject, null);
        }

        public virtual void SetValue(T baseObject, object value)
        {
            _propertyInfo.SetMethod.Invoke(baseObject, new object[] { value });
        }
    }
}
