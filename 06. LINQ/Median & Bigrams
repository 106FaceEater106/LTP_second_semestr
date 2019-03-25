using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
		public static double Median(this IEnumerable<double> items)
		{
			var sortedItems = items.ToList();
			if (!sortedItems.Any()) throw new InvalidOperationException();
			
			sortedItems.Sort();
			return sortedItems.Count % 2 == 0
				? (sortedItems[sortedItems.Count / 2] + sortedItems[sortedItems.Count / 2 - 1]) / 2
				: sortedItems[sortedItems.Count / 2];
		}

		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var enumeratorItems = items.GetEnumerator();
			enumeratorItems.MoveNext();
			var prevousItem = enumeratorItems.Current;
			
			while (enumeratorItems.MoveNext())
			{
				yield return Tuple.Create(prevousItem, enumeratorItems.Current);
				prevousItem = enumeratorItems.Current;
			}
		}
	}
}
