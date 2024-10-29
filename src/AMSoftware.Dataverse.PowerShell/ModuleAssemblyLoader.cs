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
