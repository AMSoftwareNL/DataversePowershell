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
using System.Management.Automation;
using System.Reflection;
using System.Runtime.Loader;

namespace AMSoftware.Dataverse.PowerShell
{
    // Custom AssemblyLoader is needed because of dependency conflicts
    // between PowerShell and Microsoft.PowerPlatfrom.Dataverse.Client.
    // For some reason this isn't solved in PowerShell core, but has to be sovled for each module.
    // Dispite Microsoft having complete documentation about the problem and how to solve it. 
    // https://learn.microsoft.com/en-us/powershell/scripting/dev-cross-plat/resolving-dependency-conflicts
    // https://github.com/daxian-dbw/PowerShell-ALC-Samples

    public class ModuleAssemblyLoader : IModuleAssemblyInitializer, IModuleAssemblyCleanup
    {
        public void OnImport()
        {
            AssemblyLoadContext.Default.Resolving += ModuleAssemblyLoadContext.ResolvingHandler;
        }

        public void OnRemove(PSModuleInfo module)
        {
            AssemblyLoadContext.Default.Resolving -= ModuleAssemblyLoadContext.ResolvingHandler;
        }
    }
}
