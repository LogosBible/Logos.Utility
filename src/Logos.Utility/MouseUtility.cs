using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for mouse manipulation.
	/// </summary>
	/// <remarks> See <a href="http://code.logos.com/blog/2008/01/using_processstart_to_link_to.html">Using Process.Start to link to the Internet</a>.</remarks>
	public static class MouseUtility
	{
		/// <summary>
		/// Overrides the cursor with a busy one while a process is starting.
		/// </summary>
		/// <param name="process">The process (or webpage) to start.</param>
		public static void OverrideCursor(String process)
		{
			try
			{
				Mouse.OverrideCursor = Cursors.AppStarting;
				Process.Start(process);
			}
			catch (Exception)
			{
			}
			finally
			{
				Mouse.OverrideCursor = null;
			}
		}
	}
}
