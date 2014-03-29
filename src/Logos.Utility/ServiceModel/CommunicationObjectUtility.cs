
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
        public static DisposibleObject CreateScope(ICommunicationObject client)
        {
            if (client == null)
                throw new ArgumentNullException("client");

            return DisposibleObject.Create(() => CloseOrAbort(client));
        }

        /// <summary>
        /// Closes the <see cref="ICommunicationObject"/> if it's not in the Faulted state, but Aborts it otherwise.
        /// Additionally, if Close throws an expected exception, that exception is swallowed and the operation is Aborted.
        /// </summary>
        /// <param name="client">The <see cref="ICommunicationObject"/> on which <see cref="ICommunicationObject.Close()"/>
        /// or <see cref="ICommunicationObject.Abort()"/> will be called.</param>
        /// <remarks>For more details, see:
        /// http://msdn2.microsoft.com/en-us/library/aa355056.aspx
        /// http://msdn2.microsoft.com/en-us/library/aa354510.aspx
        /// http://bloggingabout.net/blogs/erwyn/archive/2006/12/09/WCF-Service-Proxy-Helper.aspx
        /// http://blogs.breezetraining.com.au/mickb/2006/12/19/GreatArticleOnWCF.aspx
        /// </remarks>
        public static void CloseOrAbort(ICommunicationObject client)
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
        }
    }
}
