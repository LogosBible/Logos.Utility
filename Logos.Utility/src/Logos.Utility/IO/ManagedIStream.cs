
using System;
using System.IO;
using System.Runtime.InteropServices;
using IStream = System.Runtime.InteropServices.ComTypes.IStream;
using STATSTG = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace Logos.Utility.IO
{
	/// <summary>
	/// <see cref="ManagedIStream"/> provides an implementation of the COM IStream interface that wraps a .NET <see cref="Stream"/>.
	/// </summary>
	internal sealed class ManagedIStream : IStream
	{
		public ManagedIStream(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			m_stream = stream;
		}

		void IStream.Read(byte[] buffer, int cb, IntPtr pcbRead)
		{
			int val = m_stream.Read(buffer, 0, cb);
			if (pcbRead != IntPtr.Zero)
				Marshal.WriteInt32(pcbRead, val);
		}

		void IStream.Write(byte[] buffer, int cb, IntPtr pcbWritten)
		{
			m_stream.Write(buffer, 0, cb);
			if (pcbWritten != IntPtr.Zero)
				Marshal.WriteInt32(pcbWritten, cb);
		}

		void IStream.Seek(long offset, int dwOrigin, IntPtr plibNewPosition)
		{
			SeekOrigin origin;
			switch (dwOrigin)
			{
			case Win32.STREAM_SEEK_SET: origin = SeekOrigin.Begin; break;
			case Win32.STREAM_SEEK_CUR: origin = SeekOrigin.Current; break;
			case Win32.STREAM_SEEK_END: origin = SeekOrigin.End; break;
			default: throw new ArgumentOutOfRangeException("dwOrigin");
			}

			long val = m_stream.Seek(offset, origin);
			if (plibNewPosition != IntPtr.Zero)
				Marshal.WriteInt64(plibNewPosition, val);
		}

		void IStream.SetSize(long libNewSize)
		{
			m_stream.SetLength(libNewSize);
		}

		void IStream.CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
		{
			throw new NotSupportedException();
		}

		void IStream.Commit(int grfCommitFlags)
		{
			throw new NotSupportedException();
		}

		void IStream.Revert()
		{
			throw new NotSupportedException();
		}

		void IStream.LockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		void IStream.UnlockRegion(long libOffset, long cb, int dwLockType)
		{
			throw new NotSupportedException();
		}

		void IStream.Stat(out STATSTG pstatstg, int grfStatFlag)
		{
			pstatstg = new STATSTG
			{
				type = 2,
				cbSize = m_stream.Length,
			};

			if (m_stream.CanRead && m_stream.CanWrite)
				pstatstg.grfMode = Win32.STGM_READWRITE;
			else if (m_stream.CanWrite)
				pstatstg.grfMode = Win32.STGM_WRITE;
			else if (m_stream.CanRead)
				pstatstg.grfMode = Win32.STGM_READ;
			else
				throw new IOException();
		}

		void IStream.Clone(out IStream ppstm)
		{
			ppstm = null;
			throw new NotSupportedException();
		}

		readonly Stream m_stream;
	}
}
