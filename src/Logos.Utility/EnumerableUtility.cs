
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
	}
}
