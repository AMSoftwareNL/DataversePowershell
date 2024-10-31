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
using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.DynamicParameters
{
    public sealed class FileColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 32768)]
        [ValidateRange(1, 131072)]
        public int MaxSizeInKB { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new FileAttributeMetadata()
            {
                MaxSizeInKB = 32768
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as FileAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;
        }
    }
}
