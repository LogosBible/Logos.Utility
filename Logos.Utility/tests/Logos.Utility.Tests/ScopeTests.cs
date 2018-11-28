
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class ScopeTests
	{
		[Test]
		public void Empty()
		{
			// should compile and run without errors
			using (Scope.Empty)
			{
			}
		}

		[Test]
		public void Create()
		{
			int i = 0;
			using (Scope.Create(() => i++))
			{
				Assert.That(i, Is.EqualTo(0));
			}
			Assert.That(i, Is.EqualTo(1));
		}

		[Test]
		public void Cancel()
		{
			int i = 0;
			using (Scope scope = Scope.Create(() => i++))
			{
				Assert.That(i, Is.EqualTo(0));
				scope.Cancel();
			}
			Assert.That(i, Is.EqualTo(0));
		}

		[Test]
		public void Transfer()
		{
			int i = 0;
			using (Scope scope = Scope.Create(() => i++))
			{
				Assert.That(i, Is.EqualTo(0));

				using (Scope transferred = scope.Transfer())
				{
					Assert.That(i, Is.EqualTo(0));
				}
				Assert.That(i, Is.EqualTo(1));
			}
			Assert.That(i, Is.EqualTo(1));
		}
	}
}
