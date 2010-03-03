
using System.IO;

namespace Logos.Utility.IO
{
	/// <summary>
	/// <see cref="RebasedStream"/> is a <see cref="WrappingStream"/> that changes the effective origin of the wrapped stream.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/08/image_format_error_when_loading_from_a_stream.html">Image Format Error when Loading from a Stream</a>.</remarks>
	public sealed class RebasedStream : WrappingStream
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RebasedStream"/> class; the current position in <paramref name="streamBase"/>
		/// will be the origin of the <see cref="RebasedStream"/>.
		/// </summary>
		/// <param name="streamBase">The base stream.</param>
		public RebasedStream(Stream streamBase)
			: base(streamBase, Ownership.None)
		{
			m_baseOffset = streamBase.Position;
		}

		/// <summary>
		/// Gets the length in bytes of the stream.
		/// </summary>
		public override long Length
		{
			get { return base.Length - m_baseOffset; }
		}

		/// <summary>
		/// Gets or sets the position within the current stream.
		/// </summary>
		public override long Position
		{
			get { return base.Position - m_baseOffset; }
			set { base.Position = value + m_baseOffset; }
		}

		/// <summary>
		/// Sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the <paramref name="origin"/> parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
		/// <returns>The new position within the current stream.</returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin)
				offset += m_baseOffset;

			return base.Seek(offset, origin) - m_baseOffset;
		}

		/// <summary>
		/// Sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		public override void SetLength(long value)
		{
			base.SetLength(value + m_baseOffset);
		}

		// the offset within the base stream where this stream begins
		readonly long m_baseOffset;
	}
}
