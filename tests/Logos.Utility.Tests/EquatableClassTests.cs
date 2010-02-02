
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class EquatableClassTests
	{
		[TestCase(null, 0)]
		[TestCase("one", 1)]
		[TestCase("million", 1000000)]
		[TestCase("minus", -2)]
		public void Constructor(string s, int i)
		{
			EquatableClass ec = new EquatableClass(s, i);
			Assert.That(ec.Text, Is.EqualTo(s));
			Assert.That(ec.Count, Is.EqualTo(i));
		}

		[Test]
		public void Equals()
		{
			Assert.That(ec_zero_0.Equals(ec_zero_0), Is.True);
			Assert.That(ec_zero_0.Equals(ec_zero_0_2), Is.True);
			Assert.That(ec_zero_0.Equals(ec_zero_1), Is.False);
			Assert.That(ec_zero_0.Equals(ec_one_0), Is.False);
			Assert.That(ec_zero_0.Equals(ec_one_1), Is.False);
		}

		[Test]
		public void EqualsNull()
		{
			Assert.That(ec_zero_0.Equals(null), Is.False);
			Assert.That(ec_zero_0.Equals(ec_null), Is.False);
		}

		[Test]
		public void EqualsObject()
		{
			Assert.That(ec_zero_0.Equals((object) ec_zero_0), Is.True);
			Assert.That(ec_zero_0.Equals((object) ec_zero_0_2), Is.True);
			Assert.That(ec_zero_0.Equals((object) ec_zero_1), Is.False);
			Assert.That(ec_zero_0.Equals((object) ec_one_0), Is.False);
			Assert.That(ec_zero_0.Equals((object) ec_one_1), Is.False);
		}

		[Test]
		public void OperatorEquality()
		{
			Assert.That(ec_zero_0 == ec_zero_0_2, Is.True);
			Assert.That(ec_zero_0 == ec_zero_1, Is.False);
			Assert.That(ec_zero_0 == ec_one_0, Is.False);
			Assert.That(ec_zero_0 == ec_one_1, Is.False);
			Assert.That(ec_zero_0 == ec_null, Is.False);
			Assert.That(ec_null == ec_zero_0, Is.False);
		}

		[Test]
		public void OperatorInequality()
		{
			Assert.That(ec_zero_0 != ec_zero_0_2, Is.False);
			Assert.That(ec_zero_0 != ec_zero_1, Is.True);
			Assert.That(ec_zero_0 != ec_one_0, Is.True);
			Assert.That(ec_zero_0 != ec_one_1, Is.True);
			Assert.That(ec_zero_0 != ec_null, Is.True);
			Assert.That(ec_null != ec_zero_0, Is.True);
		}

		[Test]
		public void HashCode()
		{
			Assert.That(ec_zero_0.GetHashCode(), Is.EqualTo(ec_zero_0_2.GetHashCode()));
			Assert.That(ec_zero_0.GetHashCode(), Is.Not.EqualTo(ec_zero_1.GetHashCode()));
			Assert.That(ec_zero_0.GetHashCode(), Is.Not.EqualTo(ec_one_0.GetHashCode()));
			Assert.That(ec_zero_0.GetHashCode(), Is.Not.EqualTo(ec_one_1.GetHashCode()));
		}

		EquatableClass ec_zero_0 = new EquatableClass("zero", 0);
		EquatableClass ec_zero_0_2 = new EquatableClass("zero", 0);
		EquatableClass ec_zero_1 = new EquatableClass("zero", 1);
		EquatableClass ec_one_0 = new EquatableClass("one", 0);
		EquatableClass ec_one_1 = new EquatableClass("one", 1);
		EquatableClass ec_null = null;
	}
}
