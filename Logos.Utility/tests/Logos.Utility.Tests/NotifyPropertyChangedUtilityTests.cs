
using System.ComponentModel;
using Logos.Utility.ComponentModel;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class NotifyPropertyChangedUtilityTests
	{
		[TestCase("prop1", "prop1", true)]
		[TestCase("prop1", "prop2", false)]
		[TestCase(null, "prop1", true)]
		public void HasChanged(string eventPropertyName, string testPropertyName, bool expected)
		{
			PropertyChangedEventArgs args = new PropertyChangedEventArgs(eventPropertyName);
			Assert.That(args.HasChanged(testPropertyName), Is.EqualTo(expected));
		}
	}
}
