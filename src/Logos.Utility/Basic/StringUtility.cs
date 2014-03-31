using System.Globalization;

namespace Logos.Utility.Basic
{

	/// <summary>
	/// Provides methods for working with <see cref="string"/>.
	/// </summary>
	public static class StringUtility
	{
		/// <summary>
		/// Formats the string using the invariant culture.
		/// </summary>
		/// <param name="format">The format string.</param>
		/// <param name="args">The format arguments.</param>
		/// <returns>The formatted string.</returns>
		public static string FormatInvariant(string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}
	}
}
