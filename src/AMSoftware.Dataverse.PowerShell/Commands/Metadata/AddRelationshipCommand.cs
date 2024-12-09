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
using AMSoftware.Dataverse.PowerShell.ArgumentCompleters;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Linq;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands.Metadata
{
    [Cmdlet(VerbsCommon.Add, "DataverseRelationship", DefaultParameterSetName = AddManyToOneRelationshipParameterSet)]
    [OutputType(typeof(RelationshipMetadataBase))]
    public sealed class AddRelationshipCommand : RequestCmdletBase
    {
        private const string AddManyToManyRelationshipParameterSet = "AddManyToManyRelationship";
        private const string AddManyToOneRelationshipParameterSet = "AddManyToOneRelationship";
        private const string AddCustomerRelationshipParameterSet = "AddCustomerRelationship";

        [Parameter(Mandatory = true)]
        [Alias("EntityLogicalName", "LogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string Table { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [Alias("AttributeLogicalName")]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ColumnNameArgumentCompleter))]
        public string ColumnName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ColumnDisplayName { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [PSDefaultValue(Value = ColumnRequiredLevel.Optional)]
        public ColumnRequiredLevel ColumnRequired { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ColumnDescription { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(TableNameArgumentCompleter))]
        public string RelatedTable { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = true, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string AccountName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddCustomerRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string ContactName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        [ValidateNotNullOrEmpty]
        public string Intersect { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddCustomerRelationshipParameterSet)]
        public SwitchParameter Searchable { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        public AssociatedMenuConfiguration MenuConfiguration { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToManyRelationshipParameterSet)]
        public AssociatedMenuConfiguration RelatedMenuConfiguration { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        public CascadeConfiguration Behavior { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddManyToOneRelationshipParameterSet)]
        public SwitchParameter Polymorphic { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = AddCustomerRelationshipParameterSet)]
        public SwitchParameter Customer { get; set; }

        public override void Execute()
        {
            switch (ParameterSetName)
            {
                case AddManyToManyRelationshipParameterSet:
                    var manyToManyRequest = BuildManyToManyRequest();
                    var manyToManyResponse = ExecuteOrganizationRequest<CreateManyToManyResponse>(manyToManyRequest);

                    break;
                case AddManyToOneRelationshipParameterSet:
                    if (Polymorphic.ToBool())
                    {
                        // Check if Lookup already exists.
                        // If this is the case we don't create a PolymorphicLookup, 
                        // but add a new relationship to the existing PolymorphicLookup
                        var entityAttributes = Session.Current.Client.GetAllAttributesForEntity(Table);
                        var lookupAttribute = (LookupAttributeMetadata)entityAttributes.SingleOrDefault(a =>
                                a.LogicalName.Equals(ColumnName, StringComparison.InvariantCultureIgnoreCase) &&
                                a.AttributeType == AttributeTypeCode.Lookup);

                        if (lookupAttribute == null)
                        {
                            var polymorhpicRequest = BuildPolymorphicRequest();
                            var polymorphicResponse = ExecuteOrganizationRequest<CreatePolymorphicLookupAttributeResponse>(polymorhpicRequest);
                        }
                        else
                        {
                            var manyToOneRequest = BuildManyToOneRequest(lookupAttribute);
                            var manyToOneResponse = ExecuteOrganizationRequest<CreateOneToManyResponse>(manyToOneRequest);
                        }
                    }
                    else
                    {
                        var manyToOneRequest = BuildManyToOneRequest();
                        var manyToOneResponse = ExecuteOrganizationRequest<CreateOneToManyResponse>(manyToOneRequest);
                    }
                    break;
                case AddCustomerRelationshipParameterSet:
                    var customerRequest = BuildCustomerRequest();
                    var customerResponse = ExecuteOrganizationRequest<CreateCustomerRelationshipsResponse>(customerRequest);
                    break;
                default:
                    break;
            }

            switch (ParameterSetName)
            {
                case AddCustomerRelationshipParameterSet:

                    var accountRetrieveResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(
                        new RetrieveRelationshipRequest()
                        {
                            Name = AccountName
                        });
                    WriteObject(accountRetrieveResponse.RelationshipMetadata);

                    var contactRetrieveResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(
                        new RetrieveRelationshipRequest()
                        {
                            Name = ContactName
                        });
                    WriteObject(contactRetrieveResponse.RelationshipMetadata);
                    break;
                default:
                    var retrieveResponse = ExecuteOrganizationRequest<RetrieveRelationshipResponse>(
                        new RetrieveRelationshipRequest()
                        {
                            Name = Name
                        });
                    WriteObject(retrieveResponse.RelationshipMetadata);
                    break;
            }
        }

        private CreateManyToManyRequest BuildManyToManyRequest()
        {
            var request = new CreateManyToManyRequest()
            {
                IntersectEntitySchemaName = Intersect,
                ManyToManyRelationship = new ManyToManyRelationshipMetadata()
                {
                    SchemaName = Name,
                    Entity1LogicalName = Table,
                    Entity2LogicalName = RelatedTable,
                    IsValidForAdvancedFind = Searchable.ToBool()
                }
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(MenuConfiguration)))
                request.ManyToManyRelationship.Entity1AssociatedMenuConfiguration = MenuConfiguration;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(RelatedMenuConfiguration)))
                request.ManyToManyRelationship.Entity2AssociatedMenuConfiguration = RelatedMenuConfiguration;

            return request;
        }

        private CreateOneToManyRequest BuildManyToOneRequest(LookupAttributeMetadata existingLookupAttribute = null)
        {
            var request = new CreateOneToManyRequest()
            {
                // Defines the lookup to create on the Referencing table
                Lookup = existingLookupAttribute ?? BuildLookupAttribute(),
                // Defines the relationship to create to support the lookup
                OneToManyRelationship = BuildOneToManyRelationship(),
            };

            return request;
        }

        private CreatePolymorphicLookupAttributeRequest BuildPolymorphicRequest()
        {
            var request = new CreatePolymorphicLookupAttributeRequest()
            {
                Lookup = BuildLookupAttribute(),
                OneToManyRelationships = new OneToManyRelationshipMetadata[] { BuildOneToManyRelationship() }
            };

            return request;
        }

        private CreateCustomerRelationshipsRequest BuildCustomerRequest()
        {
            var request = new CreateCustomerRelationshipsRequest()
            {
                Lookup = BuildLookupAttribute(),
                OneToManyRelationships = new OneToManyRelationshipMetadata[]
                {
                    new OneToManyRelationshipMetadata()
                    {
                          ReferencedEntity = "account",
                          ReferencingEntity = Table,
                          SchemaName = AccountName,
                          IsValidForAdvancedFind = Searchable.ToBool()
                    },
                    new OneToManyRelationshipMetadata()
                    {
                          ReferencedEntity = "contact",
                          ReferencingEntity = Table,
                          SchemaName = ContactName,
                          IsValidForAdvancedFind = Searchable.ToBool()
                    }
                }
            };

            return request;
        }

        private LookupAttributeMetadata BuildLookupAttribute()
        {
            var attribute = new LookupAttributeMetadata
            {
                LogicalName = ColumnName,
                SchemaName = ColumnName,
                DisplayName = new Label(ColumnDisplayName, Session.Current.LanguageId),
                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                IsValidForAdvancedFind = new BooleanManagedProperty(Searchable.ToBool())
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnRequired)))
                attribute.RequiredLevel = new AttributeRequiredLevelManagedProperty((AttributeRequiredLevel)ColumnRequired);

            if (MyInvocation.BoundParameters.ContainsKey(nameof(ColumnDescription)))
                attribute.Description = new Label(ColumnDescription, Session.Current.LanguageId);

            return attribute;
        }

        private OneToManyRelationshipMetadata BuildOneToManyRelationship()
        {
            var relationship = new OneToManyRelationshipMetadata
            {
                ReferencedEntity = RelatedTable,
                ReferencingEntity = Table,
                SchemaName = Name,
                IsValidForAdvancedFind = Searchable.ToBool()
            };

            if (MyInvocation.BoundParameters.ContainsKey(nameof(MenuConfiguration)))
                relationship.AssociatedMenuConfiguration = MenuConfiguration;

            if (MyInvocation.BoundParameters.ContainsKey(nameof(Behavior)))
                relationship.CascadeConfiguration = Behavior;

            return relationship;
        }
    }
}
