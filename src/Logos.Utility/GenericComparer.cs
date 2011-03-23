
using System;
using System.Collections.Generic;

namespace Logos.Utility
{
	/// <summary>
	/// Implements the IComparer{T} interface with a delegate.
	/// </summary>
	/// <typeparam name="T">Type of the object to compare.</typeparam>
	public sealed class GenericComparer<T> : Comparer<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GenericComparer{T}"/> class.
		/// </summary>
		/// <param name="comparer">The comparer delegate.</param>
		public GenericComparer(Func<T, T, int> comparer)
		{
			if (comparer == null)
				throw new ArgumentNullException("comparer");

			m_comparer = comparer;
		}

		/// <summary>
		/// Performs a comparison of two objects of the same type.
		/// </summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>Less than zero: x is less than y. Zero: x equals y. Greater than zero: x is greater than y.</returns>
		public override int Compare(T x, T y)
		{
			return m_comparer(x, y);
		}

		readonly Func<T, T, int> m_comparer;
	}
}
