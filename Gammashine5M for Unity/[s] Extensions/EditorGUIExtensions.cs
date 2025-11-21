#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Snaplight.Extension
{
    public static class EditorGUIExtensions
    {
        public static Texture2D CreateColorBox(this EditorGUI GUI, int width, int height, Color color)
        {
            Color32 col32 = color;
            Color32[] pix = new Color32[width * height];

            for (int i = 0; i < pix.Length; i++) pix[i] = col32;

            Texture2D result = new(width, height);

            result.SetPixels32(pix);
            result.Apply();

            return result;
        }

        //public static string ArrayElementType(this EditorGUI GUI, string text)
        //{
        //    string input = text;
        //    int startIndex = input.IndexOf('$') + 1;
        //    int length = input.IndexOf('>') - startIndex;

        //    return input.Substring(startIndex, length);
        //}
    }
}
#endif