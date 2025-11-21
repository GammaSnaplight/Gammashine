using UnityEngine;

namespace Snaplight
{
    sealed class AutomateInitialization
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutomateCollection()
        {
            
        }
    }
}
