
using System;

namespace Logos.Utility.Basic
{
	/// <summary>
	/// Provides utility methods for inspecting and using the current environment.
	/// </summary>
	public static class EnvironmentUtility
	{
		/// <summary>
		/// Returns <c>true</c> if the current OS is Windows Vista (or Server 2008) or later.
		/// </summary>
		/// <returns><c>true</c> if the current OS is at least Windows Vista (or Server 2008).</returns>
		public static bool IsWindowsVistaOrLater()
		{
			OperatingSystem os = Environment.OSVersion;
			return os.Platform == PlatformID.Win32NT && os.Version >= new Version(6, 0);
		}
	}
}
