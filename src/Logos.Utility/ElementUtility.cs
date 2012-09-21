using System;
using System.Windows;

namespace Logos.Utility
{
	/// <summary>
	/// Implementation of the ElementUtility.AddHandler and ElementUtility.RemoveHandler functions used in HyperlinkUtility.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/01/hyperlinks_to_the_web_in_wpf.html">Hyperlinks to the Web in WPF</a>.</remarks>
	public static class ElementUtility
	{
		/// <summary>
		/// Adds a handler to the dependency object.
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <param name="re">The routed event.</param>
		/// <param name="de">The handler delegate.</param>
		public static void AddHandler(DependencyObject d, RoutedEvent re, Delegate de)
		{
			if (d is UIElement)
			{
				((UIElement)d).AddHandler(re, de);
			}
			else if (d is ContentElement)
			{
				((ContentElement)d).AddHandler(re, de);
			}
			else if (d is UIElement3D)
			{
				((UIElement3D)d).AddHandler(re, de);
			}
		}

		/// <summary>
		/// Removes a handler from the dependency object.
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <param name="re">The routed event.</param>
		/// <param name="de">The handler delegate.</param>
		public static void RemoveHandler(DependencyObject d, RoutedEvent re, Delegate de)
		{
			if (d is UIElement)
			{
				((UIElement)d).RemoveHandler(re, de);
			}
			else if (d is ContentElement)
			{
				((ContentElement)d).RemoveHandler(re, de);
			}
			else if (d is UIElement3D)
			{
				((UIElement3D)d).RemoveHandler(re, de);
			}
		}
	}
}
