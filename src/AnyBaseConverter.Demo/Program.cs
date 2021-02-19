using System;
using System.Linq;
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
            var aaa=BaseConverter.Convert(source, BaseConverter.BaseCharSet.Base36,
                BaseConverter.BaseCharSet.Base66_Url_Safe_Custom);
            var bbb=BaseConverter.Convert(aaa, BaseConverter.BaseCharSet.Base66_Url_Safe_Custom,
                BaseConverter.BaseCharSet.Base36);
            Console.WriteLine($"{source} - {aaa} - {bbb}");
            */
            var aaa = BaseConverter.ToUrlSafe(129999999999999993);
            GenerateSampleData();
            GenerateSampleData2();
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
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
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
                var uid = BaseConverter.Convert(value, BaseConverter.BaseCharSet.Base36_Custom,
                    BaseConverter.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = BaseConverter.Convert(uid, BaseConverter.BaseCharSet.Base66_Url_Safe_Custom,
                    BaseConverter.BaseCharSet.Base36_Custom);
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

                string value = $"{sku}{i.ToString().PadLeft(7, '0')}";
                var hashCode = Math.Abs(value.GetHashCode());

                value = $"{hashCode.ToString().First()}{value}";

                var uid = BaseConverter.Convert(value, BaseConverter.BaseCharSet.Base36_Custom,
                    BaseConverter.BaseCharSet.Base66_Url_Safe_Custom);
                var uid2 = BaseConverter.Convert(uid, BaseConverter.BaseCharSet.Base66_Url_Safe_Custom,
                    BaseConverter.BaseCharSet.Base36_Custom);
                if (value != uid2)
                {
                    int x = 1;
                }

                sb.AppendLine($"{value},{uid},https://y.esquel.cn/b/_13{uid}");
            }

            string result = sb.ToString();
            int x1 = 1;
        }
    }
}
