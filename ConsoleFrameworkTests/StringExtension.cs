using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrameworkTests
{
    public static class StringExtension
    {
        static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();

        static Random random = new Random();

        public static string GetRandomString(this string value,int size)
        {
            var result = new string(
                Enumerable.Repeat(chars, size)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

    }
}
