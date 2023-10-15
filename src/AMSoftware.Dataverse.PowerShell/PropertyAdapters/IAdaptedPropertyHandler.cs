using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSoftware.Dataverse.PowerShell.PropertyAdapters
{
    internal interface IAdaptedPropertyHandler<T>
    {
        string TypeName { get; }
        bool IsSettable { get; }
        bool IsGettable { get; }

        object GetValue(T baseObject);
        void SetValue(T baseObject, object value);
    }
}
