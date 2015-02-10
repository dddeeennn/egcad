using System;
using System.Collections.Generic;

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

		public static string ToStr(this double[] array, string separator)
		{
			return string.Join(separator, array);
		}

		public static string ToStr(this double[] array)
		{
			return string.Join(" , ", array);
		}

		public static int[] Concat(this int[] first, params int[] second)
		{
			var result = new int[first.Length + second.Length];
			first.CopyTo(result, 0);
			second.CopyTo(result, first.Length);
			return result;
		}

		public static double[] GetRow(this double[,] source, int rowIndex)
		{
			var result = new Double[source.GetLength(1)];

			for (var i = 0; i < source.GetLength(1); i++)
			{
				result[i] = source[rowIndex, i];
			}
			return result;
		}

		public static double[] GetColumn(this double[,] source, int columnIndex)
		{
			var result = new Double[source.GetLength(0)];

			for (var i = 0; i < source.GetLength(0); i++)
			{
				result[i] = source[i, columnIndex];
			}
			return result;
		}
	}
}
