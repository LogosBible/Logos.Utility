
using System.Collections.Generic;
using NUnit.Framework;
using Logos.Utility.Collection;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class DictionaryUtilityTests
	{
		[Test]
		public void GetOrAddValueNew()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
            int i = DictionaryUtility.GetOrAddValue<string, int>(dict, "test"); 
			Assert.That(i, Is.EqualTo(0));
			Assert.That(dict["test"], Is.EqualTo(0));
		}

		[Test]
		public void GetOrAddValueCreator()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
            int i = DictionaryUtility.GetOrAddValue<string, int>(dict, "test",()=>1);
			Assert.That(i, Is.EqualTo(1));
			Assert.That(dict["test"], Is.EqualTo(1));
		}

		[Test]
		public void GetValueOrDefault()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
            int i = DictionaryUtility.GetValueOrDefault<string, int>(dict, "test");
			Assert.That(i, Is.EqualTo(0));
		}

		[Test]
		public void GetValueOrDefaultValue()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
            int i = DictionaryUtility.GetValueOrDefault<string, int>(dict, "test",1);
			Assert.That(i, Is.EqualTo(1));
		}

		[Test]
		public void GetValueOrDefaultValueCreator()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
            int i = DictionaryUtility.GetValueOrDefault<string, int>(dict, "test",()=>1);
			Assert.That(i, Is.EqualTo(1));
		}
	}
}
