using System;
using Logos.Utility.Basic;

namespace Logos.Utility
{
	/// <summary>
	/// Helper methods for working with <see cref="TimeSpan"/>.
	/// </summary>
	public static class TimeSpanUtility
	{
		/// <summary>
		/// Formats <paramref name="ts"/> as a concise string suitable for logging; the precision of the returned
		/// string (which is not culture-sensitive) depends on the duration of the <see cref="TimeSpan"/>.
		/// </summary>
		/// <param name="ts">The TimeSpan to render.</param>
		/// <returns>A culture-invariant string with a concise rendering of the TimeSpan.</returns>
		public static string FormatForLogging(TimeSpan ts)
		{
			if (ts.Ticks < 0)
				throw new ArgumentOutOfRangeException("ts", "The TimeSpan must have a positive duration.");

			// log the timespan in the most appropriate format for the actual duration
			double seconds = ts.Seconds + ts.Milliseconds / 1000.0;
			if (ts.TotalSeconds < 10)
				return StringUtility.FormatInvariant("{0}ms", ((int)(ts.TotalMilliseconds + 0.5)));
			else if (ts.TotalMinutes < 1)
				return StringUtility.FormatInvariant("{0:0.00}s", (Math.Min(seconds, 59.99)));
			else if (ts.TotalHours < 1)
				return StringUtility.FormatInvariant("{0}m {1:0.0}s", ts.Minutes, Math.Min(seconds, 59.9));
			else if (ts.TotalDays < 1)
				return StringUtility.FormatInvariant("{0}h {1}m {2:0}s", (int)ts.TotalHours, ts.Minutes, Math.Min(seconds, 59));
			else
				return StringUtility.FormatInvariant("{0}d {1}h {2}m {3:0}s",(int)ts.TotalDays, ts.Hours, ts.Minutes, Math.Min(seconds, 59));
		}
	}
}
