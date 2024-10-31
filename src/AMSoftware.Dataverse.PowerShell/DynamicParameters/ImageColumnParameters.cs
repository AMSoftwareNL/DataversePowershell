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
    public sealed class ImageColumnParameters : ColumnTypeParametersBase
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter IsPrimaryImage { get; set; }

        [Parameter(Mandatory = false)]
        [PSDefaultValue(Value = 10240)]
        [ValidateRange(1, 30720)]
        public int MaxSizeInKB { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter CanStoreFullImage { get; set; }

        internal override AttributeMetadata CreateAttributeMetadata()
        {
            var result = new ImageAttributeMetadata()
            {
                IsPrimaryImage = IsPrimaryImage.ToBool(),
                CanStoreFullImage = CanStoreFullImage.ToBool(),
                MaxSizeInKB = 10240
            };

            return result;
        }

        internal override void ApplyParameters(PSCmdlet context, ref AttributeMetadata attribute)
        {
            var result = attribute as ImageAttributeMetadata;

            if (context.MyInvocation.BoundParameters.ContainsKey(nameof(MaxSizeInKB)))
                result.MaxSizeInKB = MaxSizeInKB;
        }
    }
}
