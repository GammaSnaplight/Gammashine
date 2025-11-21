#if UNITY_EDITOR

using System.IO;

using UnityEditor;

using UnityEngine;

namespace Snaplight.Bindfold.UnityEditor
{
    sealed class FBXToPrefabricated
    {
        private const string _path = "Assets/Prefabricated";

        [MenuItem("Snaplight/FBX to Prefabricates")]
        private static void CollectionPrefabricates()
        {
            //---
            Directory.CreateDirectory(_path);

            //---
            if (Selection.objects.Length == 0)
            {
                Debug.LogAssertion("Selection Objects equal zero.");
                return;
            }

            //---
            foreach (Object obj in Selection.objects)
            {
                GameObject fbx = obj as GameObject;
                if (fbx == null) continue;

                string prefabPath = $"{_path}/{fbx.name}.prefab";

                PrefabUtility.SaveAsPrefabAsset(fbx, prefabPath);
            }

            //---
            AssetDatabase.Refresh();
        }
    }
}

#endif