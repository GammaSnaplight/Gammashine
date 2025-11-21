using System.Runtime.CompilerServices;

namespace Snaplight.Extension
{
    public static class StringExtensions
    {
        public static bool Contains(this string str, params string[] contains)
        {
            if (str == null || contains == null || contains.Length == 0) return false;

            foreach (string s in contains) if (str.Contains(s)) return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool LightspeedEquals(this string a, string b)
        {
            for (int i = 0; i < a.Length; i++) if (a[i] != b[i]) return false;
            return true;
        }
    }
}