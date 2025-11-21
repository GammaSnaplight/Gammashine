using UnityEngine;
using UnityEditor;

using Snaplight;
using Snaplight.Extension;
using System;

namespace Gammashine.Bindfolds.Unity.Editor
{
    [CustomPropertyDrawer(typeof(VMeAttribute))]
    public class MeAttributeDrawer : DecoratorDrawer
    {
        private GUIStyle _gui;
        private GUIStyle _background;

        public override void OnGUI(Rect position)
        {
            _gui = new GUIStyle
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal = { textColor = Colorful.BlueBalmy }
            };

            _background = new GUIStyle();
            _background.normal.background = EditorGUIExtensions.CreateColorBox(new EditorGUI(), 20, 20, Colorful.Mix(Colorful.BlueBalmy, 1, 5));

            VMeAttribute me = (VMeAttribute)attribute;

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginVertical(_background);

            EditorGUILayout.LabelField($"◆ {me.Name}", _gui);

            EditorGUILayout.EndVertical();
        }

        public override float GetHeight()
        {
            return base.GetHeight() * 1.75f;
        }
    }
}