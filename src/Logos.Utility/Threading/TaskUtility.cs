
using System;
using System.Threading.Tasks;

namespace Logos.Utility.Threading
{
	/// <summary>
	/// Provides helper methods for working with <see cref="Task"/>.
	/// </summary>
	public static class TaskUtility
	{
		/// <summary>
		/// Creates an <see cref="IAsyncResult"/> object that's suitable to be returned from an APM-style method.
		/// </summary>
		/// <typeparam name="T">The type of value being returned by the EndXxx APM method.</typeparam>
		/// <param name="task">The task that produces the value to be returned.</param>
		/// <param name="callback">The <see cref="AsyncCallback"/> supplied to the BeginXxx APM method.</param>
		/// <param name="state">The state object supplied to the BeginXxx APM method.</param>
		/// <returns>An <see cref="IAsyncResult"/> object that can be returned from an APM-style BeginXxx method.</returns>
		public static IAsyncResult CreateAsyncResult<T>(this Task<T> task, AsyncCallback callback, object state)
		{
			// create result object that can hold the asynchronously-computed value
			TaskCompletionSource<T> result = new TaskCompletionSource<T>(state);

			// set the result (or failure) when the value is known
			task.ContinueWith(t =>
				{
                    result.SetFromTask(t);
                    if (callback != null)
                        callback(result.Task);
                });

			// the result's task functions as the IAsyncResult APM return value
			return result.Task;
		}
	}
}
