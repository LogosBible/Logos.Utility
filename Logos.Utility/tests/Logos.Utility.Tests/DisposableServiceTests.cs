
using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class DisposableServiceTests
	{
		[Test]
		public void Dispose()
		{
			using (TestService service = new TestService())
			{
				Assert.That(service.OnDisposingCalled, Is.EqualTo(0));
				Assert.That(service.DisposeCalled, Is.EqualTo(0));
				Assert.That(service.Method(), Is.EqualTo(1));

				service.Dispose();

				Assert.That(service.OnDisposingCalled, Is.EqualTo(1));
				Assert.That(service.DisposeCalled, Is.EqualTo(1));
				Assert.Throws<ObjectDisposedException>(() => service.Method());

				service.Dispose();

				Assert.That(service.OnDisposingCalled, Is.EqualTo(1));
				Assert.That(service.DisposeCalled, Is.EqualTo(1));
			}
		}

		[Test]
		public void DisposingEvent()
		{
			int called = 0;

			using (TestService service = new TestService())
			{
				service.Disposing += (s, e) => called++;
				Assert.That(called, Is.EqualTo(0));
				Assert.That(service.Method(), Is.EqualTo(1));

				service.Dispose();
				Assert.That(called, Is.EqualTo(1));
				Assert.Throws<ObjectDisposedException>(() => service.Method());

				service.Dispose();
				Assert.That(called, Is.EqualTo(1));
			}
		}

		private class TestService : DisposableService
		{
			public int OnDisposingCalled { get; private set; }

			public int DisposeCalled { get; private set; }

			public int Method()
			{
				VerifyNotDisposed();

				return 1; 
			}

			protected override void OnDisposing()
			{
				// shouldn't throw
				VerifyNotDisposed();

				OnDisposingCalled++;
				base.OnDisposing();
			}

			protected override void Dispose(bool disposing)
			{
				try
				{
					DisposeCalled++;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}
	}
}
