
using System;

namespace Logos.Utility.Tests
{
	/// <summary>
	/// Gives a reference implementation of an equatable class.
	/// </summary>
	public sealed class EquatableClass : IEquatable<EquatableClass>
	{
		/// <summary>
		/// Constructs a new instance of <see cref="EquatableClass"/>.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="count">The count.</param>
		public EquatableClass(string text, int count)
		{
			m_text = text;
			m_count = count;
		}

		/// <summary>
		/// Returns the text.
		/// </summary>
		public string Text
		{
			get { return m_text; }
		}

		/// <summary>
		/// Returns the count.
		/// </summary>
		public int Count
		{
			get { return m_count; }
		}

		/// <summary>
		/// Returns <c>true</c> if this object is equal to the specified object.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns><c>true</c> if this object is equal to the specified object; otherwise <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return Equals(obj as EquatableClass);
		}

		/// <summary>
		/// Returns <c>true</c> if this object is equal to the specified <see cref="EquatableClass"/>.
		/// </summary>
		/// <param name="other">The <see cref="EquatableClass"/> to compare to.</param>
		/// <returns><c>true</c> if this object is equal to the specified <see cref="EquatableClass"/>; otherwise <c>false</c>.</returns>
		public bool Equals(EquatableClass other)
		{
			// this is the core implementation of Equals; other methods delegate to this
			return other != null && m_text == other.m_text && m_count == other.m_count;
		}

		/// <summary>
		/// Returns a hash code for the current object.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			// combine the hash codes of the various components of this class
			return HashCodeUtility.CombineHashCodes(ObjectUtility.GetHashCode(m_text), m_count);
		}

		/// <summary>
		/// Determines whether two specified <see cref="EquatableClass"/> objects have the same value.
		/// </summary>
		/// <param name="left">An <see cref="EquatableClass"/> or a <c>null</c> reference.</param>
		/// <param name="right">An <see cref="EquatableClass"/> or a <c>null</c> reference.</param>
		/// <returns><c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		public static bool operator ==(EquatableClass left, EquatableClass right)
		{
			return ObjectImpl.OperatorEquality(left, right);
		}

		/// <summary>
		/// Determines whether two specified <see cref="EquatableClass"/> objects have different value.
		/// </summary>
		/// <param name="left">An <see cref="EquatableClass"/> or a <c>null</c> reference.</param>
		/// <param name="right">An <see cref="EquatableClass"/> or a <c>null</c> reference.</param>
		/// <returns><c>true</c> if the value of <paramref name="left"/> is different from the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
		public static bool operator !=(EquatableClass left, EquatableClass right)
		{
			return ObjectImpl.OperatorInequality(left, right);
		}

		readonly string m_text;
		readonly int m_count;
	}
}
