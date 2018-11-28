using System;
using NUnit.Framework;

namespace Logos.Utility.Tests
{
	[TestFixture]
	public class Ascii85HttpHeaderTests
	{
		[Test]
		public void WikipediaSample()
		{
			const string c_ascii85 = @"9jqo^BlbD-BleB1DJ+*+F(f,q/0JhKF<GL>Cj@.4Gp$d7F!,L7@<6@)/0JDEF<G%<+EV:2F!,O<DJ+*.@<*K0@<6L(Df-\0Ec5e;DffZ(EZee.Bl.9pF""AGXBPCsi+DGm>@3BB/F*&OCAfu2/AKYi(DIb:@FD,*)+C]U=@3BN#EcYf8ATD3s@q?d$AftVqCh[NqF<G:8+EV:.+Cf>-FD5W8ARlolDIal(DId<j@<?3r@:F%a+D58'ATD4$Bl@l3De:,-DJs`8ARoFb/0JMK@qB4^F!,R<AKZ&-DfTqBG%G>uD.RTpAKYo'+CT/5+Cei#DII?(E,9)oF*2M7/c";
			const string c_separators = "()<>;@,;:\\\"/[]?=";

			Assert.That(Ascii85HttpHeader.Decode(Ascii85HttpHeader.Encode(c_ascii85)), Is.EqualTo(c_ascii85));
			Assert.That(Ascii85HttpHeader.Decode(Ascii85HttpHeader.Encode(c_separators)), Is.EqualTo(c_separators));
		}

		[TestCase("", "")]
		[TestCase("(", "v")]
		[TestCase(")", "w")]
		[TestCase("<", "x")]
		[TestCase(">", "y")]
		[TestCase("@", "|")]
		[TestCase(",", "~a")]
		[TestCase(";", "~b")]
		[TestCase(":", "~c")]
		[TestCase("\\", "~d")]
		[TestCase("\"", "~e")]
		[TestCase("/", "~f")]
		[TestCase("[", "~g")]
		[TestCase("]", "~h")]
		[TestCase("?", "~i")]
		[TestCase("=", "~j")]
		[TestCase(" \r\n\t!01234567890ABCabcz-*", " \r\n\t!01234567890ABCabcz-*")]
		public void RoundTrip(string plain, string encoded)
		{
			Assert.That(Ascii85HttpHeader.Encode(plain), Is.EqualTo(encoded));
			Assert.That(Ascii85HttpHeader.Decode(encoded), Is.EqualTo(plain));
		}

		[Test]
		public void EncodeNull()
		{
            Assert.Throws<ArgumentNullException>(() => Ascii85HttpHeader.Encode(null));
		}

		[Test]
		public void DecodeNull()
		{
            Assert.Throws<ArgumentNullException>(() => Ascii85HttpHeader.Decode(null));
		}

		[Test]
		public void TooLow()
		{
            Assert.Throws<FormatException>(() => Ascii85HttpHeader.Encode("\v"));
		}

		[Test]
		public void TooHigh()
		{
            Assert.Throws<FormatException>(() => Ascii85HttpHeader.Encode("€"));
		}

		[Test]
		public void MisplacedTilde()
		{
			Assert.Throws<FormatException>(() => Ascii85.Decode("a~z"));
		}

		[Test]
		public void FinalTilde()
		{
            Assert.Throws<FormatException>(() => Ascii85.Decode("~"));
		}

	}
}
