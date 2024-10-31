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
using Microsoft.Xrm.Sdk;
using System;
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Converters
{
    public sealed class EntityConverter : PSTypeConverter
    {
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
        {
            return false;
        }

        public override bool CanConvertTo(object sourceValue, Type destinationType)
        {
            if (destinationType == typeof(Guid)) return true;
            if (destinationType == typeof(EntityReference)) return true;
            return false;
        }

        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            throw new NotSupportedException();
        }

        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
        {
            if (sourceValue != null && sourceValue is Entity entityValue)
            {
                if (destinationType == typeof(Guid)) return entityValue.Id;
                if (destinationType == typeof(EntityReference)) return entityValue.ToEntityReference();
            }

            if (sourceValue != null && sourceValue is EntityReference entityReferenceValue && destinationType == typeof(Guid))
            {
                return entityReferenceValue.Id;
            }

            throw new NotSupportedException();
        }
    }
}
