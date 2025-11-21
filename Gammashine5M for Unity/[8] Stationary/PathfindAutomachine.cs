using System;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Automachinery
{
    public static class PathfindAutomachine
    {
        private static readonly Dictionary<Type, HashSet<Component>> _typeCache = new();
        private static readonly Dictionary<string, GameObject> _nameCache = new();

        public static void Register(Component component)
        {
            if (component == null) return;

            Type type = component.GetType();

            if (!_typeCache.TryGetValue(type, out var set))
            {
                set = new HashSet<Component>();
                _typeCache[type] = set;
            }

            set.Add(component);

            string name = component.gameObject.name;
            if (!_nameCache.ContainsKey(name))
                _nameCache[name] = component.gameObject;
        }

        public static void Unregister(Component component)
        {
            if (component == null) return;

            Type type = component.GetType();

            if (_typeCache.TryGetValue(type, out var set))
            {
                set.Remove(component);
                if (set.Count == 0)
                    _typeCache.Remove(type);
            }

            string name = component.gameObject.name;
            if (_nameCache.TryGetValue(name, out var go) && go == component.gameObject)
                _nameCache.Remove(name);
        }

        public static T PathfindTypewhere<T>() where T : Component
        {
            if (_typeCache.TryGetValue(typeof(T), out var set))
            {
                foreach (var item in set)
                    if (item != null)
                        return item as T;
            }

            T found = UnityEngine.Object.FindObjectOfType<T>();
            if (found != null)
                Register(found);
            return found;
        }

        public static T[] PathfindsTypewhere<T>() where T : Component
        {
            if (_typeCache.TryGetValue(typeof(T), out var set))
            {
                List<T> result = new();
                foreach (var item in set)
                    if (item != null)
                        result.Add(item as T);
                return result.ToArray();
            }

            T[] found = UnityEngine.Object.FindObjectsOfType<T>();
            foreach (var item in found)
                Register(item);
            return found;
        }

        public static GameObject PathfindName(string name)
        {
            if (_nameCache.TryGetValue(name, out var go) && go != null)
                return go;

            GameObject found = GameObject.Find(name);
            if (found != null)
                _nameCache[name] = found;

            return found;
        }

        public static T PathfindComponent<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            T comp = go.GetComponent<T>();
            if (comp != null)
                Register(comp);
            return comp;
        }

        public static T GatheringComponent<T>(GameObject go) where T : Component
        {
            if (go == null) return null;

            if (_typeCache.TryGetValue(typeof(T), out var set))
            {
                foreach (var item in set)
                {
                    if (item == null) continue;

                    if (item.gameObject == go)
                        return item as T;
                }
            }

            // fallback (если не нашли в кеше)
            T component = go.GetComponent<T>();
            if (component != null)
                Register(component);

            return component;
        }

        public static void Clearfull()
        {
            _typeCache.Clear();
            _nameCache.Clear();
        }
    }
}
