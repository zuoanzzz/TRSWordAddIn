using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace TRSWordAddIn.Utils
{
    public class SpecThread
    {
        //string specword = "一是二是三是四是五是六是七是八是九是十是";
        string[] speclist_a = new string[] { "一是", "二是", "三是", "四是", "五是", "六是", "七是", "八是", "九是", "十是" };
        string[] speclist_b = new string[] { "(一)", "(二)", "(三)", "(四)", "(五)", "(六)", "(七)", "(八)", "(九)", "(十)" };
        string[] speclist_c = new string[] { "（一）", "（二）", "（三）", "（四）", "（五）", "（六）", "（七）", "（八）", "（九）", "（十）" };
        string[] speclist_d = new string[] { "(1)", "(2)", "(3)", "(4)", "(5)", "(6)", "(7)", "(8)", "(9)", "(10)" };
        string[] speclist_e = new string[] { "（1）", "（2）", "（3）", "（4）", "（5）", "（6）", "（7）", "（8）", "（9）", "（10）" };
        List<CheckParagraph> pList = new List<CheckParagraph>();

        List<List<string>> all_speclist = new List<List<string>>();

        public void init_speclist()
        {
            this.all_speclist.Clear();
            this.all_speclist.Add(speclist_a.ToList());
            this.all_speclist.Add(speclist_b.ToList());
            this.all_speclist.Add(speclist_c.ToList());
            this.all_speclist.Add(speclist_d.ToList());
            this.all_speclist.Add(speclist_e.ToList());
        }
        
        public void setP(string pid, int start, string text)
        {

            for (int i = 0; i < all_speclist.Count; ++i )
            {
                for (int j = 0; j < all_speclist[i].Count - 1; ++j)
                {
                    string a = all_speclist[i][j];

                    int find_post = 0;
                    string ta = text.Substring(find_post);
                    int find_index = ta.IndexOf(a);

                    if (j == 0 && find_index <= -1)
                    {
                        break;
                    }

                    while (find_index > -1)
                    {
                        CheckParagraph one = new CheckParagraph();
                        one.pid = pid;
                        one.p_start = find_index + find_post;
                        one.start = start + one.p_start;
                        one.end = one.start + a.Length;
                        //i_pid
                        one.type = i.ToString() + "_"  + pid;
                        one.oldValue = a;
                        one.NumberValue = j + 1;
                        one.text = text;
                        pList.Add(one);

                        
                        //MessageBox.Show(ta + "\n" + a);

                        find_post = one.p_start + a.Length;
                        ta = text.Substring(find_post);
                        find_index = ta.IndexOf(a);

                    }
                }
            }



                //for (int i = 0; i < speclist_a.Length - 1; i++)
                //{
                //    string a = speclist_a[i];

                //    int find_post = 0;
                //    string ta = text.Substring(find_post);
                //    int find_index = ta.IndexOf(a);

                //    if (i == 0 && find_index <= -1)
                //    {
                //        break;
                //    }

                //    while (find_index > -1)
                //    {
                //        CheckParagraph one = new CheckParagraph();
                //        one.pid = pid;
                //        one.p_start = find_index + find_post;
                //        one.start = start + one.p_start;
                //        one.end = one.start + 2;
                //        one.type = "a_" + pid;
                //        one.oldValue = a;
                //        one.NumberValue = i + 1;
                //        one.text = text;
                //        pList.Add(one);

                //        //MessageBox.Show(ta + "\n"+ a);

                //        find_post = one.p_start + 2;
                //        ta = text.Substring(find_post);
                //        find_index = ta.IndexOf(a);

                //    }
                //}            
        }
        public List<ErrInfo> CheckResult()
        {
            List<ErrInfo> ResList = new List<ErrInfo>();
            Dictionary<string, int> numlist = new Dictionary<string, int>();

            this.pList = this.pList.OrderBy(t => t.start).ToList();

            //开始验证正确性
            foreach (CheckParagraph one in pList)
            {
                if (numlist.ContainsKey(one.type) == true)
                {
                    //if (one.NumberValue == 1)
                    //{
                    //    numlist[one.type] = 1;
                    //}
                    //else
                    //{
                    //    numlist[one.type] += 1;
                    //}
                    numlist[one.type] += 1;

                }
                else
                {
                    numlist.Add(one.type, 1);
                }

                //MessageBox.Show(one.type +  "  " + one.NumberValue + "  " + startlist[one.type]);
                //检查序号是否正确
                if (one.NumberValue != numlist[one.type])
                {
                    //增加序号异常的错误
                    ErrInfo err = new ErrInfo();
                    err.pid = one.pid;
                    err.senStartPos = Convert.ToString(one.p_start);
                    err.senEndPos = Convert.ToString(one.p_start + one.oldValue.Length);
                    //err.totalStart = one.start;
                    //err.totalEnd = one.end;
                    err.startPos = err.senStartPos;
                    err.endPos = err.senEndPos;
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
                    err.collateWord = "序号使用不当";

                    try
                    {
                        List<string> type_list = one.type.Split('_').ToList();
                        //MessageBox.Show(err.errorWord + "\n" + one.type + "\n" + numlist[one.type].ToString());
                        err.collateWord = this.all_speclist[int.Parse(type_list[0])][numlist[one.type] - 1];
                        
                    }
                    catch (Exception ex)
                    {
                        
                    }

                    //if (one.type.IndexOf("a") > -1)
                    //{
                    //    err.collateWord = ChineseNumFormat.onlyIntToStringNum(numlist[one.type]) + "是";
                    //}
                    //else if (one.type.IndexOf("b") > -1)
                    //{
                    //    err.collateWord = ChineseNumFormat.onlyIntToStringNum_l(numlist[one.type]) + "要";
                    //}
                    //else
                    //{
                    //    err.collateWord = "序号使用不当";
                    //}

                    err.weight = "1.0";

                    ResList.Add(err);
                }


            }

            return ResList;
        }
    }
}
