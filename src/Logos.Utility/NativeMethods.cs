
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Logos.Utility
{
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		[DllImport("Kernel32.dll", ExactSpelling = true)]
		public static extern IntPtr GetCurrentThread();
	}

	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		[DllImport("Kernel32.dll", ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetThreadPriority(IntPtr hThread, int nPriority);
	}
}
