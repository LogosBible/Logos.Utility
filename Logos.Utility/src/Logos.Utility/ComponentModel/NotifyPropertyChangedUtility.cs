
using System.ComponentModel;

namespace Logos.Utility.ComponentModel
{
	/// <summary>
	/// Provides helper methods for working with <see cref="INotifyPropertyChanged"/>.
	/// </summary>
	public static class NotifyPropertyChangedUtility
	{
		/// <summary>
		/// Returns <c>true</c> if <paramref name="e"/> indicates that the property named by <paramref name="propertyName"/> has changed.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/>.</param>
		/// <param name="propertyName">The property name.</param>
		/// <returns><c>true</c> if the property named by <paramref name="propertyName"/> has changed; otherwise <c>false</c>.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/04/handling_the_propertychanged_event.html">Handling the PropertyChanged event</a>.</remarks>
		public static bool HasChanged(this PropertyChangedEventArgs e, string propertyName)
		{
			string eventPropertyName = e.PropertyName;
			return string.IsNullOrEmpty(eventPropertyName) || propertyName == eventPropertyName;
		}
	}
}
