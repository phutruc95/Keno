using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
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

        public static void SaveStream(Stream stream)
        {
            Image img = Image.FromStream(stream);
            img.Save(@"E:\rose1.jpeg");
        }
    }
}