using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    public enum TableOwnershipType
    {
        User = 0x1,
        Organization = 0x8
    }

    public enum ColumnRequiredLevel
    {
        Optional = 0,
        Required = 1,
        Recommended = 3
    }

    public enum ColumnType
    {
        Boolean,
        Customer,
        DateTime,
        Decimal,
        Double,
        Integer,
        Lookup,
        Memo,
        Money,
        Owner,
        PartyList,
        Picklist,
        State,
        Status,
        String,
        Uniqueidentifier,
        CalendarRules,
        Virtual,
        BigInt,
        ManagedProperty,
        EntityName,
        Image,
        MultiSelectPicklist,
        File
    }

    public enum CurrencyPrecisionSource : int
    {
        PrecisionProperty = 0,
        Pricing = 1,
        Currency = 2
    }
}
