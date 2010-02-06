
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class ObjectUtilityTests
	{
		[TestCase(null, 0)]
		[TestCase(1, 1)]
		[TestCase(2, 2)]
		public void GetHashCode(object obj, int hashCode)
		{
			Assert.That(ObjectUtility.GetHashCode(obj), Is.EqualTo(hashCode));
		}
	}
}
