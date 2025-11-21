using System;
using System.Runtime.CompilerServices;

using UnityEngine;

namespace Snaplight.VisualizationEngine
{
    public static partial class VEngine
    {
        public static string Auto([CallerFilePath] string path = "", [CallerLineNumber] int line = 0, [CallerMemberName] string method = "")
            => $"{path} : ({line}) {method}";

        public static string Writeline()
            => $"{Auto()} - MEOW!";

        public static string Writeline(object info)
            => $"{Auto()} - {info}";
    }
}
