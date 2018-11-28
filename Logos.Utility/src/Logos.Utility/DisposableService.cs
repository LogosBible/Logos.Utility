
using System;
using System.Diagnostics;

namespace Logos.Utility
{
	/// <summary>
	/// A thread-safe disposable service that supports property notifications.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/03/threadsafe_disposable_objects.html">Thread-safe disposable objects</a>.</remarks>
	public abstract class DisposableService : IDisposable
	{
		/// <summary>
		/// Raised by the Dispose method immediately before disposing.
		/// </summary>
		/// <remarks>This event is raised only once.</remarks>
		public event EventHandler Disposing
		{
			add
			{
				lock (m_disposingLock)
				{
					VerifyNotDisposed();
					m_disposing += value;
				}
			}
			remove
			{
				lock (m_disposingLock) 
					m_disposing -= value;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			// only dispose once
			EventHandler disposing;
			lock (m_disposingLock)
			{
				if (m_isDisposing)
					return;
				m_isDisposing = true;
				disposing = m_disposing;
				m_disposing = null;
			}

			// this should never be null; it at least calls OnDisposing
			disposing(this, EventArgs.Empty);

			// mark as disposed
			m_isDisposed = true;

			// dispose
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DisposableService"/> class.
		/// </summary>
		protected DisposableService()
		{
			m_disposing = delegate { OnDisposing(); };
			m_disposingLock = new object();
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		/// <remarks>This method is guaranteed to be called only once.</remarks>
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>
		/// Verifies that the instance is not disposed.
		/// </summary>
		/// <exception cref="ObjectDisposedException">The instance is disposed.</exception>
		protected void VerifyNotDisposed()
		{
			if (m_isDisposed)
				throw new ObjectDisposedException(GetType().Name);
		}

		/// <summary>
		/// Called during the call to Dispose, immediately before Dispose(bool) is called.
		/// </summary>
		/// <remarks>The object is not yet marked as disposed, so VerifyNotDisposed will not throw an exception.</remarks>
		protected virtual void OnDisposing()
		{
		}

#if DEBUG
		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="DisposableService"/> is reclaimed by garbage collection.
		/// </summary>
		/// <remarks>The finalizer is suppressed by Dispose, so this method should only be called if the client
		/// fails to dispose the instance.</remarks>
		~DisposableService()
		{
			Debug.Fail("Not disposed: " + GetType().Name);
		}
#endif

		readonly object m_disposingLock;
		EventHandler m_disposing;
		bool m_isDisposing;
		volatile bool m_isDisposed;
	}
}
