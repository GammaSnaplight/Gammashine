using UnityEngine;

namespace Snaplight.VisualizationEngine
{
    public static partial class VEngine
    {
        public static void WriteLog()
        {
            Debug.Log(Writeline());
        }

        public static void WriteLog(object info)
        {
            Debug.Log(Writeline(info));
        }
    }
}
