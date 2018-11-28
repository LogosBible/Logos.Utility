
using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class ExpressionUtilityTests
	{
		[Test]
		public void GetPropertyName()
		{
			Assert.AreEqual("TestReadOnly", ExpressionUtility.GetPropertyName((Tester t) => t.TestReadOnly));
			Assert.AreEqual("TestReadWrite", ExpressionUtility.GetPropertyName((Tester t) => t.TestReadWrite));

			Tester tester = new Tester();
			Assert.AreEqual("TestReadOnly", ExpressionUtility.GetPropertyName(() => tester.TestReadOnly));
			Assert.AreEqual("TestReadWrite", ExpressionUtility.GetPropertyName(() => tester.TestReadWrite));

			Assert.AreEqual("TestStaticReadOnly", ExpressionUtility.GetPropertyName(() => Tester.TestStaticReadOnly));
			Assert.AreEqual("TestStaticReadWrite", ExpressionUtility.GetPropertyName(() => Tester.TestStaticReadWrite));
		}

		private class Tester
		{
			public int TestReadOnly
			{
				get { throw new InvalidOperationException(); }
			}

			public int TestReadWrite
			{
				get { throw new InvalidOperationException(); }
				set { throw new InvalidOperationException(); }
			}

			public static int TestStaticReadOnly
			{
				get { throw new InvalidOperationException(); }
			}

			public static int TestStaticReadWrite
			{
				get { throw new InvalidOperationException(); }
				set { throw new InvalidOperationException(); }
			}
		}

	}
}
