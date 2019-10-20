using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Keno.ViewModel
{
    public class Breadcrumb
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public Breadcrumb(string url, string name)
        {
            Url = url;
            Name = name;
        }
        public static string Render(List<Breadcrumb> lst)
        {
            if (lst == null || lst.Count == 0) return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"breadcrumb\">");
            for (int i = 0; i < lst.Count; i++ )
            {
                sb.Append("<li>");
                if (i == 0) sb.Append("<i class=\"ace-icon fa fa-home home-icon\"></i>");
                if(i == lst.Count - 1)
                {
                    sb.Append(lst[i].Name);
                }
                else
                {
                   sb.AppendFormat("<a href=\"{0}\">{1}</a>", lst[i].Url, lst[i].Name);
                   sb.Append("</li>");
                }
            }
            sb.Append("</ul>");

            return sb.ToString();
        }
    }
}