using System;
using System.Text;

namespace Logos.Utility
{
    /// <summary>
    /// Converts between an Ascii85-encoded string and an escaped version that is always valid
    /// in an HTTP header. This class assumes that the "z" exception for 5 consecutive zero bytes
    /// is implemented, but the "y" exception for 5 consecutive space characters is not implemented,
    /// as is the case with the Ascii85 class.
    /// </summary>
    /// <remarks>See <a href="http://en.wikipedia.org/wiki/Ascii85">Ascii85 at Wikipedia</a> and
    /// <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec2.html#sec2.2">RFC 2616 section 2.2</a>.</remarks>
    public static class Ascii85HttpHeader
    {
        /// <summary>
        /// Encodes the specified Ascii85 string for safe passage in an HTTP header by
        /// substituting other characters for characters used by Ascii85 in the set to
        /// avoid the set of separator characters not allowed in HTTP headers, which is
        /// {()&lt;&gt;@,;:\"/[]?=}. The curly braces aren't used by Ascii85, but cannot be
        /// used in HTTP headers, either. Since there are 15 disallowed characters, and only
        /// 6 printable ASCII characters that are both allowed in HTTP headers and not used
        /// by Ascii85, one of those (~) is used as an escape character to introduce a
        /// two-character set to represent each of 10 of those disallowed sequences.
        /// 
        /// This function would normally be used to encode the results of Ascii85.Encode().
        ///  
        /// </summary>
        /// <param name="ascii85">A valid Ascii85 string.</param>
        /// <returns>A string that is valid as an HTTP header.</returns>
        public static string Encode(string ascii85)
        {
            if (ascii85 == null)
                throw new ArgumentNullException("ascii85");

            // Instantiate a StringBuilder with more than enough room to store the new string.
            StringBuilder sb = new StringBuilder(ascii85.Length * 2);

            // walk the characters
            foreach (char ch in ascii85)
            {
                switch (ch)
                {
                    case '(':
                        sb.Append('v');
                        break;
                    case ')':
                        sb.Append('w');
                        break;
                    case '<':
                        sb.Append('x');
                        break;
                    case '>':
                        sb.Append('y');
                        break;
                    case '@':
                        sb.Append('|');
                        break;
                    case ',':
                        sb.Append("~a");
                        break;
                    case ';':
                        sb.Append("~b");
                        break;
                    case ':':
                        sb.Append("~c");
                        break;
                    case '\\':
                        sb.Append("~d");
                        break;
                    case '"':
                        sb.Append("~e");
                        break;
                    case '/':
                        sb.Append("~f");
                        break;
                    case '[':
                        sb.Append("~g");
                        break;
                    case ']':
                        sb.Append("~h");
                        break;
                    case '?':
                        sb.Append("~i");
                        break;
                    case '=':
                        sb.Append("~j");
                        break;
                    case ' ':
                    case '\n':
                    case '\r':
                    case '\t':
                    case 'z':
                        // Pass white space and z through unchanged.
                        sb.Append(ch);
                        break;
                    default:
                        if ((ch < c_firstCharacter) || (ch > c_lastCharacter))
                            throw new FormatException("Invalid character '{0}' in Ascii85 string.".FormatInvariant(ch));
                        else
                            sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decodes the specified HTTP parameter string to an Ascii85 string.
        /// The results of this function would normally be fed to Ascii85.Decode().
        /// </summary>
        /// <param name="encoded">The HTTP parameter string</param>
        /// <returns>The Ascii85 string.</returns>
        public static string Decode(string encoded)
        {
            if (encoded == null)
                throw new ArgumentNullException("encoded");

            // The decoded string will not be longer than the encoded string.
            StringBuilder sb = new StringBuilder(encoded.Length);

            // Walk the input string.

            int i = 0;
            while (i < encoded.Length)
            {
                switch (encoded[i])
                {
                    case 'v':
                        sb.Append('(');
                        break;
                    case 'w':
                        sb.Append(')');
                        break;
                    case 'x':
                        sb.Append('<');
                        break;
                    case 'y':
                        sb.Append('>');
                        break;
                    case '|':
                        sb.Append('@');
                        break;
                    case ' ':
                    case '\n':
                    case '\r':
                    case '\t':
                        // Pass white space through unchanged.
                        sb.Append(encoded[i]);
                        break;
                    case '~':
                        i++;
                        if (i >= encoded.Length)
                            throw new FormatException("'~' cannot be the last character of the encoded string.");
                        switch (encoded[i])
                        {
                            case 'a':
                                sb.Append(',');
                                break;
                            case 'b':
                                sb.Append(';');
                                break;
                            case 'c':
                                sb.Append(':');
                                break;
                            case 'd':
                                sb.Append('\\');
                                break;
                            case 'e':
                                sb.Append('"');
                                break;
                            case 'f':
                                sb.Append('/');
                                break;
                            case 'g':
                                sb.Append('[');
                                break;
                            case 'h':
                                sb.Append(']');
                                break;
                            case 'i':
                                sb.Append('?');
                                break;
                            case 'j':
                                sb.Append('=');
                                break;
                            default:
                                throw new FormatException("Unexpected character following '~': '{0}'".FormatInvariant(encoded[i]));
                        }
                        break;
                    default:
                        sb.Append(encoded[i]);
                        break;
                }
                i++;
            }
            return sb.ToString();
        }

        // the first and last characters used in the Ascii85 encoding character set
        const char c_firstCharacter = '!';
        const char c_lastCharacter = 'u';
    }
}
