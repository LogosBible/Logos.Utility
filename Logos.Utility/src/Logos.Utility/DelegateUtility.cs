
using System;

namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for manipulating delegates.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/07/casting_delegates.html">Casting delegates</a>.</remarks>
	public static class DelegateUtility
	{
		/// <summary>
		/// Casts the specified delegate to the specified type.
		/// </summary>
		/// <param name="source">The delegate to cast.</param>
		/// <returns>A delegate of the specified type which, when invoked, executes
		/// the specified delegate.</returns>
		/// <remarks>This method uses the <b>CreateDelegate</b> method of
		/// <see cref="Delegate" /> to create a new delegate from an existing delegate.
		/// Use this method to cast a delegate from one type to another, where the delegate types
		/// have exactly the same parameter types and return type.</remarks>
		public static T Cast<T>(Delegate source) where T : class
		{
			return Cast(source, typeof(T)) as T;
		}

		/// <summary>
		/// Casts the specified delegate to the specified type.
		/// </summary>
		/// <param name="source">The delegate to cast.</param>
		/// <param name="type">The type of delegate to which to cast.</param>
		/// <returns>A delegate of the specified type which, when invoked, executes
		/// the specified delegate.</returns>
		/// <remarks>This method uses the <b>CreateDelegate</b> method of
		/// <see cref="Delegate" /> to create a new delegate from an existing delegate.
		/// Use this method to cast a delegate from one type to another, where the delegate types
		/// have exactly the same parameter types and return type.</remarks>
		public static Delegate Cast(Delegate source, Type type)
		{
			if (source == null)
				return null;

			Delegate[] delegates = source.GetInvocationList();
			if (delegates.Length == 1)
				return Delegate.CreateDelegate(type,
					delegates[0].Target, delegates[0].Method);

			Delegate[] delegatesDest = new Delegate[delegates.Length];
			for (int nDelegate = 0; nDelegate < delegates.Length; nDelegate++)
				delegatesDest[nDelegate] = Delegate.CreateDelegate(type,
					delegates[nDelegate].Target, delegates[nDelegate].Method);
			return Delegate.Combine(delegatesDest);
		}
	}
}
