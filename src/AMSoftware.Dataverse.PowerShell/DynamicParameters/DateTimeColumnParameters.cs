﻿/* 
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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class DateTimeColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = DateTimeFormat.DateAndTime)]
        public DateTimeFormat Format { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 1)]
        public DateTimeBehavior Behavior { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = ImeMode.Auto)]
        public ImeMode ImeMode { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new DateTimeAttributeMetadata()
            {
                Format = DateTimeFormat.DateAndTime,
                DateTimeBehavior = DateTimeBehavior.UserLocal,
                ImeMode = ImeMode.Auto
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as DateTimeAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Format)))
                result.Format = Format;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(ImeMode)))
                result.ImeMode = ImeMode;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(Behavior)))
                result.DateTimeBehavior = Behavior;
        }
    }
}
