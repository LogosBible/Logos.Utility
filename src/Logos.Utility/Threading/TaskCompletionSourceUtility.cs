
using System;
using System.Threading.Tasks;

namespace Logos.Utility.Threading
{
	/// <summary>
	/// Provides helper methods for working with <see cref="TaskCompletionSource{T}"/>.
	/// </summary>
	public static class TaskCompletionSourceUtility
	{
		/// <summary>
		/// Sets the state of the specified <see cref="TaskCompletionSource{TResult}.Task"/> to that of the specified <see cref="Task{TResult}"/>.
		/// </summary>
		/// <param name="source">A <see cref="TaskCompletionSource{TResult}"/> that will have its Task's status set.</param>
		/// <param name="task">The <see cref="Task{TResult}"/> that supplies the result or exception for the <see cref="TaskCompletionSource{TResult}"/>.</param>
		public static void SetFromTask<TResult>(this TaskCompletionSource<TResult> source, Task<TResult> task)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (task == null)
				throw new ArgumentNullException("task");

			switch (task.Status)
			{
			case TaskStatus.RanToCompletion:
				source.SetResult(task.Result);
				break;

			case TaskStatus.Faulted:
				source.SetException(task.Exception.InnerExceptions);
				break;

			case TaskStatus.Canceled:
				source.SetCanceled();
				break;

			default:
				throw new InvalidOperationException("The task was not completed.");
			}
		}
	}
}
