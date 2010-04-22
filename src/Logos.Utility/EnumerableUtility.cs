
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logos.Utility
{
	/// <summary>
	/// Provides helper methods for working with <see cref="IEnumerable{T}"/>.
	/// </summary>
	public static class EnumerableUtility
	{
		/// <summary>
		/// Returns the source sequence, or an empty sequence if <paramref name="source"/> is <c>null</c>.
		/// </summary>
		/// <param name="source">The source sequence.</param>
		/// <returns>The source sequence, or an empty sequence if <paramref name="source"/> is <c>null</c>.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/03/emptyifnull.html">EmptyIfNull</a>.</remarks>
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
		{
			return source ?? Enumerable.Empty<T>();
		}

		/// <summary>
		/// Sorts the elements of a sequence in ascending order according to a key.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.</returns>
		/// <remarks>This method only sorts as much of <paramref name="source"/> as is required to yield the
		/// elements that are requested from the return value.</remarks>
		public static IOrderedEnumerable<TSource> LazyOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return LazyOrderBy(source, keySelector, null, false);
		}

		/// <summary>
		/// Sorts the elements of a sequence in ascending order according to a key.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.</returns>
		/// <remarks>This method only sorts as much of <paramref name="source"/> as is required to yield the
		/// elements that are requested from the return value.</remarks>
		public static IOrderedEnumerable<TSource> LazyOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return LazyOrderBy(source, keySelector, comparer, false);
		}

		/// <summary>
		/// Sorts the elements of a sequence in descending order according to a key.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.</returns>
		/// <remarks>This method only sorts as much of <paramref name="source"/> as is required to yield the
		/// elements that are requested from the return value.</remarks>
		public static IOrderedEnumerable<TSource> LazyOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			return LazyOrderBy(source, keySelector, null, true);
		}

		/// <summary>
		/// Sorts the elements of a sequence in descending order according to a key.
		/// </summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> whose elements are sorted according to a key.</returns>
		/// <remarks>This method only sorts as much of <paramref name="source"/> as is required to yield the
		/// elements that are requested from the return value.</remarks>
		public static IOrderedEnumerable<TSource> LazyOrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return LazyOrderBy(source, keySelector, comparer, true);
		}

		private static IOrderedEnumerable<TSource> LazyOrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (keySelector == null)
				throw new ArgumentNullException("keySelector");

			return new OrderedEnumerable<TSource>(source, new ElementComparer<TSource, TKey>(keySelector, comparer ?? Comparer<TKey>.Default, descending, null));
		}

		/// <summary>
		/// Computes the sum of a sequence of <see cref="Nullable{Decimal}"/> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="Nullable{Decimal}"/> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <remarks>This method returns zero if <paramref name="source"/> contains no elements.</remarks>
		public static decimal? NullableSum(this IEnumerable<decimal?> source)
		{
			return source.Aggregate((decimal?) 0, (sum, value) => sum + value );
		}

		/// <summary>
		/// Computes the sum of a sequence of <see cref="Nullable{Double}"/> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="Nullable{Double}"/> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <remarks>This method returns zero if <paramref name="source"/> contains no elements.</remarks>
		public static double? NullableSum(this IEnumerable<double?> source)
		{
			return source.Aggregate((double?) 0, (sum, value) => sum + value);
		}

		/// <summary>
		/// Computes the sum of a sequence of <see cref="Nullable{Int32}"/> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="Nullable{Int32}"/> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <remarks>This method returns zero if <paramref name="source"/> contains no elements.</remarks>
		public static int? NullableSum(this IEnumerable<int?> source)
		{
			return source.Aggregate((int?) 0, (sum, value) => { return checked(sum + value); });
		}

		/// <summary>
		/// Computes the sum of a sequence of <see cref="Nullable{Int64}"/> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="Nullable{Int64}"/> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <remarks>This method returns zero if <paramref name="source"/> contains no elements.</remarks>
		public static long? NullableSum(this IEnumerable<long?> source)
		{
			return source.Aggregate((long?) 0, (sum, value) => { return checked(sum + value); });
		}

		/// <summary>
		/// Computes the sum of a sequence of <see cref="Nullable{Single}"/> values.
		/// </summary>
		/// <param name="source">A sequence of <see cref="Nullable{Single}"/> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <remarks>This method returns zero if <paramref name="source"/> contains no elements.</remarks>
		public static float? NullableSum(this IEnumerable<float?> source)
		{
			return source.Aggregate((float?) 0, (sum, value) => sum + value);
		}

		/// <summary>
		/// Returns all the elements in the specified collection that are not null.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that are not null.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2010/04/wherenotnull_extension_method.html">WhereNotNull Extension Method</a>.</remarks>
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
			where T : class
		{
			return source.Where(x => x != null);
		}

		/// <summary>
		/// Returns all the elements in the specified collection that are not null.
		/// </summary>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that are not null.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2010/04/wherenotnull_extension_method.html">WhereNotNull Extension Method</a>.</remarks>
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
			where T : struct
		{
			if (source == null)
				throw new ArgumentNullException("source");
			return WhereNotNullImpl(source);
		}

		private static IEnumerable<T> WhereNotNullImpl<T>(this IEnumerable<T?> source)
			where T : struct
		{
			foreach (T? t in source)
			{
				if (t.HasValue)
					yield return t.Value;
			}
		}
	}
}
