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
                case ColumnType.Customer:
                    return new CustomerColumnParameters();
                case ColumnType.Lookup:
                    return new LookupColumnParameters();
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
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.CustomerType)
                return new CustomerColumnParameters();
            if (attribute.AttributeTypeName == AttributeTypeDisplayName.LookupType)
                return new LookupColumnParameters();
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
