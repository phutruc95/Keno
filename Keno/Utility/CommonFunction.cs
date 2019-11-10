using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Keno.Utility
{
    public class CommonFunction
    {
        public static string ShortenString(string str, int length)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            string ellipsis = string.Empty;

            if (str.Length < length) length = str.Length;
            else ellipsis = "...";

            str = str.Substring(0, length) + ellipsis;
            return str;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsOverdued(DateTime OverduedDate)
        {
            return (OverduedDate > DateTime.Now);
        }
    }
}