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
using AMSoftware.Dataverse.PowerShell.Commands;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public abstract class ColumnTypeParametersBase
    {
        protected internal ColumnTypeParametersBase() { }

        internal static ColumnTypeParametersBase Create(ColumnType columnType)
        {
            switch (columnType)
            {
                case ColumnType.Boolean:
                    return new BooleanColumnParameters();
                case ColumnType.DateTime:
                    return new DateTimeColumnParameters();
                case ColumnType.Decimal:
                    return new DecimalColumnParameters();
                case ColumnType.Double:
                    return new DoubleColumnParameters();
                case ColumnType.Integer:
                    return new IntegerColumnParameters();
                case ColumnType.Memo:
                    return new MemoColumnParameters();
                case ColumnType.Money:
                    return new MoneyColumnParameters();
                case ColumnType.String:
                    return new StringColumnParameters();
                case ColumnType.BigInt:
                    return new BigIntColumnParameters();
                case ColumnType.Picklist:
                    return new PicklistColumnParameters();
                case ColumnType.MultiSelectPicklist:
                    return new MultiSelectPicklistColumnParameters();
                case ColumnType.Image:
                    return new ImageColumnParameters();
                case ColumnType.File:
                    return new FileColumnParameters();
                default:
                    throw new NotSupportedException();
            }
        }

        internal static ColumnTypeParametersBase Create(AttributeMetadata attribute)
        {
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.BooleanType)
                return new BooleanColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.DateTimeType)
                return new DateTimeColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.DecimalType)
                return new DecimalColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.DoubleType)
                return new DoubleColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.IntegerType)
                return new IntegerColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.MemoType)
                return new MemoColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.MoneyType)
                return new MoneyColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.StringType)
                return new StringColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.BigIntType)
                return new BigIntColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.PicklistType)
                return new PicklistColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.MultiSelectPicklistType)
                return new MultiSelectPicklistColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.ImageType)
                return new ImageColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.FileType)
                return new FileColumnParameters();

            return null;
        }

        internal abstract AttributeMetadata CreateAttributeMetadata();

        internal abstract void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute);
    }
}
