#define NEOS_PLUGIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NEOS_PLUGIN
using BaseX;
#endif

namespace NeosXS
{
#if NEOS_PLUGIN
    class LogHelper
    {
        public static void Debug(object objToDebug)
        {
            UniLog.Log("[NeosXS] (DEBUG) " + objToDebug);
        }

        public static void Warn(object objToDebug)
        {
            UniLog.Warning("[NeosXS] (WARN) " + objToDebug);
        }

        public static void Error(object objToDebug)
        {
            UniLog.Error("[NeosXS] (ERROR) " + objToDebug);
        }
    }
#else
    class LogHelper
    {
        public static void Debug(object objToDebug)
        {
            Console.WriteLine("[NeosXS] (DEBUG)" + objToDebug);
        }

        public static void Warn(object objToDebug)
        {
            Console.WriteLine("[NeosXS] (WARN)" + objToDebug);
        }

        public static void Error(object objToDebug)
        {
            Console.WriteLine("[NeosXS] (ERROR)" + objToDebug);
        }
    }
#endif
}
