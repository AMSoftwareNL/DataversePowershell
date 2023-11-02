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
                    typeProperties.Add(new PSAdaptedProperty(property.Name, new MemberTypePropertyHandler<MetadataBase>(property)));
                }
            }
            typeProperties = typeProperties.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            var resultCollection = new Collection<PSAdaptedProperty>(typeProperties);
            return resultCollection;
        }
    }
}
