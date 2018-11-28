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

		[Test]
		public void InvalidConstructorArgument()
		{
			Assert.Throws<ArgumentNullException>
                (() => new ReadOnlyHashSet<int>(null));
		}

		[Test]
		public void IsReadOnly()
		{
			Assert.IsTrue(m_set.IsReadOnly);
		}

		[Test]
		public void AddToCollection()
		{
			Assert.Throws<NotSupportedException>
                (() => ((ICollection<int>) m_set).Add(0));
		}

		[Test]
		public void Remove()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ICollection<int>) m_set).Remove(1));
		}

		[Test]
		public void Clear()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ICollection<int>) m_set).Clear());
		}

		[Test]
		public void AddToSet()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ISet<int>) m_set).Add(0));
		}

		[Test]
		public void ExceptWith()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ISet<int>) m_set).ExceptWith(new[] { 1 }));
		}

		[Test]
		public void IntersectWith()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ISet<int>) m_set).IntersectWith(new[] {1}));
		}

		[Test]
		public void SymmetricExceptWith()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ISet<int>) m_set).SymmetricExceptWith(new[] { 1 }));
		}

		[Test]
		public void UnionWith()
		{
            Assert.Throws<NotSupportedException>
                (() => ((ISet<int>) m_set).UnionWith(new[] { 0 }));
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
