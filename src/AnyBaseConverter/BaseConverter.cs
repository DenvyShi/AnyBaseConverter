using System;
using System.Linq;
using System.Numerics;

namespace AnyBaseConverter
{
    public static class BaseConverter
    {
        public class MyBase
        {
            // public static string Base10{return }
        }
        //url safe char: [0-9a-zA-Z]
        //-_.~
        //Don't change the sequence
        internal const string CHARSET65_URL_SAFE = "0O259cdghAT34BCD~EFGHIJKL678MPefQRSUuvwVpqrsW_.XYZab1Nijklmnotxyz-";
        private const string CHARSET10 = "0123456789";
        // private const string CHARSET32_NBD = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        private const string CHARSET32_NBD = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private const string CHARSET32_UNIQUEID = "04C57389AZ6BYDEFGH12IJKTLMNOPQRWSUVX";
        private const string CHARSET36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        internal const string CHARSET62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private const string CAMBIA95 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~_!$()+,;@.:=^*?&<>[]{}%#|`/\\ \"'-";
        private const string ASCII85 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#";
        private const string BASE64_RFC4648 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
        private const string BASE64_RFC4648_SAFE = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";

        private const string CHARSET95 =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~_!$()+,;@.:=^*?&<>[]{}%#|`/\\ \"'-";
        private const string CHARSET94_NBD =
            "5Y*pz~STUV=^ABCWX!$(NOP#|`/_6789qrstabcdQ?&<>[]{}%e01234fghijklmnoR\\\"'-uvwHIJKLMxy)+,;@.:DEFGZ";//The first char CAN NOT be 0
        public enum BaseCharSet
        {
            Base10 = 0,
            Base32_NBD = 1,
            Base36 = 2,
            Base62 = 3,
            Base65_Url_Safe=4,
            Base95=5,
            Base95_NBD=6,
            Base32_UNIQUEID=7,

        }

        public static string Convert(
            string input,
            BaseCharSet sourceAlphabet,
            BaseCharSet targetAlphabet)
        {
            string sourceCharset = GetCharsetOf(sourceAlphabet);
            string targetCharset = GetCharsetOf(targetAlphabet);
            var number = ToDec(sourceCharset, input);
            var targetString = ToBase(targetCharset, number);
            return targetString;
        }

        private static string GetCharsetOf(BaseCharSet baseCharSet)
        {
            string charset = "";
            switch (baseCharSet)
            {
                case BaseCharSet.Base10:
                    charset = CHARSET10;
                    break;
                case BaseCharSet.Base32_NBD:
                    charset = CHARSET32_NBD;
                    break;
                case BaseCharSet.Base36:
                    charset = CHARSET36;
                    break;
                case BaseCharSet.Base62:
                    charset = CHARSET62;
                    break;
                case BaseCharSet.Base65_Url_Safe:
                    charset = CHARSET65_URL_SAFE;
                    break;
                case BaseCharSet.Base95:
                    charset = CHARSET95;
                    break;
                case BaseCharSet.Base95_NBD:
                    charset = CHARSET94_NBD;
                    break;
                case BaseCharSet.Base32_UNIQUEID:
                    charset = CHARSET32_UNIQUEID;
                    break;
            }

            return charset;
        }

        public static string ToBase(BigInteger input, BaseCharSet targetBase)
        {
            string charset = GetCharsetOf(targetBase);
            return ToBase(charset, input);
        }
        /// <summary>
        ///     Converts an input string in base-n into the equivalent in base-ten based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>BigInteger base-ten value of given input</returns>
        private static BigInteger ToDec(Char[] charset, String input)
        {
            return ToDec(new String(charset), input);
        }

        /// <summary>
        ///     Converts an input string in base-n into the equivalent in base-ten based on the given charset
        /// </summary>
        /// <param name="input"></param>
        /// <param name="base"></param>
        /// <param name="charset"></param>
        /// <returns>BigInteger base-ten value of given input</returns>
        public static BigInteger ToDec(string input, BaseCharSet @base)
        {
            var basedCharset = GetCharsetOf(@base);
            return ToDec(basedCharset, input);

        }

        public static BigInteger Base32ToDec(string base32Input)
        {
            var basedCharset = GetCharsetOf(BaseCharSet.Base32_NBD);
            return ToDec(basedCharset, base32Input);

        }
        public static BigInteger ToDec(String charset, String input)
        {
            // validation
            if (null == charset)
            {
                throw new ArgumentException("Charset cannot be undefined", "charset");
            }
            if (1 > charset.Length)
            {
                throw new ArgumentException("Charset cannot be empty", "charset");
            }
            if (null == input)
            {
                throw new ArgumentException("Input cannot be undefined", "input");
            }
            if (input.ToList().Any(x => !charset.Contains(x)))
            {
                throw new ArgumentException("Input contains characters which are not defined in the charset", "input");
            }

            BigInteger nummericBase = charset.Length;
            BigInteger result = 0;
            Int32 inputLength = input.Length;
            for (Int32 i = 0; i < inputLength; i++)
            {
                Int32 valueOfChar = charset.IndexOf(input[i]);
                BigInteger pow = BigInteger.Pow(nummericBase, inputLength - i - 1);
                BigInteger mul = BigInteger.Multiply(valueOfChar, pow);
                result = BigInteger.Add(result, mul);
            }

            return result;
        }

        /// <summary>
        ///     Converts an input in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        private static String ToBase(Char[] charset, Int32 input)
        {
            return ToBase(new String(charset), new BigInteger(input));
        }

        /// <summary>
        ///     Converts an input in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        private static String ToBase(String charset, Int32 input)
        {
            return ToBase(charset, new BigInteger(input));
        }

        /// <summary>
        ///     Converts an input string in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        private static String ToBase(Char[] charset, String input)
        {
            return ToBase(new String(charset), BigInteger.Parse(input));
        }

        /// <summary>
        ///     Converts an input string in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        public static String ToBase(String charset, String input)
        {
            return ToBase(charset, BigInteger.Parse(input));
        }

        /// <summary>
        ///     Converts an input in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        public static String ToBase(Char[] charset, BigInteger input)
        {
            return ToBase(new String(charset), input);
        }

        /// <summary>
        ///     Converts an input in base-ten into the equivalent in base-n based on the given charset
        /// </summary>
        /// <param name="charset"></param>
        /// <param name="input"></param>
        /// <returns>String base-n value of given input</returns>
        public static String ToBase(String charset, BigInteger input)
        {
            if (null == charset)
            {
                throw new ArgumentException("Charset cannot be undefined", "charset");
            }
            if (1 > charset.Length)
            {
                throw new ArgumentException("Charset cannot be empty", "charset");
            }

            BigInteger nummericBase = charset.Length;
            String result = String.Empty;
            do
            {
                BigInteger div = BigInteger.Divide(input, nummericBase);
                BigInteger mul = BigInteger.Multiply(div, nummericBase);
                BigInteger reminder = BigInteger.Subtract(input, mul);
                result = charset[(Int32)reminder] + result;
                input = div;
            }
            while (0 < input);

            return result;
        }
    }
}