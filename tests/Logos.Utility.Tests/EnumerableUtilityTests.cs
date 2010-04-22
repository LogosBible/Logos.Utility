
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class EnumerableUtilityTests
	{
		[Test]
		public void EmptyIfNullNull()
		{
			CollectionAssert.AreEqual(new int[0], ((IEnumerable<int>) null).EmptyIfNull());
		}

		[Test]
		public void EmptyIfNullNonNull()
		{
			CollectionAssert.AreEqual(new[] { 1, 2 }, (new[] { 1, 2 }).EmptyIfNull());
		}

		const int NullInt = 1234567;

		[TestCase(new[] { 1, 2, 3, 4, 5 }, 15)]
		[TestCase(new[] { -1, -2, -3, -4, -5 }, -15)]
		[TestCase(new[] { 1, 2, 3, NullInt, 5 }, null)]
		[TestCase(new int[0], 0)]
		[TestCase(new[] { 1 }, 1)]
		[TestCase(new[] { NullInt }, null)]
		public void NullableSum(int[] values, int? expectedSum)
		{
			IEnumerable<int?> source = ToNullable(values, NullInt);

			Assert.That(source.Select(i => ToDecimal(i)).NullableSum(), Is.EqualTo(ToDecimal(expectedSum)));
			Assert.That(source.Select(i => ToDouble(i)).NullableSum(), Is.EqualTo(ToDouble(expectedSum)));
			Assert.That(source.Select(i => ToFloat(i)).NullableSum(), Is.EqualTo(ToFloat(expectedSum)));
			Assert.That(source.NullableSum(), Is.EqualTo(expectedSum));
			Assert.That(source.Select(i => ToLong(i)).NullableSum(), Is.EqualTo(ToLong(expectedSum)));
		}

		[Test]
		public void NullableSumNull()
		{
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<decimal?>) null).NullableSum());
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<double?>) null).NullableSum());
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<float?>) null).NullableSum());
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<int?>) null).NullableSum());
			Assert.Throws<ArgumentNullException>(() => ((IEnumerable<long?>) null).NullableSum());
		}

		[Test]
		public void NullableSumOverflow()
		{
			Assert.Throws<OverflowException>(() => (new decimal?[] { decimal.MaxValue, decimal.MaxValue }).NullableSum());
			Assert.Throws<OverflowException>(() => (new decimal?[] { decimal.MinValue, decimal.MinValue }).NullableSum());
			Assert.Throws<OverflowException>(() => (new int?[] { int.MaxValue, int.MaxValue }).NullableSum());
			Assert.Throws<OverflowException>(() => (new int?[] { int.MinValue, int.MinValue }).NullableSum());
			Assert.Throws<OverflowException>(() => (new long?[] { long.MaxValue, long.MaxValue }).NullableSum());
			Assert.Throws<OverflowException>(() => (new long?[] { long.MinValue, long.MinValue }).NullableSum());
		}

		[Test]
		public void NullableSumInfinity()
		{
			Assert.That((new double?[] { double.MaxValue, double.MaxValue }).NullableSum(), Is.EqualTo(double.PositiveInfinity));
			Assert.That((new double?[] { double.MinValue, double.MinValue }).NullableSum(), Is.EqualTo(double.NegativeInfinity));
			Assert.That((new float?[] { float.MaxValue, float.MaxValue }).NullableSum(), Is.EqualTo(float.PositiveInfinity));
			Assert.That((new float?[] { float.MinValue, float.MinValue }).NullableSum(), Is.EqualTo(float.NegativeInfinity));
		}

		[Test]
		public void WhereNotNullClassNullArgument()
		{
			IEnumerable<string> input = null;
			Assert.Throws<ArgumentNullException>(() => input.WhereNotNull());
		}

		[Test]
		public void WhereNotNullClass()
		{
			IEnumerable<string> input = new[] { null, "this", null, "is", "a", null, "test", null };
			CollectionAssert.AreEqual("this is a test".Split(' '), input.WhereNotNull());
		}

		[Test]
		public void WhereNotNullClassAllNull()
		{
			Assert.AreEqual(0, new string[] { null, null, null }.WhereNotNull().Count());
		}

		[Test]
		public void WhereNotNullStructNullArgument()
		{
			IEnumerable<int?> input = null;
			Assert.Throws<ArgumentNullException>(() => input.WhereNotNull());
		}

		[Test]
		public void WhereNotNullStruct()
		{
			IEnumerable<int?> input = new int?[] { null, 0, null, 1, 2, null, 3, null };
			CollectionAssert.AreEqual(Enumerable.Range(0, 4), input.WhereNotNull());
		}

		[Test]
		public void WhereNotNullStructAllNull()
		{
			Assert.AreEqual(0, new int?[] { null, null, null }.WhereNotNull().Count());
		}

		// Converts an array of non-nullable items to nullable items, replacing 'nullValue' with null.
		private static IEnumerable<T?> ToNullable<T>(T[] source, T nullValue)
			where T : struct
		{
			return source.Select(t => t.Equals(nullValue) ? default(T?) : t);
		}

		private static decimal? ToDecimal(int? value)
		{
			return value.HasValue ? (decimal?) value.Value : null;
		}

		private static double? ToDouble(int? value)
		{
			return value.HasValue ? (double?) value.Value : null;
		}

		private static float? ToFloat(int? value)
		{
			return value.HasValue ? (float?) value.Value : null;
		}

		private static long? ToLong(int? value)
		{
			return value.HasValue ? (long?) value.Value : null;
		}
	}
}
