
using System;
using System.Threading;

namespace Logos.Utility.Threading
{
	/// <summary>
	/// Provides a standard implementation of <see cref="IAsyncResult"/> for asynchronous work that doesn't return a result.
	/// </summary>
	/// <remarks>This class is based on code published at <a href="http://msdn.microsoft.com/en-us/magazine/cc163467.aspx">Implementing
	/// the CLR Asynchronous Programming Model</a>.</remarks>
	public class AsyncResult : IAsyncResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncResult"/> class.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <param name="state">The state.</param>
		public AsyncResult(AsyncCallback callback, object state)
		{
			m_callback = callback;
			m_objAsyncState = state;
		}

		/// <summary>
		/// Waits for the asynchronous operation to complete, rethrowing any exception that occurred.
		/// </summary>
		public void EndInvoke()
		{
			// wait for the operation if necessary
			if (!IsCompleted)
				GetWaitHandle().WaitOne();

			// close the wait handle
			DisposableUtility.Dispose(ref m_waitHandle);

			// rethrow any exception that occurred during processing
			if (m_exception != null)
				throw m_exception;
		}

		/// <summary>
		/// Indicates that the asynchronous work has completed.
		/// </summary>
		/// <param name="bCompletedSynchronously">Indicates that the asynchronous work completed synchronously if set to <c>true</c>.</param>
		public void Finish(bool bCompletedSynchronously)
		{
			DoFinish(bCompletedSynchronously);
		}

		/// <summary>
		/// Indicates that the asynchronous work has completed.
		/// </summary>
		/// <param name="exception">The exception that occurred during the asynchronous work.</param>
		/// <param name="bCompletedSynchronously">Indicates that the asynchronous work completed synchronously if set to <c>true</c>.</param>
		public void Finish(Exception exception, bool bCompletedSynchronously)
		{
			// store the exception, then finish
			if (exception == null)
				throw new ArgumentNullException("exception");
			m_exception = exception;
			DoFinish(bCompletedSynchronously);
		}

		/// <summary>
		/// Gets a value that indicates whether the asynchronous operation has completed.
		/// </summary>
		/// <returns>true if the operation is complete; otherwise, false.</returns>
		public bool IsCompleted
		{
			get { return Thread.VolatileRead(ref m_nState) != State.Running; }
		}

		/// <summary>
		/// Gets a <see cref="WaitHandle"/> that is used to wait for an asynchronous operation to complete.
		/// </summary>
		/// <returns>A <see cref="WaitHandle"/> that is used to wait for an asynchronous operation to complete.</returns>
		public WaitHandle AsyncWaitHandle
		{
			get { return GetWaitHandle(); }
		}

		/// <summary>
		/// Gets a user-defined object that qualifies or contains information about an asynchronous operation.
		/// </summary>
		/// <returns>A user-defined object that qualifies or contains information about an asynchronous operation.</returns>
		public object AsyncState
		{
			get { return m_objAsyncState; }
		}

		/// <summary>
		/// Gets a value that indicates whether the asynchronous operation completed synchronously.
		/// </summary>
		/// <returns>true if the asynchronous operation completed synchronously; otherwise, false.</returns>
		public bool CompletedSynchronously
		{
			get { return Thread.VolatileRead(ref m_nState) == State.CompletedSynchronously; }
		}

		private void DoFinish(bool bCompletedSynchronously)
		{
			// mark as completed
			int nOldState = Interlocked.Exchange(ref m_nState, bCompletedSynchronously ? State.CompletedSynchronously : State.CompletedAsynchronously);
			if (nOldState != State.Running)
				throw new InvalidOperationException();

			// set the AsyncWaitHandle (if it has already been lazily created)
			if (m_waitHandle != null)
				m_waitHandle.Set();

			// if a callback was provided, invoke it
			if (m_callback != null)
				m_callback(this);
			m_callback = null;
		}

		private WaitHandle GetWaitHandle()
		{
			// check if needs to be created
			if (m_waitHandle == null)
			{
				// create a new manual reset event with the correct state
				bool bWasCompleted = IsCompleted;
				ManualResetEvent mre = new ManualResetEvent(bWasCompleted);

				if (Interlocked.CompareExchange(ref m_waitHandle, mre, null) != null)
				{
					// we lost the race to create the event; dispose the unnecessary one
					DisposableUtility.Dispose(ref mre);
				}
				else
				{
					if (!bWasCompleted && IsCompleted)
					{
						// if the operation wasn't done when we created the event but is done now, set the event
						m_waitHandle.Set();
					}
				}
			}

			return m_waitHandle;
		}

		private static class State
		{
			public const int Running = 0;
			public const int CompletedSynchronously = 1;
			public const int CompletedAsynchronously = 2;
		}

		AsyncCallback m_callback;
		readonly object m_objAsyncState;
		Exception m_exception;
		ManualResetEvent m_waitHandle;
		int m_nState;
	}

	/// <summary>
	/// Provides a standard implementation of <see cref="IAsyncResult"/> for asynchronous work that returns a result.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AsyncResult<T> : AsyncResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncResult&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <param name="state">The state.</param>
		public AsyncResult(AsyncCallback callback, object state)
			: base(callback, state)
		{
		}

		/// <summary>
		/// Waits for the asynchronous operation to complete, rethrowing any exception that occurred.
		/// </summary>
		/// <returns>The result set by the asynchronous work.</returns>
		public new T EndInvoke()
		{
			base.EndInvoke();
			return m_result;
		}

		/// <summary>
		/// Indicates that the asynchronous work has completed
		/// </summary>
		/// <param name="result">The result generated by the asynchronous work.</param>
		/// <param name="bCompletedSynchronously">Indicates that the asynchronous work completed synchronously if set to <c>true</c>.</param>
		public void Finish(T result, bool bCompletedSynchronously)
		{
			m_result = result;
			Finish(bCompletedSynchronously);
		}

		T m_result;
	}
}
