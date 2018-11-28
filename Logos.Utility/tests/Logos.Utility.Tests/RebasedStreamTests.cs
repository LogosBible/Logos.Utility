
using System;
using System.IO;
using Logos.Utility.IO;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class RebasedStreamTests
	{
		[SetUp]
		public void SetUp()
		{
			m_buffer = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
			m_stream = new MemoryStream();
			m_stream.Write(m_buffer, 0, m_buffer.Length);
			m_stream.Position = 0;
		}

		[TearDown]
		public void TearDown()
		{
			m_stream.Dispose();
		}

		[Test]
		public void BadConstructorArguments()
		{
			Assert.Throws<ArgumentNullException>(() => new RebasedStream(null));
		}

		[Test]
		public void Length()
		{
			using (RebasedStream stream = CreateRebasedStream(2))
			{
				Assert.That(stream.Length, Is.EqualTo(m_buffer.Length - 2));
			}
		}

		[Test]
		public void Position()
		{
			using (RebasedStream stream = CreateRebasedStream(3))
			{
				stream.Position = 4;
				Assert.That(stream.Position, Is.EqualTo(4));
				Assert.That(m_stream.Position, Is.EqualTo(7));

				stream.Position = 0;
				Assert.That(stream.Position, Is.EqualTo(0));
				Assert.That(m_stream.Position, Is.EqualTo(3));
			}
		}

		[Test]
		public void Seek()
		{
			using (RebasedStream stream = CreateRebasedStream(5))
			{
				Assert.AreEqual(0, stream.Seek(0, SeekOrigin.Begin));
				Assert.AreEqual(0, stream.Position);
				Assert.AreEqual(5, m_stream.Position);

				Assert.AreEqual(2, stream.Seek(2, SeekOrigin.Begin));
				Assert.AreEqual(2, stream.Position);
				Assert.AreEqual(7, m_stream.Position);

				Assert.AreEqual(4, stream.Seek(2, SeekOrigin.Current));
				Assert.AreEqual(4, stream.Position);
				Assert.AreEqual(9, m_stream.Position);

				Assert.AreEqual(6, stream.Seek(-1, SeekOrigin.End));
				Assert.AreEqual(6, stream.Position);
				Assert.AreEqual(11, m_stream.Position);
			}
		}

		[Test]
		public void SetLengthShorter()
		{
			using (RebasedStream stream = CreateRebasedStream(4))
			{
				stream.SetLength(5);
				Assert.That(stream.Length, Is.EqualTo(5));
				Assert.That(m_stream.Length, Is.EqualTo(9));
			}
		}

		[Test]
		public void SetLengthLonger()
		{
			using (RebasedStream stream = CreateRebasedStream(4))
			{
				stream.SetLength(15);
				Assert.That(stream.Length, Is.EqualTo(15));
				Assert.That(m_stream.Length, Is.EqualTo(19));
			}
		}

		[Test]
		public void Read()
		{
			using (RebasedStream stream = CreateRebasedStream(3))
			{
				byte[] buffer = new byte[16];
				Assert.AreEqual(9, stream.Read(buffer, 0, buffer.Length));
				CollectionAssert.AreEqual(new byte[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 0, 0, 0, 0, 0, 0 }, buffer);
			}
		}

		[Test]
		public void Dispose()
		{
			using (RebasedStream stream = CreateRebasedStream(3))
			{
				stream.Dispose();
				Assert.Throws<ObjectDisposedException>(() => { long p = stream.Position; });
				Assert.Throws<ObjectDisposedException>(() => stream.Position = 3);
				Assert.Throws<ObjectDisposedException>(() => { long p = stream.Length; });
				Assert.Throws<ObjectDisposedException>(() => stream.SetLength(20));
				Assert.Throws<ObjectDisposedException>(() => stream.Seek(0, SeekOrigin.Begin));

			}
		}

		private RebasedStream CreateRebasedStream(int offset)
		{
			m_stream.Position = offset;
			return new RebasedStream(m_stream);
		}

		byte[] m_buffer;
		Stream m_stream;
	}
}
