/* 
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
