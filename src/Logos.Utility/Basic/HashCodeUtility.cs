namespace Logos.Utility.Basic
{
	/// <summary>
	/// Provides methods for manipulating and creating hash codes.
	/// </summary>
	/// <remarks>
	/// <para>This code is based on Bob Jenkins' public domain <a href="http://burtleburtle.net/bob/c/lookup3.c">lookup3.c</a> code.</para>
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

		/// <summary>
		/// Gets a hash code for the specified <see cref="bool"/>; this hash code is guaranteed not to change in the future.
		/// </summary>
		/// <param name="value">The <see cref="bool"/> to hash.</param>
		/// <returns>A hash code for the specified <see cref="bool"/>.</returns>
		public static int GetPersistentHashCode(bool value)
		{
			// these values are the persistent hash codes for 0 and 1
			return value ? -1266253386 : 1800329511;
		}

		/// <summary>
		/// Gets a hash code for the specified <see cref="int"/>; this hash code is guaranteed not to change in the future.
		/// </summary>
		/// <param name="value">The <see cref="int"/> to hash.</param>
		/// <returns>A hash code for the specified <see cref="int"/>.</returns>
		/// <remarks>Based on <a href="http://www.concentric.net/~Ttwang/tech/inthash.htm">Robert Jenkins' 32 bit integer hash function</a>.</remarks>
		public static int GetPersistentHashCode(int value)
		{
			unchecked
			{
				uint hash = (uint) value;
				hash = (hash + 0x7ed55d16) + (hash << 12);
				hash = (hash ^ 0xc761c23c) ^ (hash >> 19);
				hash = (hash + 0x165667b1) + (hash << 5);
				hash = (hash + 0xd3a2646c) ^ (hash << 9);
				hash = (hash + 0xfd7046c5) + (hash << 3);
				hash = (hash ^ 0xb55a4f09) ^ (hash >> 16);
				return (int) hash;
			}
		}

		/// <summary>
		/// Gets a hash code for the specified <see cref="long"/>; this hash code is guaranteed not to change in the future.
		/// </summary>
		/// <param name="value">The <see cref="long"/> to hash.</param>
		/// <returns>A hash code for the specified <see cref="long"/>.</returns>
		/// <remarks>Based on <a href="http://www.concentric.net/~Ttwang/tech/inthash.htm">64 bit to 32 bit Hash Functions</a>.</remarks>
		public static int GetPersistentHashCode(long value)
		{
			unchecked
			{
				ulong hash = (ulong) value;
				hash = (~hash) + (hash << 18);
				hash = hash ^ (hash >> 31);
				hash = hash * 21;
				hash = hash ^ (hash >> 11);
				hash = hash + (hash << 6);
				hash = hash ^ (hash >> 22);
				return (int) hash;
			}
		}

		/// <summary>
		/// Gets a hash code for the specified <see cref="string"/>; this hash code is guaranteed not to change in the future.
		/// </summary>
		/// <param name="value">The <see cref="string"/> to hash.</param>
		/// <returns>A hash code for the specified <see cref="string"/>.</returns>
		/// <remarks>Based on <a href="http://www.azillionmonkeys.com/qed/hash.html">SuperFastHash</a>.</remarks>
		public static int GetPersistentHashCode(string value)
		{
			unchecked
			{
				// check for degenerate input
				if (string.IsNullOrEmpty(value))
					return 0;

				int length = value.Length;
				uint hash = (uint) length;

				int remainder = length & 1;
				length >>= 1;

				// main loop
				int index = 0;
				for (; length > 0; length--)
				{
					hash += value[index];
					uint temp = (uint) (value[index + 1] << 11) ^ hash;
					hash = (hash << 16) ^ temp;
					index += 2;
					hash += hash >> 11;
				}

				// handle odd string length
				if (remainder == 1)
				{
					hash += value[index];
					hash ^= hash << 11;
					hash += hash >> 17;
				}

				// force "avalanching" of final 127 bits
				hash ^= hash << 3;
				hash += hash >> 5;
				hash ^= hash << 4;
				hash += hash >> 17;
				hash ^= hash << 25;
				hash += hash >> 6;

				return (int) hash;
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
