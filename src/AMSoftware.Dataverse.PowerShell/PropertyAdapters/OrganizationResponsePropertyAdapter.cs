using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Reflection;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    public sealed class OrganizationResponsePropertyAdapter : PropertyAdapterBase<OrganizationResponse>
    {
        List<PropertyInfo> _typeProperties = new List<PropertyInfo>();

        public OrganizationResponsePropertyAdapter() : base()
        {
            _typeProperties = new List<PropertyInfo>();

            Type organizationResponseType = typeof(OrganizationResponse);
            PropertyInfo[] organizationResponseProperties = organizationResponseType.GetProperties();

            string[] excludedProperties = { "ExtensionData" };
            foreach (var property in organizationResponseProperties)
            {
                if (!excludedProperties.Contains(property.Name))
                {
                    _typeProperties.Add(property);
                }
            }
        }

        protected override Collection<PSAdaptedProperty> GetAdaptedProperties(OrganizationResponse internalObject)
        {
            var resultCollection = new List<PSAdaptedProperty>();

            List<PSAdaptedProperty> properties = new List<PSAdaptedProperty>();
            foreach (var property in _typeProperties)
            {
                resultCollection.Add(new PSAdaptedProperty(property.Name, new MemberTypePropertyHandler<OrganizationResponse>(property)));

                if (property.Name == nameof(OrganizationResponse.Results))
                {
                    foreach (var key in internalObject.Results.Keys)
                    {
                        if (internalObject.Results[key] != null)
                        {
                            properties.Add(new PSAdaptedProperty(key, new ReadonlyCollectionPropertyHandler<OrganizationResponse, string, object>(property, key)));
                        }
                    }
                }
            }
            resultCollection = resultCollection.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();

            foreach (var prop in properties.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase))
            {
                if (!resultCollection.Any(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    resultCollection.Add(prop);
                }
            }

            return new Collection<PSAdaptedProperty>(resultCollection);
        }
    }
}
