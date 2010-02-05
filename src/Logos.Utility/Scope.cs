
using System;

namespace Logos.Utility
{
	/// <summary>
	/// Executes the specified delegate when disposed.
	/// </summary>
    /// <remarks>See <a href="http://code.logos.com/blog/2008/08/leverage_using_blocks_with_scope.html">Leverage using blocks with Scope</a>.</remarks>
	public sealed class Scope : IDisposable
	{
		/// <summary>
		/// Creates a <see cref="Scope" /> for the specified delegate.
		/// </summary>
		/// <param name="onDispose">The delegate.</param>
		/// <returns>An instance of <see cref="Scope" /> that calls the delegate when disposed.</returns>
		/// <remarks>If <paramref name="onDispose"/> is null, the instance does nothing when disposed.</remarks>
		public static Scope Create(Action onDispose)
		{
			return new Scope(onDispose);
		}

		/// <summary>
		/// Creates a <see cref="Scope" /> that disposes the specified object.
		/// </summary>
		/// <param name="disposable">The object to dispose.</param>
		/// <returns>An instance of <see cref="Scope" /> that disposes the object when disposed.</returns>
		/// <remarks>If disposable is null, the instance does nothing when disposed.</remarks>
		public static Scope Create<T>(T disposable) where T : IDisposable
		{
			return disposable == null ? Empty : new Scope(disposable.Dispose);
		}

		/// <summary>
		/// An empty scope, which does nothing when disposed.
		/// </summary>
		public static readonly Scope Empty = new Scope(null);

		/// <summary>
		/// Cancel the call to the encapsulated delegate.
		/// </summary>
		/// <remarks>After calling this method, disposing this instance does nothing.</remarks>
		public void Cancel()
		{
			m_fnDispose = null;
		}

		/// <summary>
		/// Returns a new Scope that will call the encapsulated delegate.
		/// </summary>
		/// <returns>A new Scope that will call the encapsulated delegate.</returns>
		/// <remarks>After calling this method, disposing this instance does nothing.</remarks>
		public Scope Transfer()
		{
			Scope scope = new Scope(m_fnDispose);
			m_fnDispose = null;
			return scope;
		}

		/// <summary>
		/// Calls the encapsulated delegate.
		/// </summary>
		public void Dispose()
		{
			if (m_fnDispose != null)
			{
				m_fnDispose();
				m_fnDispose = null;
			}
		}

		private Scope(Action onDispose)
		{
			m_fnDispose = onDispose;
		}

		Action m_fnDispose;
	}
}
