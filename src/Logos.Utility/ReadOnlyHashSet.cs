using System;
using System.Collections;
using System.Collections.Generic;

namespace Logos.Utility
{
	/// <summary>
	/// Implements a read-only wrapper around a <see cref="HashSet{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of item in the ReadOnlyHashSet.</typeparam>
	public sealed class ReadOnlyHashSet<T> : ISet<T>, IReadOnlyCollection<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyHashSet{T}"/> class.
		/// </summary>
		/// <param name="baseSet">The <see cref="HashSet{T}"/> to wrap.</param>
		public ReadOnlyHashSet(HashSet<T> baseSet)
		{
			if (baseSet == null)
				throw new ArgumentNullException("baseSet");

			m_base = baseSet;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		public IEnumerator<T> GetEnumerator()
		{
			return m_base.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return m_base.GetEnumerator();
		}

		/// <summary>
		/// Adds an item to the collection. This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ICollection<T>.Add(T item)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Adds an item to the collection. This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		bool ISet<T>.Add(T item)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both.
		/// This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ISet<T>.UnionWith(IEnumerable<T> other)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Modifies the current set so that it contains only elements that are also in a specified collection.
		/// This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ISet<T>.IntersectWith(IEnumerable<T> other)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Removes all elements in the specified collection from the current set.
		/// This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ISet<T>.ExceptWith(IEnumerable<T> other)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.
		/// This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ISet<T>.SymmetricExceptWith(IEnumerable<T> other)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Determines whether a set is a subset of a specified collection.
		/// </summary>
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			return m_base.IsSubsetOf(other);
		}

		/// <summary>
		/// Determines whether the current set is a proper (strict) subset of a specified collection.
		/// </summary>
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			return m_base.IsProperSubsetOf(other);
		}

		/// <summary>
		/// Determines whether the current set is a superset of a specified collection.
		/// </summary>
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			return m_base.IsSupersetOf(other);
		}

		/// <summary>
		/// Determines whether the current set is a proper (strict) superset of a specified collection.
		/// </summary>
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			return m_base.IsProperSupersetOf(other);
		}

		/// <summary>
		/// Determines whether the current set overlaps with the specified collection.
		/// </summary>
		public bool Overlaps(IEnumerable<T> other)
		{
			return m_base.Overlaps(other);
		}

		/// <summary>
		/// Determines whether the current set and the specified collection contain the same elements.
		/// </summary>
		public bool SetEquals(IEnumerable<T> other)
		{
			return m_base.SetEquals(other);
		}

		/// <summary>
		/// Copies the elements of the ReadOnlyHashSet to an Array, starting at a particular Array index.
		/// </summary>
		public void CopyTo(T[] array, int arrayIndex)
		{
			m_base.CopyTo(array, arrayIndex);
		}

		/// <summary>
		/// Removes all items from the set. This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		void ICollection<T>.Clear()
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Determines whether the set contains a specific value.
		/// </summary>
		public bool Contains(T item)
		{
			return m_base.Contains(item);
		}

		/// <summary>
		/// Removes a value from the set. This method throws a <see cref="NotSupportedException"/>.
		/// </summary>
		bool ICollection<T>.Remove(T item)
		{
			throw CreateReadOnlyException();
		}

		/// <summary>
		/// Gets the number of elements contained in the set.
		/// </summary>
		public int Count
		{
			get { return m_base.Count; }
		}

		/// <summary>
		/// Returns <c>true</c>; this collection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get { return true; }
		}

		private static NotSupportedException CreateReadOnlyException()
		{
			return new NotSupportedException("ReadOnlyHashSet<T> may not be modified.");
		}

		readonly HashSet<T> m_base;
	}
}
