using System.Management.Automation;

namespace AMSoftware.Dataverse.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "DataverseSession")]
    [OutputType(typeof(Session))]
    public sealed class GetSessionCommand : CmdletBase
    {
        protected override void Execute()
        {
            WriteObject(Session.Current);
        }
    }
}
