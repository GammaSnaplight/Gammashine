#if UNITY_EDITOR

using byGammaSnaplight.Gammashine.Mathematics;

using System;

using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Gammashine.Bindfolds.Unity.Editor
{
    /// <summary>
    /// 💛 Данный класс автоматически сохраняет состояние в редакторе Unity.
    /// </summary>
    [InitializeOnLoad]
    public static class Autosave
    {
        // Actions
        /// <summary>💛 Подпишитесь на данное событие чтобы реагировать после каждого авто-сохранения.</summary>
        public static event Action Autosaved;

        // Serializable
        public static LogType LogTypeReactions;
        public static int SecondsInterval = 300;

        // Variables
        private static double _timer;
        //private static int Index;
        private static bool _isPlayback;

        static Autosave()
        {
            //---
            Initialize();
        }

        public static void Initialize()
        {
            //---
            EditorApplication.update += Playback;
            Application.logMessageReceived += ErrorCallback;

            //---
            _timer = EditorApplication.timeSinceStartup + SecondsInterval;
        }

        public static void Playback()
        {
            //---
            if (!_isPlayback)
            {
                Debug.LogAssertion("❌ Autosave is disabled due to errors in the console");
                return;
            }

            //---
            if (EditorApplication.timeSinceStartup >= _timer)
            {
                if (!EditorApplication.isPlaying)
                {
                    //---
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                    AssetDatabase.SaveAssets();

                    //Index++;

                    //---
                    _timer = EditorApplication.timeSinceStartup + SecondsInterval;
                    _timer = Mathlight.MinMax(_timer, 0, double.MaxValue);

                    if (_timer == double.MaxValue)
                    {
                        Debug.LogAssertion("❌ Autosave has been stopped! The stopwatch has gone beyond the double type");
                        Elimination();
                    }

                    //---
                    Debug.Log($"💽 Autosave");

                    //---
                    Autosaved?.Invoke();
                }
                else return;
            }
        }

        public static void Elimination()
        {
            EditorApplication.update -= Playback;
            Application.logMessageReceived -= ErrorCallback;
        }

        private static void ErrorCallback(string logString, string stackTrace, LogType type)
        {
            _isPlayback = type != LogTypeReactions;
        }
    }
}

#endif