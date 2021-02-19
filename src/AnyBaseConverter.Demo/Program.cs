using System;
using Cryptography.Obfuscation;
using SmartQrCode.Helper;

namespace AnyBaseConverter.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "abcafdasfasfdasdfzxcvzxvzvczxvz";
            var aaa=BaseConverter.Convert(source, BaseConverter.BaseCharSet.Base62,
                BaseConverter.BaseCharSet.Base10);
            var bbb=BaseConverter.Convert(aaa, BaseConverter.BaseCharSet.Base10,
                BaseConverter.BaseCharSet.Base62);
            Console.WriteLine($"{source} - {aaa} - {bbb}");

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
    }
}
