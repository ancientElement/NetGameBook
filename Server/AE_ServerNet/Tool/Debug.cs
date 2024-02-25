using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE_ServerNet
{
    public static class Debug
    {
        public static void Log(string msg)
        {
#if UnityEngine
            UnityEngine.Debug.log(msg);
#else
            Console.WriteLine(msg);
#endif
        }
    }
}
