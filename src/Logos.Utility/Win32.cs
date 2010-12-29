
namespace Logos.Utility
{
	internal static class Win32
	{
		public const int THREAD_MODE_BACKGROUND_BEGIN = 0x00010000;
		public const int THREAD_MODE_BACKGROUND_END = 0x00020000;

		public const int S_OK = 0;
		public const int S_FALSE = 1;

		public const int STGM_READ = 0;
		public const int STGM_WRITE = 1;
		public const int STGM_READWRITE = 2;

		public const int STREAM_SEEK_SET = 0;
		public const int STREAM_SEEK_CUR = 1;
		public const int STREAM_SEEK_END = 2;
	}
}
