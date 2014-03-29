
using System;

namespace Logos.Utility
{
	/// <summary>
	/// This class has two usages:
	/// 1) Given an Action, it will be executed and released.
	/// 2) Given an instance (it must implement IDispose), it can be used and automatically released when calling "Dispose".
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/08/leverage_using_blocks_with_scope.html">Leverage using blocks with Scope</a>.</remarks>
	public sealed class DisposibleObject : IDisposable
	{
		/// <summary>
		/// Creates a <see cref="Scope" /> for the specified delegate.
		/// </summary>
		/// <param name="onDispose">The delegate.</param>
		/// <returns>An instance of <see cref="Scope" /> that calls the delegate when disposed.</returns>
		/// <remarks>If <paramref name="onDispose"/> is null, the instance does nothing when disposed.</remarks>
		public static DisposibleObject Create(Action onDispose)
		{
			return new DisposibleObject(onDispose);
		}

		/// <summary>
		/// Creates a <see cref="Scope" /> that disposes the specified object.
		/// </summary>
		/// <param name="disposable">The object to dispose.</param>
		/// <returns>An instance of <see cref="Scope" /> that disposes the object when disposed.</returns>
		/// <remarks>If disposable is null, the instance does nothing when disposed.</remarks>
		public static DisposibleObject Create<T>(T disposable) where T : IDisposable
		{
			return disposable == null ? Empty : new DisposibleObject(disposable.Dispose);
		}

		/// <summary>
		/// An empty scope, which does nothing when disposed.
		/// </summary>
		public static readonly DisposibleObject Empty = new DisposibleObject(null);

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
		public DisposibleObject Transfer()
		{
			DisposibleObject scope = new DisposibleObject(m_fnDispose);
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
		/// <summary>
		/// A private constructor that will keep the implemented Dispose function through Action delegate
		/// </summary>
		private DisposibleObject(Action onDispose)
		{
			m_fnDispose = onDispose;
		}
		/// <summary>
		/// An action delegate variable
		/// </summary>
		Action m_fnDispose;
	}
}
