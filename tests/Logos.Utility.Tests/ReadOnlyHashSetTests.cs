using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class ReadOnlyHashSetTests
	{
		[SetUp]
		public void SetUp()
		{
			m_set = new HashSet<int> { 1, 2, 3 }.AsReadOnly();
		}

		[Test, ExpectedException(typeof(ArgumentNullException))]
		public void InvalidConstructorArgument()
		{
			var obj = new ReadOnlyHashSet<int>(null);
		}

		[Test]
		public void IsReadOnly()
		{
			Assert.IsTrue(m_set.IsReadOnly);
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void AddToCollection()
		{
			((ICollection<int>) m_set).Add(0);
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void Remove()
		{
			((ICollection<int>) m_set).Remove(1);
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void Clear()
		{
			((ICollection<int>) m_set).Clear();
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void AddToSet()
		{
			((ISet<int>) m_set).Add(0);
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void ExceptWith()
		{
			((ISet<int>) m_set).ExceptWith(new[] { 1 });
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void IntersectWith()
		{
			((ISet<int>) m_set).IntersectWith(new[] {1 });
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void SymmetricExceptWith()
		{
			((ISet<int>) m_set).SymmetricExceptWith(new[] { 1 });
		}

		[Test, ExpectedException(typeof(NotSupportedException))]
		public void UnionWith()
		{
			((ISet<int>) m_set).UnionWith(new[] { 0 });
		}

		[Test]
		public void Contains()
		{
			Assert.IsFalse(m_set.Contains(0));
			Assert.IsTrue(m_set.Contains(1));
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(3, m_set.Count);
		}

		[Test]
		public void IsSubsetOf()
		{
			Assert.IsTrue(m_set.IsSubsetOf(new[] { 1, 2, 3 }));
			Assert.IsTrue(m_set.IsSubsetOf(new[] { 1, 2, 3, 4 }));
			Assert.IsFalse(m_set.IsSubsetOf(new[] { 2, 3, 4 }));
		}

		[Test]
		public void IsProperSubsetOf()
		{
			Assert.IsFalse(m_set.IsProperSubsetOf(new[] { 1, 2, 3 }));
			Assert.IsTrue(m_set.IsProperSubsetOf(new[] { 1, 2, 3, 4 }));
			Assert.IsFalse(m_set.IsProperSubsetOf(new[] { 2, 3, 4 }));
		}

		[Test]
		public void IsSupersetOf()
		{
			Assert.IsTrue(m_set.IsSupersetOf(new[] { 1, 2, 3 }));
			Assert.IsFalse(m_set.IsSupersetOf(new[] { 1, 2, 3, 4 }));
			Assert.IsTrue(m_set.IsSupersetOf(new[] { 2, 3 }));
		}

		[Test]
		public void IsProperSupersetOf()
		{
			Assert.IsFalse(m_set.IsProperSupersetOf(new[] { 1, 2, 3 }));
			Assert.IsFalse(m_set.IsProperSupersetOf(new[] { 1, 2, 3, 4 }));
			Assert.IsTrue(m_set.IsProperSupersetOf(new[] { 2, 3 }));
		}

		[Test]
		public void Overlaps()
		{
			Assert.IsTrue(m_set.Overlaps(new[] { 2, 3, 4 }));
			Assert.IsFalse(m_set.Overlaps(new[] { 4, 5, 6 }));
		}

		[Test]
		public void SetEquals()
		{
			Assert.IsFalse(m_set.SetEquals(new[] { 2, 3, 4 }));
			Assert.IsTrue(m_set.SetEquals(new[] { 1, 2, 3 }));
		}

		ReadOnlyHashSet<int> m_set;
	}
}
