using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreCms.Net.Core.Attrbute
{
    public interface IAOPContext
    {
        IServiceProvider ServiceProvider { get; }
        object[] Arguments { get; }
        Type[] GenericArguments { get; }
        MethodInfo Method { get; }
        MethodInfo MethodInvocationTarget { get; }
        object Proxy { get; }
        object ReturnValue { get; set; }
        Type TargetType { get; }
        object InvocationTarget { get; }
    }
}
