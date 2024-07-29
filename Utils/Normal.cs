using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils

{
    public class Normal
    {
        public static String GetDateTime()
        {
            String formatstr = "yyyyMMddHHmmss";
            return DateTime.Now.ToString(formatstr, DateTimeFormatInfo.InvariantInfo);
        }
    }
}
