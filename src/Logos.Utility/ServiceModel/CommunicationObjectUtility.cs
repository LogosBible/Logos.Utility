
using System;
using System.ServiceModel;

namespace Logos.Utility.ServiceModel
{
	/// <summary>
	/// Provides methods for working with <see cref="ICommunicationObject"/>.
	/// </summary>
	public static class CommunicationObjectUtility
	{
		/// <summary>
		/// Creates a <see cref="Scope"/> that safely closes the specified <see cref="ICommunicationObject"/>.
		/// </summary>
		/// <remarks>The Scope, when disposed, will Close or Abort the session, as appropriate. </remarks>
		/// <param name="client">The client.</param>
		/// <returns></returns>
		public static Scope CreateScope(ICommunicationObject client)
		{
			if (client == null)
				throw new ArgumentNullException("client");

			// We need to Close the CommunicationObject if it's not in the Faulted state, but Abort it otherwise. Additionally.
			// if Close throws an expected exception, we swallow that exception and Abort the operation.
			// For more details, see:
			//   http://msdn2.microsoft.com/en-us/library/aa355056.aspx
			//   http://msdn2.microsoft.com/en-us/library/aa354510.aspx
			//   http://bloggingabout.net/blogs/erwyn/archive/2006/12/09/WCF-Service-Proxy-Helper.aspx
			//   http://blogs.breezetraining.com.au/mickb/2006/12/19/GreatArticleOnWCF.aspx
			return Scope.Create(delegate
			{
				if (client.State != CommunicationState.Faulted)
				{
					// client is in non-faulted state; we can attempt to Close it
					try
					{
						client.Close();
					}
					catch (CommunicationException)
					{
						client.Abort();
					}
					catch (TimeoutException)
					{
						client.Abort();
					}
					catch (Exception)
					{
						// unexpected exception--still Abort the client, but re-throw it
						client.Abort();
						throw;
					}
				}
				else
				{
					// client has already failed; have to abort
					client.Abort();
				}
			});
		}
	}
}
