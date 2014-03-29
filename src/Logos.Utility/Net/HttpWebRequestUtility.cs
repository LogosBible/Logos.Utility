
using System.Net;

namespace Logos.Utility.Net
{
	/// <summary>
	/// Provides utility methods for working with <see cref="HttpWebRequest"/>.
	/// </summary>
	public static class HttpWebRequestUtility
	{
		/// <summary>
		/// Gets the <see cref="HttpWebResponse"/> from an Internet resource.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>A <see cref="HttpWebResponse"/> that contains the response from the Internet resource.</returns>
		/// <remarks>This method does not throw a <see cref="WebException"/> for "error" HTTP status codes; the caller should
		/// check the <see cref="HttpWebResponse.StatusCode"/> property to determine how to handle the response.
		/// See <a href="http://code.logos.com/blog/2009/06/using_if-modified-since_in_http_requests.html">Using
		/// If-Modified-Since in HTTP Requests</a>.</remarks>
		public static HttpWebResponse GetHttpResponse(HttpWebRequest request)
		{
			try
			{
				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException ex)
			{
				// only handle protocol errors that have valid responses
				if (ex.Response == null || ex.Status != WebExceptionStatus.ProtocolError)
					throw;

				return (HttpWebResponse) ex.Response;
			}
		}
	}
}
