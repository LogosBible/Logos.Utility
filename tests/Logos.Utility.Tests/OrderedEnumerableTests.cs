
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class OrderedEnumerableTests
	{
		[Test]
		public void SortFirstAscending()
		{
			Random random = new Random(1);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderBy(x => x.First);
			var sorted2 = data.LazyOrderBy(x => x.First);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstAscendingSecondAscending()
		{
			Random random = new Random(2);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderBy(x => x.First).ThenBy(x => x.Second);
			var sorted2 = data.LazyOrderBy(x => x.First).ThenBy(x => x.Second);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstAscendingSecondDescending()
		{
			Random random = new Random(3);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderBy(x => x.First).ThenByDescending(x => x.Second);
			var sorted2 = data.LazyOrderBy(x => x.First).ThenByDescending(x => x.Second);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstDescending()
		{
			Random random = new Random(4);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderByDescending(x => x.First);
			var sorted2 = data.LazyOrderByDescending(x => x.First);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstDescendingSecondAscending()
		{
			Random random = new Random(5);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderByDescending(x => x.First).ThenBy(x => x.Second);
			var sorted2 = data.LazyOrderByDescending(x => x.First).ThenBy(x => x.Second);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstDescendingSecondDescending()
		{
			Random random = new Random(6);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(100), random.Next(100), random.Next());

			var sorted1 = data.OrderByDescending(x => x.First).ThenByDescending(x => x.Second);
			var sorted2 = data.LazyOrderByDescending(x => x.First).ThenByDescending(x => x.Second);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstAscendingCustom()
		{
			Random random = new Random(7);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(), random.Next(100), random.Next(100));

			var sorted1 = data.OrderBy(x => x, new TripleComparer());
			var sorted2 = data.LazyOrderBy(x => x, new TripleComparer());
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void SortFirstDescendingCustom()
		{
			Random random = new Random(8);

			var data = new Triple[100000];
			for (int index = 0; index < data.Length; index++)
				data[index] = new Triple(random.Next(), random.Next(100), random.Next(100));

			var sorted1 = data.OrderByDescending(x => x, new TripleComparer());
			var sorted2 = data.LazyOrderByDescending(x => x, new TripleComparer());
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void AscendingNullArguments()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>) null).OrderBy(x => x));
			Assert.Throws<ArgumentNullException>(() => Enumerable.Range(0, 10).OrderBy((Func<int, int>) null));
		}

		[Test]
		public void DescendingNullArguments()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int>) null).OrderByDescending(x => x));
			Assert.Throws<ArgumentNullException>(() => Enumerable.Range(0, 10).OrderByDescending((Func<int, int>) null));
		}

		[Test]
		public void DefaultComparerAscending()
		{
			Random random = new Random(9);
			int[] numbers = new int[100000];
			for (int index = 0; index < numbers.Length; index++)
				numbers[index] = random.Next();

			var sorted1 = numbers.OrderBy(x => x, null);
			var sorted2 = numbers.LazyOrderBy(x => x, null);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		[Test]
		public void DefaultComparerDescending()
		{
			Random random = new Random(9);
			int[] numbers = new int[100000];
			for (int index = 0; index < numbers.Length; index++)
				numbers[index] = random.Next();

			var sorted1 = numbers.OrderByDescending(x => x, null);
			var sorted2 = numbers.LazyOrderByDescending(x => x, null);
			CollectionAssert.AreEqual(sorted1, sorted2);
		}

		// The Triple struct can be used to verify that the sort is stable by sorting by the First (and Second)
		// properties, then using the Third property to verify that objects are in the right order.
		[DebuggerDisplay("F={m_first} S={m_second} T={m_third}")]
		private struct Triple : IEquatable<Triple>
		{
			public Triple(int first, int second, int third)
			{
				m_first = first;
				m_second = second;
				m_third = third;
			}

			public int First
			{
				get { return m_first; }
			}

			public int Second
			{
				get { return m_second; }
			}

			public int Third
			{
				get { return m_third; }
			}

			public override bool Equals(object obj)
			{
				return obj is Triple && Equals((Triple) obj);
			}

			public bool Equals(Triple other)
			{
				return m_first == other.m_first && m_second == other.m_second && m_third == other.m_third;
			}

			public override int GetHashCode()
			{
				return HashCodeUtility.CombineHashCodes(m_first, m_second, m_third);
			}

			public static bool operator==(Triple left, Triple right)
			{
				return left.Equals(right);
			}

			public static bool operator!=(Triple left, Triple right)
			{
				return !left.Equals(right);
			}

			readonly int m_first;
			readonly int m_second;
			readonly int m_third;
		}

		private class TripleComparer : IComparer<Triple>
		{
			public int Compare(Triple left, Triple right)
			{
				// sort by second, then by third descending
				int result = left.Second - right.Second;
				if (result == 0)
					result = right.Third - left.Third;
				return result;
			}
		}
	}
}
