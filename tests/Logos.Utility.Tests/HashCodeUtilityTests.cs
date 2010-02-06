
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class HashCodeUtilityTests
	{
		[TestCase(0)]
		[TestCase(1)]
		[TestCase(int.MinValue)]
		[TestCase(int.MaxValue)]
		public void HashOne(int value)
		{
			Assert.That(HashCodeUtility.CombineHashCodes(value), Is.EqualTo(HashCodeUtility.CombineHashCodes(new[] { value })));
		}

		[TestCase(0, 0)]
		[TestCase(int.MinValue, int.MaxValue)]
		[TestCase(0, -1)]
		public void HashTwo(int value1, int value2)
		{
			Assert.That(HashCodeUtility.CombineHashCodes(value1, value2), Is.EqualTo(HashCodeUtility.CombineHashCodes(new[] { value1, value2 })));
		}

		[TestCase(0, 0, 0)]
		[TestCase(int.MinValue, 0, int.MaxValue)]
		[TestCase(1, 2, 3)]
		public void HashThree(int value1, int value2, int value3)
		{
			Assert.That(HashCodeUtility.CombineHashCodes(value1, value2, value3), Is.EqualTo(HashCodeUtility.CombineHashCodes(new[] { value1, value2, value3 })));
		}

		[TestCase(0, 0, 0, 0)]
		[TestCase(int.MinValue, 0, int.MaxValue, -1)]
		[TestCase(1, 2, 3, 4)]
		public void HashFour(int value1, int value2, int value3, int value4)
		{
			Assert.That(HashCodeUtility.CombineHashCodes(value1, value2, value3, value4), Is.EqualTo(HashCodeUtility.CombineHashCodes(new[] { value1, value2, value3, value4 })));
		}
	}
}
