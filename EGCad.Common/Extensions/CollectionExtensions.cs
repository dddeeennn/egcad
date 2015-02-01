using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace EGCad.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            //code contract : precondition
            if (firstIndex == secondIndex ||
                !(firstIndex >= 0 && firstIndex < list.Count) ||
                !(secondIndex >= 0 && secondIndex < list.Count))
            {
                return;
            }

            var tmp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = tmp;
        }

        public static string ToStr(this int[] array)
        {
            return string.Join(",", array);
        }

        public static int[] Concat(this int[] first, params int[] second)
        {
            var result = new int[first.Length + second.Length];
            first.CopyTo(result, 0);
            second.CopyTo(result, first.Length);
            return result;
        }
    }
}
