using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class TimeSpanUtilityTests
	{
		[TestCase(0, "0ms")]
		[TestCase(1, "1ms")]
		[TestCase(1234, "1234ms")]
		[TestCase(9999, "9999ms")]
		[TestCase(10000, "10.00s")]
		[TestCase(10004, "10.00s")]
		[TestCase(10005, "10.01s")]
		[TestCase(59990, "59.99s")]
		[TestCase(59999, "59.99s")]
		[TestCase(60000, "1m 0.0s")]
		[TestCase(61000, "1m 1.0s")]
		[TestCase(61049, "1m 1.0s")]
		[TestCase(61050, "1m 1.1s")]
		[TestCase(3599000, "59m 59.0s")]
		[TestCase(3599850, "59m 59.9s")]
		[TestCase(3599900, "59m 59.9s")]
		[TestCase(3599950, "59m 59.9s")]
		[TestCase(86398500, "23h 59m 59s")]
		[TestCase(86399000, "23h 59m 59s")]
		[TestCase(86399500, "23h 59m 59s")]
		[TestCase(86400000, "1d 0h 0m 0s")]
		[TestCase(1121685567, "12d 23h 34m 46s")]
		public void FormatForLogging(int ms, string expected)
		{
			Assert.AreEqual(expected, TimeSpanUtility.FormatForLogging(TimeSpan.FromMilliseconds(ms)));
		}

		[Test]
		public void RoundUp()
		{
			Assert.AreEqual("1ms", TimeSpanUtility.FormatForLogging(TimeSpan.FromMilliseconds(0.5)));
		}

		[Test]
		public void NegativeTimeSpan()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => TimeSpanUtility.FormatForLogging(TimeSpan.FromMinutes(-1)));
		}
	}
}
