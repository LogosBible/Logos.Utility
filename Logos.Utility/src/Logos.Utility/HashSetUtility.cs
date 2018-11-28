using System.Collections.Generic;

namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for working with <see cref="HashSet{T}"/>.
	/// </summary>
	public static class HashSetUtility
	{
		/// <summary>
		/// Returns a read-only wrapper around <paramref name="set"/>.
		/// </summary>
		/// <typeparam name="T">The type of object in the set.</typeparam>
		/// <param name="set">The <see cref="HashSet{T}"/> to wrap.</param>
		/// <returns>A new <see cref="ReadOnlyHashSet{T}"/> that wraps <paramref name="set"/>.</returns>
		public static ReadOnlyHashSet<T> AsReadOnly<T>(this HashSet<T> set)
		{
			return new ReadOnlyHashSet<T>(set);
		}
	}
}
