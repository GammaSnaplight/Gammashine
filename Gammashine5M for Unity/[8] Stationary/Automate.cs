using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine
{
    public static class Automate
    {
        [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
        public class ModulableAttribute : Attribute { }

        public static void CollectionModule<T>(T realizationManifold, IManifoldable<IModulable> rawManifold) where T : class
        {
            if (realizationManifold == null) throw new ArgumentNullException(nameof(realizationManifold));

            Type type = typeof(T);
            List<IModulable> modules = new();

            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (typeof(IModulable).IsAssignableFrom(field.FieldType))
                {
                    if (field.GetValue(realizationManifold) is IModulable module)
                    {
                        module.Collection();
                        modules.Add(module);
                    }
                }
            }

            rawManifold.Modules = modules;
        }

        public static void CollectionModule<T, M>(T realizationManifold, IManifoldable<M> rawManifold)
            where M : class, IModulable
            where T : class
        {
            if (realizationManifold == null)
                throw new ArgumentNullException(nameof(realizationManifold));

            List<M> modules = new();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (FieldInfo field in typeof(T).GetFields(flags))
            {
                if (!field.IsDefined(typeof(ModulableAttribute), true))
                    continue;

                if (!typeof(M).IsAssignableFrom(field.FieldType))
                    continue;

                if (field.GetValue(realizationManifold) is not M module)
                    continue;

                module.Collection();

                modules.Add(module);
            }

            rawManifold.Modules = modules;
        }

        //public static void EliminationModule(IManifoldable<IModulable> manifold)
        //{
        //    if (manifold == null) throw new ArgumentNullException(nameof(manifold));

        //    Type type = typeof(IManifoldable<IModulable>);
        //    List<IModulable> modules = new();

        //    foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        //    {
        //        if (typeof(IModulable).IsAssignableFrom(field.FieldType))
        //        {
        //            if (field.GetValue(manifold) is IModulable module)
        //            {
        //                module.Elimination();     
        //            }
        //        }
        //    }

        //    modules.Clear();
        //}

        public static List<T> Gathering<T>(object obj)
        {
            List<T> r = new();
            Type type = obj.GetType();

            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (field.FieldType == typeof(T)) r.Add((T)field.GetValue(obj));
            }

            return r;
        }

        public static List<IMasterable<IManifoldable<IModulable>>> Masterminds<T>(List<GameObject> gameObjects)
            where T : IMasterable<IManifoldable<IModulable>>
        {
            List<IMasterable<IManifoldable<IModulable>>> minds = new();

            foreach (GameObject go in gameObjects)
            {
                Component[] components = go.GetComponents<Component>();

                foreach (Component component in components)
                {
                    Type componentType = component.GetType();
                    Type interfaceType = typeof(IMasterable<IManifoldable<IModulable>>);

                    if (interfaceType.IsAssignableFrom(componentType))
                    {
                        IMasterable<IManifoldable<IModulable>> mind = (IMasterable<IManifoldable<IModulable>>)component;

                        minds.Add(mind);
                    }
                }
            }

            return minds;
        }
    }
}