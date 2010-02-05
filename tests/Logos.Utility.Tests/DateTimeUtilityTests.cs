
using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class DateTimeUtilityTests
	{
		[TestCase(2000, 1, 2, 3, 4, 5, "2000-01-02T03:04:05Z")]
		[TestCase(2010, 11, 23, 12, 34, 56, "2010-11-23T12:34:56Z")]
		[TestCase(1900, 1, 1, 0, 0, 0, "1900-01-01T00:00:00Z")]
		[TestCase(2010, 12, 31, 23, 59, 59, "2010-12-31T23:59:59Z")]
		public void RoundTrip(int year, int month, int day, int hour, int minute, int second, string iso)
		{
			DateTime dt = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
			Assert.That(dt.ToIso8601(), Is.EqualTo(iso));

			DateTime dt2 = DateTimeUtility.ParseIso8601(iso);
			Assert.That(dt2, Is.EqualTo(dt));
		}
	}
}
