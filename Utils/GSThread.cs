using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRSWordAddIn.Utils
{
    
    public class GSThread
    {
        string chinese = "一二三四五六七八九";
        string chinese_liang = "第一第二第三第四第五第六第七第八第九";
        string arabic = "123456789";
        string character = ".,、，)）．是";
        //定义格式类型的种类,类型名称与序列标识
        ArrayList pList = new ArrayList();
        /// <summary>
        /// 判断段落开始的几个字符
        /// </summary>
        /// <param name="text"></param>
        public void setP(string pid,int start,string text)
        {
            for(int i = 0;i <= text.Length - 1;i++)
            {
                //0, 1, 2, 3 位置上是否存在用于序号的标点符号
                if(character.IndexOf(text[i]) > -1 && i <= 3)
                {
                    CheckParagraph one = new CheckParagraph();
                    one.p_start = 0;
                    //MessageBox.Show(start.ToString());
                    //存在标点符号,判断前面的字符是否是数字
                    string header = text.Substring(0, i);
                    string header2 = "";
                    if (text[i].ToString() == ")" || text[i].ToString() == "）")
                    {
                        header2 = header.Remove(0, 1);
                    }
                    if (header2 != "")
                    {
                        header = header2;
                        start += 1;
                        one.p_start = 1;
                    }
                    string type = getType(header, text[i].ToString());

                    
                    one.pid = pid;
                    one.start = start;
                    one.end = start + header.Length;
                    one.type = type;
                    one.oldValue = header;
                    one.text = text;
                   
                    int temp_a;
                    if(type.IndexOf("c") > -1)
                    {
                        one.NumberValue = ChineseNumFormat.onlyStringNumToInt(header);
                    }
                    else if (one.type.IndexOf("l") > -1)
                    {
                        one.NumberValue = ChineseNumFormat.onlyStringNumToInt_l(header);
                    }
                    else if(int.TryParse(header,out temp_a))
                    {
                        one.NumberValue = temp_a;
                    }else
                    {
                        break;
                    }
                    
                    pList.Add(one);
                    break;
                }

                else if (i >= 4)
                {
                    break;
                }
            }
            
        }
        public List<ErrInfo> CheckResult()
        {
            List<ErrInfo> ResList = new List<ErrInfo>();
            Dictionary<string, int> startlist = new Dictionary<string, int>();
            //开始验证正确性
            foreach (CheckParagraph one in pList)
            {
                if(startlist.ContainsKey(one.type) == true)
                {
                    if (one.NumberValue == 1)
                    {
                        startlist[one.type] = 1;
                    }
                    else
                    {
                        startlist[one.type] += 1;
                    }
                    
                }
                else{
                    startlist.Add(one.type, 1);
                }
                //MessageBox.Show(one.type +  "  " + one.NumberValue + "  " + startlist[one.type]);
                //检查序号是否正确
                if(one.NumberValue != startlist[one.type])
                {
                    //增加序号异常的错误
                    ErrInfo err = new ErrInfo();
                    err.pid = one.pid;
                    err.senStartPos = Convert.ToString(one.p_start);
                    err.senEndPos = Convert.ToString(one.p_start + one.oldValue.Length);
                    //err.totalStart = one.start;
                    //err.totalEnd = one.end;
                    err.startPos = Convert.ToString(one.p_start);
                    err.endPos = Convert.ToString(one.p_start + one.oldValue.Length);
                    err.totalStart = one.start;
                    err.totalEnd = one.end;
                    err.sentence = one.text;
                    err.senIdx = one.pid;
                    err.errorType = "100";
                    err.errorTypeInfo = "序号使用不当";
                    err.suggestions = null;
                    err.suggestType = "0";
                    err.engine = "cl";
                    
                    err.errorWord = one.oldValue;
                    
                    err.uuid = System.Guid.NewGuid().ToString("N");
                    err.alreadyChange = false;
                    err.AfterText = "";
                   

                    //正确词判断

                    if (one.type.IndexOf("c") > -1)
                    {
                        err.collateWord = ChineseNumFormat.onlyIntToStringNum(startlist[one.type]);
                    }
                    else if (one.type.IndexOf("l") > -1)
                    {
                        err.collateWord = ChineseNumFormat.onlyIntToStringNum_l(startlist[one.type]);
                    }
                    else
                    {
                        err.collateWord = startlist[one.type].ToString();
                    }

                    err.weight = "1.0";

                    ResList.Add(err);
                }
            }
            return ResList;
        }
        private string getType(string header,string fenge)
        {
            string type = "";
            if(chinese.IndexOf(header) > -1)
            {
                type += "c" + fenge;

            }
            else if(arabic.IndexOf(header) > -1)
            {
                type += "a" + fenge;
            }
            else if (chinese_liang.IndexOf(header) > -1)
            {
                type += "l" + fenge;
            }
            return type;
        }
    }
    public class CheckParagraph
    {
        public string type { get; set; }
        public string pid { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int p_start { get; set; }
        public int NumberValue { get; set; }
        public string oldValue { get; set; }
        public string suggestion { get; set; }
        public string text { get; set; }
    }
}
