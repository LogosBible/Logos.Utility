
using System.Collections.Generic;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class DictionaryUtilityTests
	{
		[Test]
		public void GetOrAddValueNew()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			int i = dict.GetOrAddValue("test");
			Assert.That(i, Is.EqualTo(0));
			Assert.That(dict["test"], Is.EqualTo(0));
		}

		[Test]
		public void GetOrAddValueCreator()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			int i = dict.GetOrAddValue("test", () => 1);
			Assert.That(i, Is.EqualTo(1));
			Assert.That(dict["test"], Is.EqualTo(1));
		}

		[Test]
		public void GetValueOrDefault()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			int i = dict.GetValueOrDefault("test");
			Assert.That(i, Is.EqualTo(0));
		}

		[Test]
		public void GetValueOrDefaultValue()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			int i = dict.GetValueOrDefault("test", 1);
			Assert.That(i, Is.EqualTo(1));
		}

		[Test]
		public void GetValueOrDefaultValueCreator()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			int i = dict.GetValueOrDefault("test", () => 1);
			Assert.That(i, Is.EqualTo(1));
		}
	}
}
