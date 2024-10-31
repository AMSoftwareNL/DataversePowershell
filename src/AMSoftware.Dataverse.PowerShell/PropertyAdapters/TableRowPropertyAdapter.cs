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
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Extensions;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    public sealed class TableRowPropertyAdapter : PropertyAdapterBase<Entity>
    {
        private List<PropertyInfo> _typeProperties = new List<PropertyInfo>();

        public TableRowPropertyAdapter()
        {
            _typeProperties = new List<PropertyInfo>();

            Type entityType = typeof(Entity);
            PropertyInfo[] entityProperties = entityType.GetProperties();

            string[] excludedProperties = { "EntityState", "ExtensionData", "FormattedValues", "Item",
                "LazyFileAttributeKey","LazyFileAttributeValue","LazyFileSizeAttributeKey","LazyFileSizeAttributeValue", "HasLazyFileAttribute"
            };
            foreach (var property in entityProperties)
            {
                if (!excludedProperties.Contains(property.Name))
                {
                    _typeProperties.Add(property);
                }
            }
        }

        protected override Collection<PSAdaptedProperty> GetAdaptedProperties(Entity internalObject)
        {
            var resultCollection = new List<PSAdaptedProperty>();
            foreach (var propertyInfo in _typeProperties)
            {
                resultCollection.Add(new PSAdaptedProperty(propertyInfo.Name, new MemberTypePropertyHandler<Entity>(propertyInfo)));
            }

            PropertyInfo formattedValuesPropertyInfo = typeof(Entity).GetProperty(nameof(Entity.FormattedValues));
            resultCollection.Add(new PSAdaptedProperty("State", new ReadonlyCollectionPropertyHandler<Entity, string, string>(formattedValuesPropertyInfo, "statecode")));
            resultCollection.Add(new PSAdaptedProperty("Status", new ReadonlyCollectionPropertyHandler<Entity, string, string>(formattedValuesPropertyInfo, "statuscode")));

            var tableMetadata = Session.Current.Client.GetEntityMetadata(internalObject.LogicalName, EntityFilters.Entity);
            string primaryNameLogicalName = tableMetadata.PrimaryNameAttribute;
            if (!string.IsNullOrWhiteSpace(primaryNameLogicalName))
            {
                resultCollection.Add(new PSAdaptedProperty("Name", new TableFieldPropertyHandler(primaryNameLogicalName)));
            }
            resultCollection = resultCollection.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            List<PSAdaptedProperty> properties = new List<PSAdaptedProperty>();
            #region Metadata Attributes
            var columnMetadata = Session.Current.Client.GetAllAttributesForEntity(internalObject.LogicalName);

            foreach (var attribute in columnMetadata)
            {
                if (attribute.AttributeTypeName == AttributeTypeDisplayName.VirtualType) continue;
                if (!string.IsNullOrWhiteSpace(attribute.AttributeOf)) continue;
                if (attribute.IsValidForRead == false) continue;

                string key = attribute.LogicalName;
                properties.Add(new PSAdaptedProperty(key, new TableFieldPropertyHandler(key)));
                if (attribute.AttributeTypeName == AttributeTypeDisplayName.CustomerType ||
                    attribute.AttributeTypeName == AttributeTypeDisplayName.LookupType ||
                    attribute.AttributeTypeName == AttributeTypeDisplayName.OwnerType)
                {
                    properties.Add(new PSAdaptedProperty(string.Format("{0}_Entity", key), new LookupFieldPropertyHandler(key)));
                }
            }
            #endregion

            #region Aliased Attributes
            foreach (var key in internalObject.Attributes.Keys)
            {
                if (internalObject.Attributes[key] != null && internalObject.Attributes[key] is AliasedValue)
                {
                    AliasedValue aliasedValueAttribute = internalObject.GetAttributeValue<AliasedValue>(key);
                    string aliassedKey = key.Replace('.', '_');
                    properties.Add(new PSAdaptedProperty(aliassedKey, new AliasFieldPropertyHandler(key)));

                    if (aliasedValueAttribute.Value != null && aliasedValueAttribute.Value is EntityReference)
                    {
                        properties.Add(new PSAdaptedProperty(string.Format("{0}_Entity", aliassedKey), new LookupFieldPropertyHandler(key)));
                    }
                }
            }
            #endregion

            #region FormattedValues
            foreach (var key in internalObject.FormattedValues.Keys)
            {
                if (internalObject.FormattedValues[key] != null)
                {
                    properties.Add(
                        new PSAdaptedProperty(string.Format("{0}_FormattedValue", key.Replace('.', '_')), 
                            new ReadonlyCollectionPropertyHandler<Entity, string, string>(
                                formattedValuesPropertyInfo, key)));
                }
            }
            #endregion
       
            foreach (var prop in properties.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase))
            {
                if (!resultCollection.Any(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    resultCollection.Add(prop);
                }
            }

            return new Collection<PSAdaptedProperty>(resultCollection);
        }

        public override Collection<string> GetTypeNameHierarchy(object baseObject)
        {
            if (baseObject is Entity internalObject)
            {
                Collection<string> typeHierarchy = base.GetTypeNameHierarchy(baseObject);
                typeHierarchy.Insert(0, string.Format("{0}+{1}", internalObject.GetType().FullName, internalObject.LogicalName));

                return typeHierarchy;
            }
            return base.GetTypeNameHierarchy(baseObject);
        }
    }
}
