
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class DelegateUtilityTests
	{
		[SetUp]
		public void SetUp()
		{
			m_n = 1;
		}

		[Test]
		public void SimpleCastTest()
		{
			Calc calc = Add;
			Calculate calculate = DelegateUtility.Cast<Calculate>(calc);
			Assert.That(calculate(2), Is.EqualTo(3));
		}

		[Test]
		public void MulticastCastTest()
		{
			Calc calc = Add;
			calc += Subtract;
			Calculate calculate = DelegateUtility.Cast<Calculate>(calc);
			Assert.That(calculate(2), Is.EqualTo(1));
		}

		[Test]
		public void NullCastTest()
		{
			Calc calc = null;
			Calculate calculate = DelegateUtility.Cast<Calculate>(calc);
			Assert.That(calculate, Is.Null);
		}

		private delegate int Calc(int x);

		private delegate int Calculate(int x);

		private int Add(int x)
		{
			return m_n += x;
		}

		private int Subtract(int x)
		{
			return m_n -= x;
		}

		private static int Add(int x, int y)
		{
			return x + y;
		}

		int m_n;
	}
}
