using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Gammashine.Bindfolds.Unity.Editor
{
    [CustomPropertyDrawer(typeof(VAutonaming))]
    public class AutonamingAttributeDrawer : PropertyDrawer
    {
        private const float _padding = 5f;
        private const float _ObjectFieldWidth = 20f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // √арантируем, что ObjectField всегда фиксированной ширины
            float textFieldWidth = position.width - _ObjectFieldWidth - _padding;

            Rect textFieldRect = new Rect(position.x, position.y, textFieldWidth, position.height);
            Rect objectFieldRect = new Rect(position.x + textFieldWidth + _padding, position.y, _ObjectFieldWidth, position.height);

            GameObject obj = EditorGUI.ObjectField(objectFieldRect, null, typeof(GameObject), true) as GameObject;

            if (obj != null)
            {
                property.stringValue = obj.name;
            }

            property.stringValue = EditorGUI.TextField(textFieldRect, property.stringValue);

            EditorGUI.EndProperty();
        }
    }

}
