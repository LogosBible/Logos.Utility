
using System;
using System.Globalization;

namespace Logos.Utility.Basic
{
	/// <summary>
	/// Provides methods for manipulating dates.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2009/04/datetime_and_iso8601.html">DateTime and ISO8601</a>.</remarks>
	public static class DateTimeUtility
	{
		/// <summary>
		/// Converts the specified ISO 8601 representation of a date and time
		/// to its DateTime equivalent.
		/// </summary>
		/// <param name="value">The ISO 8601 string representation to parse.</param>
		/// <returns>The DateTime equivalent.</returns>
		public static DateTime ParseIso8601(string value)
		{
			return DateTime.ParseExact(value, Iso8601Format, CultureInfo.InvariantCulture,
				DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
		}

		/// <summary>
		/// Formats the date in the standard ISO 8601 format.
		/// </summary>
		/// <param name="value">The date to format.</param>
		/// <returns>The formatted date.</returns>
		public static string ToIso8601(DateTime value)
		{
			return value.ToUniversalTime().ToString(Iso8601Format, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// The ISO 8601 format string.
		/// </summary>
		public const string Iso8601Format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'";
	}
}
