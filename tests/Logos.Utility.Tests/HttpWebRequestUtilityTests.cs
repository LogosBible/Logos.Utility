
using System;
using System.Net;
using Logos.Utility.Net;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class HttpWebRequestUtilityTests
	{
		[Test, Ignore("Requires internet connection")]
		public void Get404Response()
		{
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri("http://code.logos.com/non-existent"));
			using (HttpWebResponse response = request.GetHttpResponse())
			{
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
			}
		}

		[Test, Ignore("Requires internet connection")]
		public void Get304Response()
		{
			HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri("http://code.logos.com/blog/blogCodeBackground.gif"));
			request.IfModifiedSince = new DateTime(2009, 1, 1);
			using (HttpWebResponse response = request.GetHttpResponse())
			{
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotModified));
			}
		}
	}
}
