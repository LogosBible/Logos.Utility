
namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for manipulating objects.
	/// </summary>
	public static class ObjectUtility
	{
		/// <summary>
		/// Gets the hash code for the specified object.
		/// </summary>
		/// <param name="obj">The object for which to get a hash code.</param>
		/// <returns>The hash code for the specified object, or zero if the object is null.</returns>
		public static int GetHashCode<T>(T obj)
		{
			return obj == null ? 0 : obj.GetHashCode();
		}
	}
}
