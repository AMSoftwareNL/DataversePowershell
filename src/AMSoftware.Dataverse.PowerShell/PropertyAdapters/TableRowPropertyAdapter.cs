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
            var tableMetadata = Session.Current.Client.GetEntityMetadata(internalObject.LogicalName, EntityFilters.Entity | EntityFilters.Attributes);

            var resultCollection = new List<PSAdaptedProperty>();
            foreach (var propertyInfo in _typeProperties)
            {
                resultCollection.Add(new PSAdaptedProperty(propertyInfo.Name, new MemberTypePropertyHandler<Entity>(propertyInfo)));
            }

            PropertyInfo formattedValuesPropertyInfo = typeof(Entity).GetProperty(nameof(Entity.FormattedValues));
            resultCollection.Add(new PSAdaptedProperty("State", new ReadonlyCollectionPropertyHandler<Entity, string, string>(formattedValuesPropertyInfo, "statecode")));
            resultCollection.Add(new PSAdaptedProperty("Status", new ReadonlyCollectionPropertyHandler<Entity, string, string>(formattedValuesPropertyInfo, "statuscode")));

            string primaryNameLogicalName = tableMetadata.PrimaryNameAttribute;
            if (!string.IsNullOrWhiteSpace(primaryNameLogicalName))
            {
                resultCollection.Add(new PSAdaptedProperty("Name", new TableFieldPropertyHandler(primaryNameLogicalName)));
            }
            resultCollection = resultCollection.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            List<PSAdaptedProperty> properties = new List<PSAdaptedProperty>();
            #region Metadata Attributes
            foreach (var attribute in tableMetadata.Attributes)
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
