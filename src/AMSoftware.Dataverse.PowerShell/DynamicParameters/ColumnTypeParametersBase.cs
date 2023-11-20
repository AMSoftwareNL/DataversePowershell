using AMSoftware.Dataverse.PowerShell.Commands;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public abstract class ColumnTypeParametersBase
    {
        protected readonly PSCmdlet _cmdletContext;

        protected ColumnTypeParametersBase(PSCmdlet cmdletContext)
        {
            _cmdletContext = cmdletContext;
        }

        internal static ColumnTypeParametersBase Create(PSCmdlet cmdletContext)
        {
            object columnTypeValue = null;

            if (cmdletContext.MyInvocation.BoundParameters.TryGetValue("Type", out columnTypeValue) && columnTypeValue is ColumnType columnType)
            {
                switch (columnType)
                {
                    case ColumnType.Boolean:
                        return new BooleanColumnParameters(cmdletContext);
                    case ColumnType.DateTime:
                        return new DateTimeColumnParameters(cmdletContext);
                    case ColumnType.Decimal:
                        return new DecimalColumnParameters(cmdletContext);
                    case ColumnType.Double:
                        return new DoubleColumnParameters(cmdletContext);
                    case ColumnType.Integer:
                        return new IntegerColumnParameters(cmdletContext);
                    case ColumnType.Customer:
                        return new CustomerColumnParameters(cmdletContext);
                    case ColumnType.Lookup:
                        return new LookupColumnParameters(cmdletContext);
                    case ColumnType.Memo:
                        return new MemoColumnParameters(cmdletContext);
                    case ColumnType.Money:
                        return new MoneyColumnParameters(cmdletContext);
                    case ColumnType.String:
                        return new StringColumnParameters(cmdletContext);
                    case ColumnType.BigInt:
                        return new BigIntColumnParameters(cmdletContext);
                    case ColumnType.Picklist:
                        return new PicklistColumnParameters(cmdletContext);
                    case ColumnType.MultiSelectPicklist:
                        return new MultiSelectPicklistColumnParameters(cmdletContext);
                    case ColumnType.Image:
                        return new ImageColumnParameters(cmdletContext);
                    case ColumnType.File:
                        return new FileColumnParameters(cmdletContext);
                    default:
                        throw new NotSupportedException();
                }
            }

            return null;
        }

        internal abstract AttributeMetadata BuildAttributeMetadata();
    }
}
