using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for handling hyperlinks in WPF.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/01/hyperlinks_to_the_web_in_wpf.html">Hyperlinks to the Web in WPF</a>.</remarks>
	public static class HyperlinkUtility
	{
		/// <summary>
		/// Registers the LaunchDefaultBrowser property.
		/// </summary>
		public static readonly DependencyProperty LaunchDefaultBrowserProperty = DependencyProperty.RegisterAttached(
			"LaunchDefaultBrowser", typeof(bool), typeof(HyperlinkUtility), new PropertyMetadata(false, HyperlinkUtility_LaunchDefaultBrowserChanged));

		/// <summary>
		/// Gets the LaunchDefaultBrowser property.
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <returns>The property value.</returns>
		public static bool GetLaunchDefaultBrowser(DependencyObject d)
		{
			return (bool)d.GetValue(LaunchDefaultBrowserProperty);
		}

		/// <summary>
		/// Sets the LaunchDefaultBrowser property.
		/// </summary>
		/// <param name="d">The dependency object.</param>
		/// <param name="value">True or False.</param>
		public static void SetLaunchDefaultBrowser(DependencyObject d, bool value)
		{
			d.SetValue(LaunchDefaultBrowserProperty, value);
		}

		private static void HyperlinkUtility_LaunchDefaultBrowserChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			DependencyObject d = (DependencyObject)sender;

			if ((bool)e.NewValue)
				ElementUtility.AddHandler(d, Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
			else
				ElementUtility.RemoveHandler(d, Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
		}

		private static void Hyperlink_RequestNavigateEvent(object sender, RequestNavigateEventArgs e)
		{
			// use the utility to make the mouse look busy while web browser is starting
			MouseUtility.OverrideCursor(e.Uri.AbsoluteUri);
			e.Handled = true;
		}
	}
}
