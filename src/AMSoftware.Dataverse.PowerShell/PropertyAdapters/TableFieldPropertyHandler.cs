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
