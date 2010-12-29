
using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Logos.Utility
{
	/// <summary>
	/// A wrapper for <c>GCHandle.Alloc(obj, GCHandleType.Pinned)</c> and <see cref="GCHandle.Free"/> that supports <see cref="IDisposable"/>.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2010/05/pinned_gchandle_wrapper.html">Pinned GCHandle Wrapper</a>.</remarks>
	[StructLayout(LayoutKind.Auto)]
	public struct PinnedGCHandle : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PinnedGCHandle"/> class. <see cref="Dispose"/> must be called to unpin the object when it is no longer required.
		/// </summary>
		/// <param name="obj">The object to pin.</param>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public PinnedGCHandle(object obj)
		{
			m_handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
		}

		/// <summary>
		/// Unpins the object that was pinned.
		/// </summary>
		public void Dispose()
		{
			if (m_handle.IsAllocated)
				m_handle.Free();
		}

		/// <summary>
		/// Gets an <see cref="IntPtr"/> to the pinned object.
		/// </summary>
		/// <value>The pointer to the memory of the object that was pinned when this object was constructed.</value>
		public IntPtr Pointer
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return m_handle.AddrOfPinnedObject();
			}
		}

		/// <summary>
		/// Implicitly converts this <see cref="PinnedGCHandle"/> object to an <see cref="IntPtr"/>.
		/// </summary>
		/// <param name="handle">The <see cref="PinnedGCHandle"/> to convert.</param>
		/// <returns>An <see cref="IntPtr"/> to the memory of the object pinned by the <see cref="PinnedGCHandle"/>.</returns>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static implicit operator IntPtr(PinnedGCHandle handle)
		{
			return handle.Pointer;
		}

		readonly GCHandle m_handle;
	}
}
