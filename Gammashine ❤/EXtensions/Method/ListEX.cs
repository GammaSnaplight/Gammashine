using System;
using System.Collections.Generic;

namespace Snaplight.Extension
{
    public static class ListEX
    {
        public static void Add<T>(this List<T> list, T item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(item);
            }
        }

        public static void Add<T>(this List<T> currentList, List<T> otherList)
        {
            foreach (T item in otherList)
            {
                currentList.Add(item);
            }
        }

        public static void Mix<T>(this List<T> list)
        {
            Random random = new();

            int count = list.Count;

            while (count > 1)
            {
                count--;
                int mix = random.Next(count + 1);
                (list[count], list[mix]) = (list[mix], list[count]);
            }
        }
    }
}