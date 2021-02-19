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
        private const string CHARSET_Base66_URL_SAFE_Custom = "0O259cdghAT34BCD~EFGHIJKL678MPefQRSUuvwVpqrsW_.XYZab1Nijklmnotxyz-";
        private const string CHARSET_Base66_URL_SAFE = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-._~";
        private const string CHARSET_Base10 = "0123456789";
        // private const string CHARSET32_NBD = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        private const string CHARSET_Base32_CustomV1 = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
        private const string CHARSET_Base32_CustomV2 = "04C57389AZ6BYDEFGH12IJKTLMNOPQRWSUVX";
        private const string CHARSET_Base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string CHARSET_Base36_Custom = "045GH7RSTUVWXJZ89AB6QYCDEFK123LMNOPI";
        internal const string CHARSET_Base62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        // private const string CAMBIA95 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~_!$()+,;@.:=^*?&<>[]{}%#|`/\\ \"'-";
        private const string CHARSET_Base85 = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?&<>()[]{}@%$#";
        private const string BASE64_RFC4648 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz+/";
        private const string BASE64_RFC4648_SAFE = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_";

        private const string CHARSET_Base95 =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz~_!$()+,;@.:=^*?&<>[]{}%#|`/\\ \"'-";
        private const string CHARSET_Base94_Custom =
            "5Y*pz~STUV=^ABCWX!$(NOP#|`/_6789qrstabcdQ?&<>[]{}%e01234fghijklmnoR\\\"'-uvwHIJKLMxy)+,;@.:DEFGZ";//The first char CAN NOT be 0
        public enum BaseCharSet
        {
            Base10 ,
            Base32_CustomV1,
            Base36,
            Base36_Custom,
            Base62,
            Base66_Url_Safe_Custom,
            Base95,
            Base94_Custom,
            Base32_CustomV2,

            Base85,
            Base64,
            Base66_Url_Safe,
            Base64_Safe
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
                    charset = CHARSET_Base10;
                    break;
                case BaseCharSet.Base32_CustomV1:
                    charset = CHARSET_Base32_CustomV1;
                    break;
                case BaseCharSet.Base36:
                    charset = CHARSET_Base36;
                    break;
                case BaseCharSet.Base36_Custom:
                    charset = CHARSET_Base36_Custom;
                    break;
                case BaseCharSet.Base62:
                    charset = CHARSET_Base62;
                    break;
                case BaseCharSet.Base64:
                    charset = BASE64_RFC4648;
                    break;
                case BaseCharSet.Base64_Safe:
                    charset = BASE64_RFC4648_SAFE;
                    break;
                case BaseCharSet.Base66_Url_Safe_Custom:
                    charset = CHARSET_Base66_URL_SAFE_Custom;
                    break;
                case BaseCharSet.Base66_Url_Safe:
                    charset = CHARSET_Base66_URL_SAFE;
                    break;
                case BaseCharSet.Base85:
                    charset = CHARSET_Base85;
                    break;
                case BaseCharSet.Base95:
                    charset = CHARSET_Base95;
                    break;
                case BaseCharSet.Base94_Custom:
                    charset = CHARSET_Base94_Custom;
                    break;
                case BaseCharSet.Base32_CustomV2:
                    charset = CHARSET_Base32_CustomV2;
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
            var basedCharset = GetCharsetOf(BaseCharSet.Base32_CustomV1);
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