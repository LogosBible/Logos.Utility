
using System;

namespace Logos.Utility.Basic
{
	/// <summary>
	/// Provides helper methods for implementing objects.
	/// </summary>
	public static class ObjectUtility
	{
		/// <summary>
		/// Standard implementation of the equality operator.
        /// We use ReferenceEquals to compare because we don't rely on "==" or "Equal" to avoid the problem of being overridden.
		/// </summary>
		/// <param name="left">The left item.</param>
		/// <param name="right">The right item.</param>
		/// <returns>True if the items are equal.</returns>
		public static bool SafeIsEqual<T>(T left, T right)
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
        public static bool SafeIsNotEqual<T>(T left, T right)
			where T : class, IEquatable<T>
		{
			return !SafeIsEqual<T>(left, right);
		}

        /// <summary>
        /// Gets the hash code for the specified object.
        /// </summary>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <returns>The hash code for the specified object, or zero if the object is null.</returns>
        public static int SafeGetHashCode<T>(T obj)
        {
            return object.ReferenceEquals(obj, null) ? 0 : obj.GetHashCode();
        }
	}
}
