
using System;
using System.Collections.Generic;

namespace Logos.Utility
{
	// ElementComparer is a helper class for OrderedEnumerable that can compare items in a source array given their indexes.
	// It supports chaining multiple comparers together to allow secondary, tertiary, etc. sorting.
	internal abstract class ElementComparer<TSource>
	{
		// Compares the two items at the specified indexes.
		public abstract int Compare(int left, int right);

		// Creates all the keys needed to compare items.
		public abstract void CreateSortKeys(TSource[] source);

		// Creates a new ElementComparer by appending the specified ordering to this ordering, chaining them together.
		public abstract ElementComparer<TSource> Append(ElementComparer<TSource> next);
	}

	internal class ElementComparer<TSource, TKey> : ElementComparer<TSource>
	{
		public ElementComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending, ElementComparer<TSource> next)
		{
			m_keySelector = keySelector;
			m_comparer = comparer;
			m_isDescending = descending;
			m_next = next;
		}

		public override void CreateSortKeys(TSource[] source)
		{
			// create the sort key from each item in the source array
			m_keys = new TKey[source.Length];
			for (int index = 0; index < source.Length; index++)
				m_keys[index] = m_keySelector(source[index]);

			// delegate to next if necessary
			if (m_next != null)
				m_next.CreateSortKeys(source);
		}

		public override int Compare(int left, int right)
		{
			// invoke this level's comparer to get a basic result
			int result = m_comparer.Compare(m_keys[left], m_keys[right]);

			// if elements are different, return their relative order (inverting if descending)
			if (result != 0)
				return m_isDescending ? -result : result;

			// if there is a chained ordering, delegate to it
			if (m_next != null)
				return m_next.Compare(left, right);

			// elements are otherwise equal; to preserve stable sort, sort by original index
			return left - right;
		}

		public override ElementComparer<TSource> Append(ElementComparer<TSource> next)
		{
			// append the new ordering to the tail of the current chain
			var newNext = m_next == null ? next : m_next.Append(next);
			return new ElementComparer<TSource, TKey>(m_keySelector, m_comparer, m_isDescending, newNext);
		}

		readonly Func<TSource, TKey> m_keySelector;
		readonly IComparer<TKey> m_comparer;
		readonly bool m_isDescending;
		readonly ElementComparer<TSource> m_next;
		TKey[] m_keys;
	}
}
