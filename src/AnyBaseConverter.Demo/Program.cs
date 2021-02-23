using System;
using System.Linq;
using System.Numerics;
using System.Text;
using Cryptography.Obfuscation;
using Cryptography.Obfuscation.Extensions;

namespace AnyBaseConverter.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string source = "532030012410U9999999".ToUpper();
            int hashCode = source.GetStableHashCode();
            source = $"{source}-{hashCode}";
            var aaa=AnyBaseConvert.Convert(source, AnyBaseConvert.BaseCharSet.Base36,
                AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
            var bbb=AnyBaseConvert.Convert(aaa, AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom,
                AnyBaseConvert.BaseCharSet.Base36);
            Console.WriteLine($"{source} - {aaa} - {bbb}");
            */
            var aaa = AnyBaseConvert.ToUrlSafe(BigInteger.Parse("9999999999999999999999999999999999999999"));
            GenerateSampleData();
            GenerateSampleData2();
            Test3();
            /*Obfuscator dd = new Obfuscator();
            var value = Int32.MaxValue;
            Random random = new Random();
            for (int i = 0; i < 20; i++)
            {
                value = random.Next(0, Int32.MaxValue);
                var a = dd.Obfuscate(value);
                var b = dd.Deobfuscate(a);
                Console.WriteLine($"{i} - {value}-{a}-{b}");
            }*/
            var aaxa  =AnyBaseConvert.Convert("~~", AnyBaseConvert.BaseCharSet.Base66_Url_Safe, AnyBaseConvert.BaseCharSet.Base10);
            var aax2a  =AnyBaseConvert.Convert("zz", AnyBaseConvert.BaseCharSet.Base62, AnyBaseConvert.BaseCharSet.Base10);
            StringBuilder sbbase10 = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sbbase10.AppendLine(AnyBaseConvert.Convert(i.ToString(), AnyBaseConvert.BaseCharSet.Base10,
                    AnyBaseConvert.BaseCharSet.Base62));
            }

            string testString = "rHFjOe~P7F2vTG5f-I";
            string base36String = AnyBaseConvert.Convert(testString,
                AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom, AnyBaseConvert.BaseCharSet.Base36_Custom);
            string rrr = sbbase10.ToString();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        private static void Test3()
        {

            // string sku = "522030008130R";
            string sku = "IIIIIIIIIIIIII";//Max base 36
            long seq = 999999999999;
            
            string maxUniqueId = $"{sku}{seq}";

            string maxSku = "ZZZZZZZZZZZZZZ";
            string maxSkuBase16 = AnyBaseConvert.Convert(maxSku, AnyBaseConvert.BaseCharSet.Base36,
                AnyBaseConvert.BaseCharSet.Base16);

            var uid = AnyBaseConvert.Convert(maxUniqueId, AnyBaseConvert.BaseCharSet.Base36_Custom,
                AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
            string maxSeqBase10 = "999999999999999";
            string maxSeqBase16 = AnyBaseConvert.Convert(maxSeqBase10, AnyBaseConvert.BaseCharSet.Base10,
                AnyBaseConvert.BaseCharSet.Base16);
            int aa = 1;
        }

        private static void GenerateSampleData2()
        {

            string sku = "522030008130R";

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {
                string value = $"{sku}{i.ToString().PadLeft(7, '0')}";
                var hashCode = Math.Abs(value.GetHashCode());
               
                value = $"{hashCode.ToString().First()}{value}";
                var uid = AnyBaseConvert.Convert(value, AnyBaseConvert.BaseCharSet.Base36_Custom,
                    AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = AnyBaseConvert.Convert(uid, AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom,
                    AnyBaseConvert.BaseCharSet.Base36_Custom);
                if (value != uid2)
                {
                    int x = 1;
                }

                sb.AppendLine($"{value},{uid},https://y.esquel.cn/a/{uid}");
            }

            string result = sb.ToString();
        }
   
        private static void GenerateSampleData()
        {

            string sku = "522030008130R";
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 100; i++)
            {

                string humanReadable = $"{sku}{i.ToString().PadLeft(7, '0')}";
                var hashCode = Math.Abs(humanReadable.GetHashCode());

                var randomedHumanReadable = $"{hashCode.ToString().First()}{humanReadable}";

                var uid = AnyBaseConvert.Convert(randomedHumanReadable, AnyBaseConvert.BaseCharSet.Base36_Custom,
                    AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = AnyBaseConvert.Convert(uid, AnyBaseConvert.BaseCharSet.Base66_Url_Safe_Custom,
                    AnyBaseConvert.BaseCharSet.Base36_Custom);
                if (randomedHumanReadable != uid2)
                {
                    int x = 1;
                }

                sb.AppendLine($"{humanReadable},{uid},https://y.esquel.cn/b/_13{uid}");
            }

            string result = sb.ToString();
            int x1 = 1;
        }
    }
}
