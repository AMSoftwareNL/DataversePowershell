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
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace AMSoftware.Dataverse.PowerShell
{
    // Custom AssemblyLoadContext to prevent conflicts with PowerShell dependencies
    // Explanations and samples online use a dependencies folder and and 'Engine' to force the load
    // of dependencies in the correct AssemblyLoadContext. 
    // This AssemblyLoadContext takes the easy route and tries to load everything from the module directory if it is there.
    // Which basically results in everything for the module running in its own AssemblyLoadContext. Seems to work like a charm.

    internal class ModuleAssemblyLoadContext : AssemblyLoadContext
    {
        private static ModuleAssemblyLoadContext _moduleLoadContext = new(
            Path.GetDirectoryName(typeof(ModuleAssemblyLoadContext).Assembly.Location));

        private string _dependencyFolder;

        private ModuleAssemblyLoadContext(string folder)
            : base(typeof(ModuleAssemblyLoadContext).Name, isCollectible: false)
        {
            _dependencyFolder = folder;
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            string path = Path.Combine(_dependencyFolder, assemblyName.Name) + ".dll";

            if (File.Exists(path))
            {
                return LoadFromAssemblyPath(path);
            }

            return null;
        }

        internal bool CanResolve(AssemblyName assemblyName)
        {
            string path = Path.Combine(_dependencyFolder, assemblyName.Name) + ".dll";

            return File.Exists(path);
        }

        internal static Assembly ResolvingHandler(AssemblyLoadContext context, AssemblyName name)
        {
            if (_moduleLoadContext.CanResolve(name))
            {
                return _moduleLoadContext.LoadFromAssemblyName(name);
            }
            else
            {
                return null;
            }
        }
    }
}
