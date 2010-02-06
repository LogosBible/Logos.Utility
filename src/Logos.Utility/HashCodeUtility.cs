
namespace Logos.Utility
{
	/// <summary>
	/// Provides methods for manipulating and creating hash codes.
	/// </summary>
	/// <remarks>
	/// <para>This code is based on Bob Jenkins' public domain<a href="http://burtleburtle.net/bob/c/lookup3.c">lookup3.c</a> code.</para>
	/// <para>This work is hereby released into the Public Domain. To view a copy of the public domain dedication,
	/// visit http://creativecommons.org/licenses/publicdomain/ or send a letter to
	/// Creative Commons, 171 Second Street, Suite 300, San Francisco, California, 94105, USA.</para>
	/// </remarks>
	public static class HashCodeUtility
	{
		/// <summary>
		/// Combines the specified hash codes.
		/// </summary>
		/// <param name="hashCode1">The first hash code.</param>
		/// <returns>The combined hash code.</returns>
		/// <remarks>This is a specialization of <see cref="CombineHashCodes(int[])"/> for efficiency.</remarks>
		public static int CombineHashCodes(int hashCode1)
		{
			unchecked
			{
				uint a = 0xdeadbeef + 4;
				uint b = a;
				uint c = a;

				a += (uint) hashCode1;
				FinalizeHash(ref a, ref b, ref c);

				return (int) c;
			}
		}

		/// <summary>
		/// Combines the specified hash codes.
		/// </summary>
		/// <param name="hashCode1">The first hash code.</param>
		/// <param name="hashCode2">The second hash code.</param>
		/// <returns>The combined hash code.</returns>
		/// <remarks>This is a specialization of <see cref="CombineHashCodes(int[])"/> for efficiency.</remarks>
		public static int CombineHashCodes(int hashCode1, int hashCode2)
		{
			unchecked
			{
				uint a = 0xdeadbeef + 8;
				uint b = a;
				uint c = a;

				a += (uint) hashCode1;
				b += (uint) hashCode2;
				FinalizeHash(ref a, ref b, ref c);

				return (int) c;
			}
		}

		/// <summary>
		/// Combines the specified hash codes.
		/// </summary>
		/// <param name="hashCode1">The first hash code.</param>
		/// <param name="hashCode2">The second hash code.</param>
		/// <param name="hashCode3">The third hash code.</param>
		/// <returns>The combined hash code.</returns>
		/// <remarks>This is a specialization of <see cref="CombineHashCodes(int[])"/> for efficiency.</remarks>
		public static int CombineHashCodes(int hashCode1, int hashCode2, int hashCode3)
		{
			unchecked
			{
				uint a = 0xdeadbeef + 12;
				uint b = a;
				uint c = a;

				a += (uint) hashCode1;
				b += (uint) hashCode2;
				c += (uint) hashCode3;
				FinalizeHash(ref a, ref b, ref c);

				return (int) c;
			}
		}

		/// <summary>
		/// Combines the specified hash codes.
		/// </summary>
		/// <param name="hashCode1">The first hash code.</param>
		/// <param name="hashCode2">The second hash code.</param>
		/// <param name="hashCode3">The third hash code.</param>
		/// <param name="hashCode4">The fourth hash code.</param>
		/// <returns>The combined hash code.</returns>
		/// <remarks>This is a specialization of <see cref="CombineHashCodes(int[])"/> for efficiency.</remarks>
		public static int CombineHashCodes(int hashCode1, int hashCode2, int hashCode3, int hashCode4)
		{
			unchecked
			{
				uint a = 0xdeadbeef + 16;
				uint b = a;
				uint c = a;

				a += (uint) hashCode1;
				b += (uint) hashCode2;
				c += (uint) hashCode3;
				MixHash(ref a, ref b, ref c);

				a += (uint) hashCode4;
				FinalizeHash(ref a, ref b, ref c);

				return (int) c;
			}
		}

		/// <summary>
		/// Combines the specified hash codes.
		/// </summary>
		/// <param name="hashCodes">An array of hash codes.</param>
		/// <returns>The combined hash code.</returns>
		/// <remarks>This method is based on the "hashword" function at http://burtleburtle.net/bob/c/lookup3.c. It attempts to thoroughly
		/// mix all the bits in the input hash codes.</remarks>
		public static int CombineHashCodes(params int[] hashCodes)
		{
			unchecked
			{
				// check for null
				if (hashCodes == null)
					return 0x0d608219;

				int length = hashCodes.Length;

				uint a = 0xdeadbeef + (((uint) length) << 2);
				uint b = a;
				uint c = a;

				int index = 0;
				while (length - index > 3)
				{
					a += (uint) hashCodes[index];
					b += (uint) hashCodes[index + 1];
					c += (uint) hashCodes[index + 2];
					MixHash(ref a, ref b, ref c);
					index += 3;
				}

				if (length - index > 2)
					c += (uint) hashCodes[index + 2];
				if (length - index > 1)
					b += (uint) hashCodes[index + 1];

				if (length - index > 0)
				{
					a += (uint) hashCodes[index];
					FinalizeHash(ref a, ref b, ref c);
				}

				return (int) c;
			}
		}

		// The "rot()" macro from http://burtleburtle.net/bob/c/lookup3.c
		private static uint Rotate(uint x, int k)
		{
			return (x << k) | (x >> (32 - k));
		}

		// The "mix()" macro from http://burtleburtle.net/bob/c/lookup3.c
		private static void MixHash(ref uint a, ref uint b, ref uint c)
		{
			unchecked
			{
				a -= c; a ^= Rotate(c, 4); c += b;
				b -= a; b ^= Rotate(a, 6); a += c;
				c -= b; c ^= Rotate(b, 8); b += a;
				a -= c; a ^= Rotate(c, 16); c += b;
				b -= a; b ^= Rotate(a, 19); a += c;
				c -= b; c ^= Rotate(b, 4); b += a;
			}
		}

		// The "final()" macro from http://burtleburtle.net/bob/c/lookup3.c
		private static void FinalizeHash(ref uint a, ref uint b, ref uint c)
		{
			unchecked
			{
				c ^= b; c -= Rotate(b, 14);
				a ^= c; a -= Rotate(c, 11);
				b ^= a; b -= Rotate(a, 25);
				c ^= b; c -= Rotate(b, 16);
				a ^= c; a -= Rotate(c, 4);
				b ^= a; b -= Rotate(a, 14);
				c ^= b; c -= Rotate(b, 24);
			}
		}
	}
}
