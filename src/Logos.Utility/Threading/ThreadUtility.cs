
using Logos.Utility.Basic;
using System;
using System.Security.Permissions;
using System.Threading;

namespace Logos.Utility.Threading
{
	/// <summary>
	/// Utility methods for working with threads.
	/// </summary>
	public static class ThreadUtility
	{
		/// <summary>
		/// Puts the current thread into background processing mode.
		/// </summary>
		/// <returns>A Scope that must be disposed to leave background processing mode.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2008/10/using_background_processing_mode_from_c.html">Using
		/// "Background Processing Mode" from C#</a>.</remarks>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlThread)]
		public static DisposibleObject EnterBackgroundProcessingMode()
		{
			Thread.BeginThreadAffinity();
			IntPtr hThread = SafeNativeMethods.GetCurrentThread();
			if (EnvironmentUtility.IsWindowsVistaOrLater() && UnsafeNativeMethods.SetThreadPriority(hThread,
				Win32.THREAD_MODE_BACKGROUND_BEGIN))
			{
				// OS supports background processing; return Scope that exits this mode
				return DisposibleObject.Create(() =>
				{
					UnsafeNativeMethods.SetThreadPriority(hThread, Win32.THREAD_MODE_BACKGROUND_END);
					Thread.EndThreadAffinity();
				});
			}

			// OS doesn't support background processing mode (or setting it failed)
			Thread.EndThreadAffinity();
			return DisposibleObject.Empty;
		}
	}
}
