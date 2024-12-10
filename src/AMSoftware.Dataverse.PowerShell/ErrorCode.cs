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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell
{
    internal static class ErrorCode
    {
        public const string BatchOperationResponseItemFaulted = "DVPS0001";
        public const string LanguageNotInstalled = "DVPS0002";
        public const string UnknownUnmanagedSolution = "DVPS0003";
        public const string InvalidBatchId = "DVPS0004";
        public const string FaultedBatchExecution = "DVPS0005";
        public const string FaultedBatchInitialization = "DVPS0006";
        public const string FaultedTransactionExecution = "DVPS0007";
    }
}
