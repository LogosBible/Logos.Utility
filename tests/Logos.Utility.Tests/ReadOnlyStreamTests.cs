
using System;
using System.IO;
using Logos.Utility.IO;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class ReadOnlyStreamTests
	{
		[SetUp]
		public void SetUp()
		{
			m_memStream = new MemoryStream(m_abyStreamData, true);
			m_stream = new ReadOnlyStream(m_memStream);
		}

		[TearDown]
		public void TearDown()
		{
			m_stream = null;
			m_memStream = null;
		}

		[Test]
		public void Constructor()
		{
			Assert.IsTrue(m_stream.CanRead);
			Assert.IsTrue(m_stream.CanSeek);
			Assert.IsFalse(m_stream.CanWrite);
			Assert.AreEqual(m_abyStreamData.Length, m_stream.Length);
		}

		[Test]
		public void ConstructorNull()
		{
			Assert.Throws<ArgumentNullException>(() => { ReadOnlyStream stream = new ReadOnlyStream(null); });
		}

		[Test]
		public void Dispose()
		{
			m_stream.Dispose();
			m_stream.Dispose();

			Assert.IsTrue(m_memStream.CanRead);
			Assert.IsFalse(m_stream.CanRead);
			Assert.IsFalse(m_stream.CanSeek);

			Assert.Throws<ObjectDisposedException>(() => { long i = m_stream.Length; });
			Assert.Throws<ObjectDisposedException>(() => { long i = m_stream.Position; });
			Assert.Throws<ObjectDisposedException>(() => { m_stream.Position = 0; });
			Assert.Throws<ObjectDisposedException>(() => m_stream.BeginRead(new byte[1], 0, 1, null, null));
			Assert.Throws<ObjectDisposedException>(() => m_stream.EndRead(null));
			Assert.Throws<ObjectDisposedException>(() => m_stream.Flush());
			Assert.Throws<ObjectDisposedException>(() => m_stream.Read(new byte[1], 0, 1));
			Assert.Throws<ObjectDisposedException>(() => m_stream.ReadByte());
			Assert.Throws<ObjectDisposedException>(() => m_stream.Seek(0, SeekOrigin.Begin));
		}

		[Test]
		public void Flush()
		{
			m_stream.Flush();
		}

		[Test]
		public void Read()
		{
			byte[] aby = new byte[m_abyStreamData.Length];
			Assert.AreEqual(aby.Length, m_stream.Read(aby, 0, aby.Length));
			CollectionAssert.AreEqual(m_abyStreamData, aby);
		}

		[Test]
		public void ReadAsync()
		{
			byte[] aby = new byte[m_abyStreamData.Length];
			IAsyncResult ar = m_stream.BeginRead(aby, 0, 8, null, null);
			Assert.AreEqual(aby.Length, m_stream.EndRead(ar));
			CollectionAssert.AreEqual(m_abyStreamData, aby);
		}

		[Test]
		public void ReadByte()
		{
			Assert.AreEqual(m_abyStreamData[0], m_stream.ReadByte());
			Assert.AreEqual(m_abyStreamData[1], m_stream.ReadByte());
			Assert.AreEqual(m_abyStreamData[2], m_stream.ReadByte());
			m_stream.Seek(1, SeekOrigin.Current);
			Assert.AreEqual(m_abyStreamData[4], m_stream.ReadByte());
			m_stream.Position = m_stream.Position - 2;
			Assert.AreEqual(m_abyStreamData[3], m_stream.ReadByte());
			m_stream.Seek(7, SeekOrigin.Begin);
			Assert.AreEqual(m_abyStreamData[7], m_stream.ReadByte());
			m_stream.Seek(0, SeekOrigin.End);
			Assert.AreEqual(-1, m_stream.ReadByte());
		}

		[Test]
		public void Write()
		{
			Assert.Throws<NotSupportedException>(() => m_stream.Write(m_abyStreamData, 0, 1));
			Assert.Throws<NotSupportedException>(() => m_stream.WriteByte(0));
			Assert.Throws<NotSupportedException>(() => m_stream.BeginWrite(m_abyStreamData, 0, 1, null, null));
			Assert.Throws<NotSupportedException>(() => m_stream.EndWrite(null));
			Assert.Throws<NotSupportedException>(() => m_stream.SetLength(0));
		}

		Stream m_memStream;
		Stream m_stream;
		byte[] m_abyStreamData = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
	}
}
