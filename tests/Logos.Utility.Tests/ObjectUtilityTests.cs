using NUnit.Framework;
using Logos.Utility.Basic;
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
			Assert.That(ObjectUtility.SafeGetHashCode<object>(obj), Is.EqualTo(hashCode));
		}
	}
}
