using System;
using System.IO;
using System.Text;

namespace Logos.Utility
{
	/// <summary>
	/// Converts between binary data and an Ascii85-encoded string.
	/// </summary>
	/// <remarks>See <a href="http://en.wikipedia.org/wiki/Ascii85">Ascii85 at Wikipedia</a>.</remarks>
	public static class Ascii85
	{
		/// <summary>
		/// Encodes the specified byte array in Ascii85.
		/// </summary>
		/// <param name="bytes">The bytes to encode.</param>
		/// <returns>An Ascii85-encoded string representing the input byte array.</returns>
		public static string Encode(byte[] bytes)
		{
			if (bytes == null)
				throw new ArgumentNullException("bytes");

			// preallocate a StringBuilder with enough room to store the encoded bytes
			StringBuilder sb = new StringBuilder(bytes.Length * 5 / 4);

			// walk the bytes
			int count = 0;
			uint value = 0;
			foreach (byte b in bytes)
			{
				// build a 32-bit value from the bytes
				value |= ((uint) b) << (24 - (count * 8));
				count++;

				// every 32 bits, convert the previous 4 bytes into 5 Ascii85 characters
				if (count == 4)
				{
					if (value == 0)
						sb.Append('z');
					else
						EncodeValue(sb, value, 0);
					count = 0;
					value = 0;
				}
			}

			// encode any remaining bytes (that weren't a multiple of 4)
			if (count > 0)
				EncodeValue(sb, value, 4 - count);

			return sb.ToString();
		}

		/// <summary>
		/// Decodes the specified Ascii85 string into the corresponding byte array.
		/// </summary>
		/// <param name="encoded">The Ascii85 string.</param>
		/// <returns>The decoded byte array.</returns>
		public static byte[] Decode(string encoded)
		{
			if (encoded == null)
				throw new ArgumentNullException("encoded");

			// preallocate a memory stream with enough capacity to hold the decoded data
			using (MemoryStream stream = new MemoryStream(encoded.Length * 4 / 5))
			{
				// walk the input string
				int count = 0;
				uint value = 0;
				foreach (char ch in encoded)
				{
					if (ch == 'z' && count == 0)
					{
						// handle "z" block specially
						DecodeValue(stream, value, 0);
					}
					else if (ch < FirstChar || ch > LastChar)
					{
						throw new FormatException("Invalid character '{0}' in Ascii85 block.".FormatInvariant(ch));
					}
					else
					{
						// build a 32-bit value from the input characters
						try
						{
							checked { value += (uint) (s_powersOf85[count] * (ch - FirstChar)); }
						}
						catch (OverflowException ex)
						{
							throw new FormatException("The current group of characters decodes to a value greater than UInt32.MaxValue.", ex);
						}
						
						count++;

						// every five characters, convert the characters into the equivalent byte array
						if (count == 5)
						{
							DecodeValue(stream, value, 0);
							count = 0;
							value = 0;
						}
					}
				}

				if (count == 1)
				{
					throw new FormatException("The final Ascii85 block must contain more than one character.");
				}
				else if (count > 1)
				{
					// decode any remaining characters
					for (int padding = count; padding < 5; padding++)
					{
						try
						{
							checked { value += 84 * s_powersOf85[padding]; }
						}
						catch (OverflowException ex)
						{
							throw new FormatException("The current group of characters decodes to a value greater than UInt32.MaxValue.", ex);
						}
					}
					DecodeValue(stream, value, 5 - count);
				}

				return stream.ToArray();
			}
		}

		// Writes the Ascii85 characters for a 32-bit value to a StringBuilder.
		private static void EncodeValue(StringBuilder sb, uint value, int paddingBytes)
		{
			char[] encoded = new char[5];

			for (int index = 4; index >= 0; index--)
			{
				encoded[index] = (char) ((value % 85) + FirstChar);
				value /= 85;
			}

			if (paddingBytes != 0)
				Array.Resize(ref encoded, 5 - paddingBytes);

			sb.Append(encoded);
		}

		// Writes the bytes of a 32-bit value to a stream.
		private static void DecodeValue(Stream stream, uint value, int paddingChars)
		{
			stream.WriteByte((byte) (value >> 24));
			if (paddingChars == 3)
				return;
			stream.WriteByte((byte) ((value >> 16) & 0xFF));
			if (paddingChars == 2)
				return;
			stream.WriteByte(((byte) ((value >> 8) & 0xFF)));
			if (paddingChars == 1)
				return;
			stream.WriteByte((byte) (value & 0xFF));
		}

        /// <summary>
        /// The first character used in the Ascii85 encoding character set
        /// </summary>
        public const char FirstChar = '!';

        /// <summary>
        /// The last character used in the Ascii85 encoding character set
        /// </summary>
        public const char LastChar = 'u';

		static readonly uint[] s_powersOf85 = new uint[] { 85u * 85u * 85u * 85u, 85u * 85u * 85u, 85u * 85u, 85u, 1 };
	}
}
