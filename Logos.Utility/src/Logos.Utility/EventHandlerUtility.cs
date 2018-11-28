using System;

namespace Logos.Utility
{
	/// <summary>
	/// Utility methods for event handlers.
	/// </summary>
	public static class EventHandlerUtility
	{
		/// <summary>
		/// Raises the specified event.
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <remarks>This method does nothing if the event handler is null.</remarks>
		public static void Raise(this EventHandler eventHandler, object sender)
		{
			if (eventHandler != null)
				eventHandler(sender, EventArgs.Empty);
		}

		/// <summary>
		/// Raises the specified event.
		/// </summary>
		/// <param name="eventHandler">The event handler.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		/// <remarks>This method does nothing if the event handler is null.</remarks>
		public static void Raise<T>(this EventHandler<T> eventHandler, object sender, T e) where T : EventArgs
		{
			if (eventHandler != null)
				eventHandler(sender, e);
		}
	}
}
