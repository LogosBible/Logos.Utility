using Logos.Utility.Basic;
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

		[TestCase(false, 1800329511)]
		[TestCase(true, -1266253386)]
		public void GetPersistentBoolHashCode(bool value, int expected)
		{
			Assert.That(HashCodeUtility.GetPersistentHashCode(value), Is.EqualTo(expected));
		}

		[TestCase(0, 1800329511)]
		[TestCase(1, -1266253386)]
		[TestCase(2, -496519092)]
		[TestCase(3, 1332612764)]
		[TestCase(4, 946348061)]
		[TestCase(-1, -26951294)]
		[TestCase(-2, 2115881666)]
		public void GetPersistentIntHashCode(int value, int expected)
		{
			Assert.That(HashCodeUtility.GetPersistentHashCode((sbyte) value), Is.EqualTo(expected));
			Assert.That(HashCodeUtility.GetPersistentHashCode((short) value), Is.EqualTo(expected));
			if (value >= 0)
				Assert.That(HashCodeUtility.GetPersistentHashCode((char) value), Is.EqualTo(expected));
			Assert.That(HashCodeUtility.GetPersistentHashCode(value), Is.EqualTo(expected));
		}

		[TestCase(0, 720020139)]
		[TestCase(1, 357654460)]
		[TestCase(2, 715307540)]
		[TestCase(3, 1072960876)]
		[TestCase(4, 1430614333)]
		[TestCase(-1, 532412650)]
		[TestCase(-2, 340268856)]
		public void GetPersistentLongHashCode(long value, int expected)
		{
			Assert.That(HashCodeUtility.GetPersistentHashCode(value), Is.EqualTo(expected));
		}

		[TestCase("", 0)]
		[TestCase(null, 0)]
		[TestCase("a", -889528276)]
		[TestCase("b", -685344420)]
		[TestCase("c", -414938692)]
		[TestCase("abc", 2058321224)]
		[TestCase("abcabc", 120553164)]
		[TestCase("abcabca", 451022788)]
		[TestCase("\" \"", 1671599841)]
		[TestCase("#@!", 1671599841)]
		public void GetPersistentHash(string value, int expected)
		{
			Assert.That(HashCodeUtility.GetPersistentHashCode(value), Is.EqualTo(expected));
		}
	}
}
