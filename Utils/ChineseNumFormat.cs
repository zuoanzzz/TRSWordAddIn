using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils
{
    public class ChineseNumFormat
    {
        private static String[] NUMS = {"零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
        public static int onlyStringNumToInt(string num)
        {
            for (int i = 0; i < NUMS.Length; i++)
            {
                if (num.IndexOf(NUMS[i]) > -1)
                {
                    return chineseNumToNum(NUMS[i]);
                }
            }
            return 0;
        }
        public static int onlyStringNumToInt_l(string num)
        {
            switch (num)
            {
                case "第一":
                    return 1;
                case "第二":
                    return 2;
                case "第两":
                    return 2;
                case "第三":
                    return 3;
                case "第四":
                    return 4;
                case "第五":
                    return 5;
                case "第六":
                    return 6;
                case "第七":
                    return 7;
                case "第八":
                    return 8;
                case "第九":
                    return 9;
                case "第十":
                    return 10;
                case "第零":
                    return 0;
                default:
                    return 0;
            }
        }
        public static String onlyIntToStringNum(int num)
        {
            switch (num)
            {
                case 1:
                    return "一";
                case 2:
                    return "二";
                case 3:
                    return "三";
                case 4:
                    return "四";
                case 5:
                    return "五";
                case 6:
                    return "六";
                case 7:
                    return "七";
                case 8:
                    return "八";
                case 9:
                    return "九";
                case 10:
                    return "十";
                case 0:
                    return "零";
                default:
                    return num.ToString();
            }
        }
        public static String onlyIntToStringNum_l(int num)
        {
            switch (num)
            {
                case 1:
                    return "第一";
                case 2:
                    return "第二";
                case 3:
                    return "第三";
                case 4:
                    return "第四";
                case 5:
                    return "第五";
                case 6:
                    return "第六";
                case 7:
                    return "第七";
                case 8:
                    return "第八";
                case 9:
                    return "第九";
                case 10:
                    return "第十";
                case 0:
                    return "第零";
                default:
                    return num.ToString();
            }
        }
        private static int chineseNumToNum(String replaceNumber)
        {
            switch (replaceNumber)
            {
                case "一":
                    return 1;
                case "二":
                    return 2;
                case "两":
                    return 2;
                case "三":
                    return 3;
                case "四":
                    return 4;
                case "五":
                    return 5;
                case "六":
                    return 6;
                case "七":
                    return 7;
                case "八":
                    return 8;
                case "九":
                    return 9;
                case "十":
                    return 10;
                case "零":
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
