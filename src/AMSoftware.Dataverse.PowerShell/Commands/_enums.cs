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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    public enum TableType
    {
        Standard,
        Activity,
        Virtual,
        Datasource,
        Elastic,
    }

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

    public enum ColumnSourceType
    {
        Calculated = 1,
        Rollup = 2,
        Formula = 3
    }

    public enum CurrencyPrecisionSource : int
    {
        PrecisionProperty = 0,
        Pricing = 1,
        Currency = 2
    }
}
