
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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

	[ComImport, Guid("275c23e2-3747-11d0-9fea-00aa003f8646"), ClassInterface(ClassInterfaceType.None)]
	internal class MultiLanguage
	{
	}

	[ComImport, Guid("DCCFC164-2B38-11d2-B7EC-00C04F8F5D9A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	interface IMultiLanguage2
	{
		// NOTE: Only DetectCodepageInIStream is correctly defined; the other methods are simply placeholders so that the vtable order is correct.

		void GetNumberOfCodePageInfo();
		void GetCodePageInfo();
		void GetFamilyCodePage();
		void EnumCodePages();
		void GetCharsetInfo();
		void IsConvertible();
		void ConvertString();
		void ConvertStringToUnicode();
		void ConvertStringFromUnicode();
		void ConvertStringReset();
		void GetRfc1766FromLcid();
		void GetLcidFromRfc1766();
		void EnumRfc1766();
		void GetRfc1766Info();
		void CreateConvertCharset();
		void ConvertStringInIStream();
		void ConvertStringToUnicodeEx();
		void ConvertStringFromUnicodeEx();

		[PreserveSig]
		int DetectCodepageInIStream(MultiLanguageDetectCodePage flags, uint dwPrefWinCodePage, IStream pstmIn, IntPtr lpEncoding, ref int pnScores);

		void DetectInputCodepage();
		void ValidateCodePage();
		void GetCodePageDescription();
		void IsCodePageInstallable();
		void SetMimeDBSource();
		void GetNumberOfScripts();
		void EnumScripts();
		void ValidateCodePageEx();
	};

	[StructLayout(LayoutKind.Sequential)]
	struct DetectEncodingInfo
	{
		public uint nLangID;
		public uint nCodePage;
		public int nDocPercent;
		public int nConfidence;
	}

	enum MultiLanguageDetectCodePage
	{
		None = 0,
		SevenBit = 1,
		EightBit = 2,
		Dbcs = 4,
		Html = 8,
		Number = 16,
	}
}
