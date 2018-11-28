
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Logos.Utility.IO
{
	/// <summary>
	/// Provides helper methods for working with <see cref="Stream"/>.
	/// </summary>
	public static class StreamUtility
	{
		/// <summary>
		/// Detects the best <see cref="Encoding"/> to use to convert the data in the supplied stream to Unicode, and returns it.
		/// </summary>
		/// <param name="stream">The stream to detect the character encoding for.</param>
		/// <returns>The best <see cref="Encoding"/> object to be used to decode text from <paramref name="stream"/>
		/// into Unicode, or <c>null</c> if the best encoding can't be detected.</returns>
		/// <remarks>See <a href="http://code.logos.com/blog/2010/05/detecting_the_character_encoding_of_a_file.html">Detecting the Character Encoding of a File</a>.</remarks>
		public static Encoding DetectBestEncoding(Stream stream)
		{
            // check parameter validity
            if (stream == null)
				throw new ArgumentNullException("stream");
			if (!stream.CanRead)
				throw new NotSupportedException("'stream' must be readable.");
			if (!stream.CanSeek)
				throw new NotSupportedException("'stream' must be seekable.");

			// the encoding that was detected, or null on failure
			Encoding encoding = null;

			// MLang will move the stream pointer; remember its original position
			long position = stream.Position;

			// allocate a number of DetectEncodingInfo structures for MLang to fill in
			DetectEncodingInfo[] infos = new DetectEncodingInfo[8];
			int infoCount = infos.Length;

			// allow MLang to seek to the "beginning" (i.e., current position) of the stream by rebasing it
			using (RebasedStream rebased = new RebasedStream(stream))
			{
				try
				{
					// try to create MLang object
					IMultiLanguage2 multiLanguage = (IMultiLanguage2) new MultiLanguage();

					// wrap input stream with an IStream
					ManagedIStream istream = new ManagedIStream(rebased);

					// detect the code page
					int hresult = multiLanguage.DetectCodepageInIStream(MultiLanguageDetectCodePage.None, 0, istream, ref infos[0], ref infoCount);
					GC.KeepAlive(istream);

					if (infoCount > 0 && (hresult == Win32.S_OK || hresult == Win32.S_FALSE))
					{
						// take the best code page that was found
						int nCodePage = (int) infos.Take(infoCount).OrderByDescending(i => i.nConfidence).Select(i => i.nCodePage).FirstOrDefault();
						encoding = Encoding.GetEncoding(nCodePage);
					}
				}
				catch (COMException)
				{
					// failure
				}
			}

			// reset the stream back to its input position for the caller
			stream.Position = position;

			// return detected encoding (or null for failure)
			return encoding;
		}

		/// <summary>
		/// Reads exactly <paramref name="count"/> bytes from <paramref name="stream"/>.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="count">The count of bytes to read.</param>
		/// <returns>A new byte array containing the data read from the stream.</returns>
		public static byte[] ReadExactly(this Stream stream, int count)
		{
			if (count < 0)
				throw new ArgumentOutOfRangeException("count");
			byte[] buffer = new byte[count];
			ReadExactly(stream, buffer, 0, count);
			return buffer;
		}

		/// <summary>
		/// Reads exactly <paramref name="count"/> bytes from <paramref name="stream"/> into
		/// <paramref name="buffer"/>, starting at the byte given by <paramref name="offset"/>.
		/// </summary>
		/// <param name="stream">The stream to read from.</param>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="offset">The offset within the buffer at which data is first written.</param>
		/// <param name="count">The count of bytes to read.</param>
		public static void ReadExactly(this Stream stream, byte[] buffer, int offset, int count)
		{
			// check arguments
			if (stream == null)
				throw new ArgumentNullException("stream");
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (offset < 0 || offset > buffer.Length)
				throw new ArgumentOutOfRangeException("offset");
			if (count < 0 || buffer.Length - offset < count)
				throw new ArgumentOutOfRangeException("count");

			while (count > 0)
			{
				// read data
				int bytesRead = stream.Read(buffer, offset, count);

				// check for failure to read
				if (bytesRead == 0)
					throw new EndOfStreamException();

				// move to next block
				offset += bytesRead;
				count -= bytesRead;
			}
		}
	}
}
