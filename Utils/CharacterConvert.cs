using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils
{
    public static class CharacterConvert
    {
        public static string toSend(string text)
        {
            string res = "";
            res = text.Replace("%", "%25");
            res = res.Replace("&", "%26");
            res = res.Replace("(", "%28");
            res = res.Replace(")", "%29");
            res = res.Replace("+", "%2B");
            res = res.Replace(",", "%2C");
            res = res.Replace("/", "%2F");
            res = res.Replace(":", "%3A");
            res = res.Replace(";", "%3B");
            res = res.Replace("<", "%3C");
            res = res.Replace("=", "%3D");
            res = res.Replace(">", "%3E");
            res = res.Replace("?", "%3F");
            res = res.Replace("@", "%40");
            res = res.Replace("\\", "%5C");
            res = res.Replace("|", "%7C");
            return res;
        }
        public static string toLabel(string text)
        {
            string res = "";
            if(text.IndexOf("&")> -1)
            {
                res = text.Replace("&", "&&");
            }
            else
            {
                res = text;
            }
            return res;
        }
    }
}
