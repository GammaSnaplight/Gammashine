using System;
using System.Collections.Generic;

using UnityEngine;

using UObject = UnityEngine.Object;

namespace Gammashine.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetActives(bool boolean, params GameObject[] gameObjects)
        {
            foreach (GameObject go in gameObjects) go.SetActive(boolean);
        }

        public static void RemoveComponent<T>(this GameObject go) where T : Component
        {
            if (go.TryGetComponent(out T component))
            {
                UObject.Destroy(component);
            }
            else throw new NullReferenceException();
        }

        public static List<T> Childs<T>(this GameObject go)
        {
            List<T> gameObjects = new(go.transform.childCount);

            for (int i = 0; i < go.transform.childCount; i++)
            {
                gameObjects.Add(go.transform.GetChild(i).GetComponent<T>());
            }

            return gameObjects;
        }

        public static void RenamingEnumeration<T>(this GameObject go, string rename, bool isIndexFirstNumber = false)
        {
            List<GameObject> gameObjects = go.Childs<GameObject>();

            int index = 0;

            if (isIndexFirstNumber) index++;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                gameObjects[i].transform.GetChild(i).name = $"{rename}: {i + index}";
            }
        }

        public static void MirrorPositionAndRotation(this GameObject go1, GameObject go2)
        {
            Transform t1 = go1.transform;
            Transform t2 = go2.transform;

            t1.GetPositionAndRotation(out Vector3 tempPosition, out Quaternion tempRotation);
            Transform tempParent = t1.parent;

            t1.SetParent(t2.parent, true);
            t1.SetPositionAndRotation(t2.position, t2.rotation);

            t2.SetParent(tempParent, true);
            t2.SetPositionAndRotation(tempPosition, tempRotation);
        }

        public static void ReplacePositionAndRotation(this GameObject target, GameObject source)
        {
            Transform sourceTransform = source.transform;
            Transform targetTransform = target.transform;

            targetTransform.SetParent(sourceTransform.parent, true);

            targetTransform.SetPositionAndRotation(sourceTransform.position, sourceTransform.rotation);
        }
    }
}