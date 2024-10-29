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
