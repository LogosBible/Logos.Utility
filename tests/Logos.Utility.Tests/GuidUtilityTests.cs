
using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class GuidUtilityTests
	{
		[Test]
		public void SwapByteOrder()
		{
			Guid guid = new Guid(0x01020304, 0x0506, 0x0708, 9, 10, 11, 12, 13, 14, 15, 16);
			byte[] bytes = guid.ToByteArray();
			CollectionAssert.AreEqual(new byte[] { 4, 3, 2, 1, 6, 5, 8, 7, 9, 10, 11, 12, 13, 14, 15, 16 }, bytes);

			GuidUtility.SwapByteOrder(bytes);
			CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, bytes);
		}

		[Test]
		public void CreateVersion3FromWidgetsCom()
		{
			// run the test case from RFC 4122 Appendix B, as updated by http://www.rfc-editor.org/errata_search.php?rfc=4122
			Guid guid = GuidUtility.Create(GuidUtility.DnsNamespace, "www.widgets.com", 3);
			Assert.AreEqual(new Guid("3d813cbb-47fb-32ba-91df-831e1593ac29"), guid);
		}

		[Test]
		public void CreateVersion3FromPythonOrg()
		{
			// run the test case from the Python implementation (http://docs.python.org/library/uuid.html#uuid-example)
			Guid guid = GuidUtility.Create(GuidUtility.DnsNamespace, "python.org", 3);
			Assert.AreEqual(new Guid("6fa459ea-ee8a-3ca4-894e-db77e160355e"), guid);
		}

		[Test]
		public void CreateVersion5FromPythonOrg()
		{
			// run the test case from the Python implementation (http://docs.python.org/library/uuid.html#uuid-example)
			Guid guid = GuidUtility.Create(GuidUtility.DnsNamespace, "python.org", 5);
			Assert.AreEqual(new Guid("886313e1-3b8a-5372-9b90-0c9aee199e5d"), guid);
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void CreateNullName()
		{
			GuidUtility.Create(GuidUtility.DnsNamespace, null);
		}

		[Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void CreateInvalidVersion()
		{
			GuidUtility.Create(GuidUtility.DnsNamespace, "www.example.com", 4);
		}
	}
}
