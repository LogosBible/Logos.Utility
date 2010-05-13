
using System.IO;
using System.Text;
using Logos.Utility.IO;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	class StreamUtilityTests
	{
		[Test]
		public void DetectEncodingUtf8()
		{
			DoDetectEncoding(Encoding.UTF8, c_strEnglish);
			DoDetectEncoding(Encoding.UTF8, c_strKorean);
		}

		[Test]
		public void DetectEncodingUtf16()
		{
			DoDetectEncoding(Encoding.Unicode, c_strEnglish);
			DoDetectEncoding(Encoding.Unicode, c_strKorean);
		}

		[Test]
		public void DetectEncoding949()
		{
			DoDetectEncoding(Encoding.GetEncoding(949), c_strKorean);
		}

		[Test]
		public void DetectEncoding1252()
		{
			DoDetectEncoding(Encoding.GetEncoding(1252), c_strEnglish);
		}

		private static void DoDetectEncoding(Encoding encoding, string testPattern)
		{
			using (MemoryStream stream = new MemoryStream())
			using (StreamWriter writer = new StreamWriter(stream, encoding))
			{
				writer.Write(testPattern);
				writer.Flush();

				// detect encoding
				stream.Position = 0;
				Encoding detected = StreamUtility.DetectBestEncoding(stream);
				Assert.IsNotNull(detected);
				Assert.AreEqual(encoding.CodePage, detected.CodePage);

				// use encoding to read stream
				using (WrappingStream wrappingStream = new WrappingStream(stream, Ownership.None))
				using (StreamReader reader = new StreamReader(wrappingStream, detected, false))
				{
					string read = reader.ReadToEnd();
					Assert.AreEqual(testPattern, read);
				}
			}
		}

		const string c_strEnglish = "‘This’ is “Unicode” text—it has €123.45 and symbols.™\r\nIn the beginning, God created the heavens and the earth. The earth was without form and void, and darkness was over the face of the deep. And the Spirit of God was hovering over the face of the waters.";
		const string c_strKorean = "태초에 말씀이 계시니라 이 말씀이 하나님과 함께 계셨으니 이 말씀은 곧 하나님이시니라";
	}
}
