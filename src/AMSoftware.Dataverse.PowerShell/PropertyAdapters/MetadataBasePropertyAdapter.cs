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
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    public sealed class MetadataBasePropertyAdapter : PropertyAdapterBase<MetadataBase>
    {
        protected override Collection<PSAdaptedProperty> GetAdaptedProperties(MetadataBase internalObject)
        {
            string[] excludedProperties = { "Attributes", "ExtensionData", "HasChanged", "Keys", "ManyToManyRelationships", "ManyToOneRelationships", "OneToManyRelationships", "Privileges" };

            Type metadataType = internalObject.GetType();
            PropertyInfo[] metadataProperties = metadataType.GetProperties();

            List<PSAdaptedProperty> typeProperties = new List<PSAdaptedProperty>();
            foreach (var property in metadataProperties)
            {
                if (!excludedProperties.Contains(property.Name))
                {
                    var propertyTypeMembers = property.PropertyType.FindMembers(
                        MemberTypes.Method, BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance,
                        (f, g) => {
                            return f.Name == "ToString";
                        }, MethodAttributes.Public);

                    if (propertyTypeMembers == null || propertyTypeMembers.Length == 0)
                    {
                        typeProperties.Add(new PSAdaptedProperty(property.Name, 
                            new MetadataBaseValuePropertyHandler(property)));
                    } else
                    {
                        typeProperties.Add(new PSAdaptedProperty(property.Name, new MemberTypePropertyHandler<MetadataBase>(property)));
                    }
                }
            }
            typeProperties = typeProperties.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            var resultCollection = new Collection<PSAdaptedProperty>(typeProperties);
            return resultCollection;
        }
    }
}
