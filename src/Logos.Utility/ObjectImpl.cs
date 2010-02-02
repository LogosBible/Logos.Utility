
using System;

namespace Logos.Utility
{
	/// <summary>
	/// Provides helper methods for implementing objects.
	/// </summary>
	public static class ObjectImpl
	{
		/// <summary>
		/// Standard implementation of the equality operator.
		/// </summary>
		/// <param name="left">The left item.</param>
		/// <param name="right">The right item.</param>
		/// <returns>True if the items are equal.</returns>
		public static bool OperatorEquality<T>(T left, T right)
			where T : class, IEquatable<T>
		{
			if (object.ReferenceEquals(left, right))
				return true;
			else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
				return false;
			else
				return left.Equals(right);
		}

		/// <summary>
		/// Standard implementation of the inequality operator.
		/// </summary>
		/// <param name="left">The left item.</param>
		/// <param name="right">The right item.</param>
		/// <returns>True if the items are not equal.</returns>
		public static bool OperatorInequality<T>(T left, T right)
			where T : class, IEquatable<T>
		{
			if (object.ReferenceEquals(left, right))
				return false;
			else if (object.ReferenceEquals(left, null) || object.ReferenceEquals(right, null))
				return true;
			else
				return !left.Equals(right);
		}
	}
}
