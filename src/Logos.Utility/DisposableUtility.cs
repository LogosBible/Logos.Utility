
using System;

namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for manipulating disposable objects.
	/// </summary>
	public static class DisposableUtility
	{
		/// <summary>
		/// Disposes and nulls the specified object.
		/// </summary>
		/// <param name="obj">The object to dispose and null.</param>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/02/disposed_objects.html">Disposed objects</a>.</remarks>
		public static void Dispose<T>(ref T obj) where T : class, IDisposable
		{
			if (obj != null)
			{
				obj.Dispose();
				obj = null;
			}
		}

		/// <summary>
		/// Disposes the specified object after executing the specified delegate.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TOutput">The type of the output.</typeparam>
		/// <param name="d">The object to dispose.</param>
		/// <param name="fn">The delegate to execute before disposing the object.</param>
		/// <returns>The value returned by the delegate.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/02/another_extension_method_dispo.html">Another extension method: DisposeAfter</a>.</remarks>
		public static TOutput DisposeAfter<TInput, TOutput>(this TInput d, Func<TInput, TOutput> fn) where TInput : IDisposable
		{
			using (d)
				return fn(d);
		}

		/// <summary>
		/// Disposes the specified object after executing the specified delegate.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TOutput">The type of the output.</typeparam>
		/// <param name="d">The object to dispose.</param>
		/// <param name="fn">The delegate to execute before disposing the object.</param>
		/// <returns>The value returned by the delegate.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/02/another_extension_method_dispo.html">Another extension method: DisposeAfter</a>.</remarks>
		public static TOutput DisposeAfter<TInput, TOutput>(this TInput d, Func<TOutput> fn) where TInput : IDisposable
		{
			using (d)
				return fn();
		}
	}
}
