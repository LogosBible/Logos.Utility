
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class EquatableStructTests
	{
		[TestCase(null, 0)]
		[TestCase("one", 1)]
		[TestCase("million", 1000000)]
		[TestCase("minus", -2)]
		public void Constructor(string s, int i)
		{
			EquatableStruct ec = new EquatableStruct(s, i);
			Assert.That(ec.Text, Is.EqualTo(s));
			Assert.That(ec.Count, Is.EqualTo(i));
		}

		[Test]
		public void Equals()
		{
			Assert.That(es_zero_0.Equals(es_zero_0), Is.True);
			Assert.That(es_zero_0.Equals(es_zero_0_2), Is.True);
			Assert.That(es_zero_0.Equals(es_zero_1), Is.False);
			Assert.That(es_zero_0.Equals(es_one_0), Is.False);
			Assert.That(es_zero_0.Equals(es_one_1), Is.False);
		}

		[Test]
		public void EqualsNull()
		{
			Assert.That(es_zero_0.Equals(null), Is.False);
		}

		[Test]
		public void EqualsObject()
		{
			Assert.That(es_zero_0.Equals((object) es_zero_0), Is.True);
			Assert.That(es_zero_0.Equals((object) es_zero_0_2), Is.True);
			Assert.That(es_zero_0.Equals((object) es_zero_1), Is.False);
			Assert.That(es_zero_0.Equals((object) es_one_0), Is.False);
			Assert.That(es_zero_0.Equals((object) es_one_1), Is.False);
		}

		[Test]
		public void OperatorEquality()
		{
			Assert.That(es_zero_0 == es_zero_0_2, Is.True);
			Assert.That(es_zero_0 == es_zero_1, Is.False);
			Assert.That(es_zero_0 == es_one_0, Is.False);
			Assert.That(es_zero_0 == es_one_1, Is.False);
		}

		[Test]
		public void OperatorInequality()
		{
			Assert.That(es_zero_0 != es_zero_0_2, Is.False);
			Assert.That(es_zero_0 != es_zero_1, Is.True);
			Assert.That(es_zero_0 != es_one_0, Is.True);
			Assert.That(es_zero_0 != es_one_1, Is.True);
		}

		[Test]
		public void HashCode()
		{
			Assert.That(es_zero_0.GetHashCode(), Is.EqualTo(es_zero_0_2.GetHashCode()));
			Assert.That(es_zero_0.GetHashCode(), Is.Not.EqualTo(es_zero_1.GetHashCode()));
			Assert.That(es_zero_0.GetHashCode(), Is.Not.EqualTo(es_one_0.GetHashCode()));
			Assert.That(es_zero_0.GetHashCode(), Is.Not.EqualTo(es_one_1.GetHashCode()));
		}

		EquatableStruct es_zero_0 = new EquatableStruct("zero", 0);
		EquatableStruct es_zero_0_2 = new EquatableStruct("zero", 0);
		EquatableStruct es_zero_1 = new EquatableStruct("zero", 1);
		EquatableStruct es_one_0 = new EquatableStruct("one", 0);
		EquatableStruct es_one_1 = new EquatableStruct("one", 1);
	}
}
