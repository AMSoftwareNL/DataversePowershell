using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class DateTimeColumnParameters : ColumnTypeParametersBase
    {
        internal DateTimeColumnParameters(PSCmdlet cmdletContext) : base(cmdletContext)
        {
        }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DateTimeFormat.DateAndTime)]
        public DateTimeFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 1)]
        public DateTimeBehavior DateTimeBehavior { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata BuildAttributeMetadata()
        {
            var result = new DateTimeAttributeMetadata()
            {
                Format = DateTimeFormat.DateAndTime,
                DateTimeBehavior = DateTimeBehavior.UserLocal,
                ImeMode = ImeMode.Auto
            };

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            if (_cmdletContext.MyInvocation.BoundParameters.ContainsKey(nameof(DateTimeBehavior)))
                result.DateTimeBehavior = DateTimeBehavior;

            return result;
        }
    }
}
