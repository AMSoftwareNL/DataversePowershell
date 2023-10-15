using Microsoft.Xrm.Sdk;
using System;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal class AliasFieldPropertyHandler : TableFieldPropertyHandler
    {
        public AliasFieldPropertyHandler(string attributeName)
            : base(attributeName) { }

        public override bool IsSettable
        {
            get { return false; }
        }

        public override void SetValue(Entity baseObject, object value)
        {
            throw new NotSupportedException();
        }
    }

}
