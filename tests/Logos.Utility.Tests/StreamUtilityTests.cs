
using System;
using System.IO;
using System.Linq;
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

		[Test]
		public void ReadExactlyBadArguments()
		{
			using (Stream stream = new MemoryStream())
			{
				Assert.Throws<ArgumentNullException>(() => StreamUtility.ReadExactly(null, new byte[1], 0, 1));
				Assert.Throws<ArgumentNullException>(() => stream.ReadExactly(null, 0, 1));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(new byte[1], -1, 1));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(new byte[1], 1, 1));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(new byte[1], 0, -1));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(new byte[1], 0, 2));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(new byte[1], 1, 1));
				Assert.Throws<ArgumentOutOfRangeException>(() => stream.ReadExactly(-1));
			}
		}

		[Test]
		public void ReadExactlyZeroBytes()
		{
			using (Stream stream = new MemoryStream())
			{
				Assert.IsNotNull(stream.ReadExactly(0));
				Assert.AreEqual(0, stream.ReadExactly(0).Length);
			}
		}

		[Test]
		public void ReadExactly()
		{
			byte[] abySource = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				byte[] read = stream.ReadExactly(5);
				CollectionAssert.AreEqual(abySource.Take(5), read);

				read = stream.ReadExactly(6);
				CollectionAssert.AreEqual(abySource.Skip(5), read);

				Assert.Throws<EndOfStreamException>(() => stream.ReadExactly(1));
			}

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				byte[] read = stream.ReadExactly(11);
				CollectionAssert.AreEqual(abySource, read);
			}

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				Assert.Throws<EndOfStreamException>(() => stream.ReadExactly(12));
			}
		}

		[Test]
		public void ReadExactlyArray()
		{
			byte[] abySource = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				byte[] buffer = new byte[20];
				stream.ReadExactly(buffer, 3, 8);
				CollectionAssert.AreEqual(new byte[3].Concat(abySource.Take(8)).Concat(new byte[9]), buffer);
			}

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				byte[] buffer = new byte[20];
				stream.ReadExactly(buffer, 5, 11);
				CollectionAssert.AreEqual(new byte[5].Concat(abySource).Concat(new byte[4]), buffer);
			}

			using (Stream streamSource = new MemoryStream(abySource))
			using (Stream stream = new SlowStream(streamSource))
			{
				byte[] buffer = new byte[20];
				Assert.Throws<EndOfStreamException>(() => stream.ReadExactly(buffer, 5, 12));
			}
		}

		private class SlowStream : WrappingStream
		{
			public SlowStream(Stream stream)
				: base(stream, Ownership.None)
			{
			}

			public override int Read(byte[] buffer, int offset, int count)
			{
				return base.Read(buffer, offset, Math.Min(count, 2));
			}
		}
	}
}
