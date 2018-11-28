
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	/// <summary>
	/// Provides a reference implementation of a clonable class.
	/// </summary>
	/// <remarks>See <a href="http://code.logos.com/blog/2008/06/implementing_clone.html">Implementing Clone</a>.</remarks>
	public class ClonableBase
	{
		public ClonableBase()
		{
		}

		public int Count { get; set; }

		public ClonableBase Clone()
		{
			return CloneCore();
		}

		protected virtual ClonableBase CloneCore()
		{
			return new ClonableBase(this);
		}

		protected ClonableBase(ClonableBase other)
		{
			Count = other.Count;
		}
	}

	public class ClonableDerived : ClonableBase
	{
		public ClonableDerived()
		{
		}

		public string Name { get; set; }

		public new ClonableDerived Clone()
		{
			return (ClonableDerived) CloneCore();
		}

		protected override ClonableBase CloneCore()
		{
			return new ClonableDerived(this);
		}

		protected ClonableDerived(ClonableDerived other)
			: base(other)
		{
			Name = other.Name;
		}
	}

	[TestFixture]
	public class ClonableClassTests
	{
		[Test]
		public void CloneBase()
		{
			ClonableBase b = new ClonableBase { Count = 3 };
			ClonableBase b2 = b.Clone();
			Assert.That(b2, Is.Not.SameAs(b));
			Assert.That(b2.Count, Is.EqualTo(b.Count));
		}

		[Test]
		public void CloneDerived()
		{
			ClonableDerived d = new ClonableDerived { Count = 3, Name = "Test" };
			ClonableDerived d2 = d.Clone();
			Assert.That(d2, Is.Not.SameAs(d));
			Assert.That(d2.Count, Is.EqualTo(d.Count));
			Assert.That(d2.Name, Is.EqualTo(d.Name));
		}

		[Test]
		public void CloneDerivedAsBase()
		{
			ClonableBase b = new ClonableDerived { Count = 3, Name = "Test" };
			ClonableBase b2 = b.Clone();
			Assert.That(b2, Is.Not.SameAs(b));
			Assert.That(b2.Count, Is.EqualTo(b.Count));
			Assert.That(b2, Is.TypeOf<ClonableDerived>());
			Assert.That(((ClonableDerived) b2).Name, Is.EqualTo(((ClonableDerived) b).Name));
		}
	}
}
