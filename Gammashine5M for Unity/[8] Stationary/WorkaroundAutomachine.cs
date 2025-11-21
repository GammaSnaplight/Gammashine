using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

namespace Gammashine.Automachinery
{
    public static class WorkaroundAutomachine
    {
        private static readonly Dictionary<string, FieldInfo> _fieldMemory = new();
        private static readonly Dictionary<string, Func<object, object>> _getterLightfastMemory = new();
        private static readonly Dictionary<string, Type> _typeMemory = new();

        // Поиск поля в иерархии
        private static FieldInfo FindFieldInHierarchy(Type type, string fieldName)
        {
            while (type != null)
            {
                FieldInfo fi = type.GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.Static |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (fi != null)
                    return fi;
                type = type.BaseType;
            }
            return null;
        }

        // Поиск свойства в иерархии
        private static PropertyInfo FindPropertyInHierarchy(Type type, string propertyName)
        {
            while (type != null)
            {
                PropertyInfo pi = type.GetProperty(propertyName,
                    BindingFlags.Instance | BindingFlags.Static |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (pi != null)
                    return pi;
                type = type.BaseType;
            }
            return null;
        }

        /// <summary>
        /// Получение FieldInfo с кэшированием и поддержкой иерархии.
        /// </summary>
        public static FieldInfo GatheringFieldInfo(Type type, string fieldName)
        {
            string key = $"{type.FullName}.{fieldName}";
            if (_fieldMemory.TryGetValue(key, out var cached) && cached != null)
                return cached;

            FieldInfo fi = FindFieldInHierarchy(type, fieldName) ?? throw new ArgumentException($"Поле '{fieldName}' не найдено в иерархии типа '{type.FullName}'");
            _fieldMemory[key] = fi;
            return fi;
        }

        /// <summary>
        /// Получение значения приватного поля (через Expression или reflection).
        /// </summary>
        public static object GatheringField(object obj, string fieldName)
        {
#if !UNITY_WEBGL
        Type type = obj.GetType();
        string key = $"{type.FullName}.{fieldName}.getter";

        if (!_getterLightfastMemory.TryGetValue(key, out Func<object, object> getter))
        {
            FieldInfo fi = GatheringFieldInfo(type, fieldName);

            ParameterExpression param = Expression.Parameter(typeof(object), "inst");
            Expression<Func<object, object>> expr = Expression.Lambda<Func<object, object>>(
                Expression.Convert(
                    Expression.Field(
                        Expression.Convert(param, type),
                        fi),
                    typeof(object)
                ),
                param
            );
            getter = expr.Compile();
            _getterLightfastMemory[key] = getter;
        }
        return getter(obj);
#else
            FieldInfo fi = GatheringFieldInfo(obj.GetType(), fieldName);
            return fi.GetValue(obj);
#endif
        }

        /// <summary>
        /// Установка значения приватного поля.
        /// </summary>
        public static void EstablishField(object obj, string fieldName, object value)
        {
            FieldInfo fi = GatheringFieldInfo(obj.GetType(), fieldName);
            fi.SetValue(obj, value);
        }

        /// <summary>
        /// Получение значения поля или свойства (приоритет — поле).
        /// </summary>
        public static object GetValueSmart(object obj, string name)
        {
            Type type = obj.GetType();

            FieldInfo fi = FindFieldInHierarchy(type, name);
            if (fi != null)
                return fi.GetValue(obj);

            PropertyInfo pi = FindPropertyInHierarchy(type, name);
            if (pi != null)
                return pi.GetValue(obj);

            throw new Exception($"Ни поле, ни свойство '{name}' не найдено в типе {type.FullName}");
        }

        /// <summary>
        /// Получение типа по строковому имени с кэшированием.
        /// </summary>
        public static Type GatheringType(string typeName)
        {
            if (_typeMemory.TryGetValue(typeName, out var cached))
                return cached;

            Type type = Type.GetType(typeName);
            if (type == null)
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetType(typeName);
                    if (type != null) break;
                }
            }
            if (type == null)
            {
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = asm.GetTypes().FirstOrDefault(t => t.Name == typeName);
                    if (type != null) break;
                }
            }

            _typeMemory[typeName] = type ?? throw new ArgumentException($"Тип '{typeName}' не найден.");
            return type;
        }

        /// <summary>
        /// Ищет первый объект данного типа в сцене по строковому имени.
        /// </summary>
        public static object SinglefirstPathfindTypename(string typeName)
        {
            Type type = GatheringType(typeName);
            return UnityEngine.Object.FindAnyObjectByType(type);
        }

        /// <summary>
        /// Generic: ищет первый объект типа.
        /// </summary>
        public static T FindFirstOfType<T>()
            where T : UnityEngine.Object
        {
            return UnityEngine.Object.FindObjectOfType<T>();
        }

        /// <summary>
        /// Выводит все поля объекта (отладка).
        /// </summary>
        public static void DebugAllFields(object obj)
        {
            Type type = obj.GetType();
            Debug.Log($"== Поля для типа {type.FullName} ==");
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                var value = field.GetValue(obj);
                Debug.Log($"[{field.FieldType.Name}] {field.Name} = {value}");
            }
        }

        /// <summary>
        /// Выводит все свойства объекта (отладка).
        /// </summary>
        public static void DebugAllProperties(object obj)
        {
            Type type = obj.GetType();
            Debug.Log($"== Свойства для типа {type.FullName} ==");
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                object value;
                try
                {
                    value = prop.GetValue(obj);
                }
                catch
                {
                    value = "n/a";
                }

                Debug.Log($"[{prop.PropertyType.Name}] {prop.Name} = {value}");
            }
        }

        public static object ProfoundGatheringField(object obj, string path)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));

            string[] parts = path.Split('.');
            object currentObject = obj;
            Type currentType = obj.GetType();

            foreach (string part in parts)
            {
                FieldInfo field
                    = currentType.GetField(part, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    ?? throw new MissingFieldException($"Field '{part}' not found in type '{currentType.FullName}'.");

                currentObject = field.GetValue(currentObject);
                if (currentObject == null)
                    return null;

                currentType = currentObject.GetType();
            }

            return currentObject;
        }

        public static void ProfoundEstablishField(object obj, string path, object value)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty.", nameof(path));

            string[] parts = path.Split('.');
            object currentObject = obj;
            Type currentType = obj.GetType();

            for (int i = 0; i < parts.Length - 1; i++)
            {
                FieldInfo field = currentType.GetField(parts[i], BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (field == null)
                    throw new MissingFieldException($"Field '{parts[i]}' not found in type '{currentType.FullName}'.");

                currentObject = field.GetValue(currentObject);
                if (currentObject == null)
                    throw new NullReferenceException($"Field '{parts[i]}' in type '{currentType.FullName}' is null.");

                currentType = currentObject.GetType();
            }

            string lastPart = parts[^1];
            FieldInfo targetField = currentType.GetField(lastPart, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (targetField == null)
                throw new MissingFieldException($"Field '{lastPart}' not found in type '{currentType.FullName}'.");

            targetField.SetValue(currentObject, value);
        }

        public static void ValidateFields(object obj, string contextName = null)
        {
            if (obj == null)
            {
                Debug.LogError($"[Validation] Object is null (Context: {contextName})");
                return;
            }

            var type = obj.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                // Пропускаем помеченные [NonSerialized]
                if (field.IsNotSerialized) continue;

                var value = field.GetValue(obj);

                // Проверка на null
                if (value == null)
                {
                    Debug.LogError($"[Validation: {type.Name}] Field '{field.Name}' is null (Context: {contextName})");
                    continue;
                }

                // Проверка на пустую строку
                if (value is string str && string.IsNullOrWhiteSpace(str))
                {
                    Debug.LogError($"[Validation: {type.Name}] Field '{field.Name}' is empty string (Context: {contextName})");
                    continue;
                }

                // Проверка на пустые списки
                if (value is System.Collections.ICollection collection && collection.Count == 0)
                {
                    Debug.LogWarning($"[Validation: {type.Name}] Field '{field.Name}' collection is empty (Context: {contextName})");
                    continue;
                }
            }
        }
    }
}