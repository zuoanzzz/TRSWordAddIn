using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TRSWordAddIn.Utils;

namespace TRSWordAddIn
{
    public partial class Form_Result : Form
    {
        BaseRibbon baseRibbon;
        int selectuuid = -1;

        string error_type = "";
        string error_word = "";
        string suggestion_word = "";
        string collate_word = "";
        int suggestion_type = 1;

        string tip_keyword = "修改提示";
        string collate_keyword = "修改意见";

        public List<ErrInfo> List = new List<ErrInfo>();
        int difurl = 0;
        public Form_Result(BaseRibbon br, int select_id)
        {
            InitializeComponent();
            this.baseRibbon = br;
            this.selectuuid = select_id;

            //显示当前的
            this.init();

        }
        public void parse_comment(string str)
        {

            this.baseRibbon.Log("str:" + str.ToString());
            List<string> split_list = str.Split('●').ToList();
            this.baseRibbon.Log("split_list.Count:" + split_list.Count.ToString());
            if (split_list.Count == 4)
            {
                this.baseRibbon.Log("split_list[3]:" + split_list[3].ToString());
                List<string> split3_list = split_list[3].Split(':').ToList();
                if ( split3_list.Count == 2)
                {
                    if (split3_list[0] == this.tip_keyword)
                    {
                        this.suggestion_word = split3_list[1];
                        this.suggestion_type = 1;
                        this.collate_word = "";
                       
                    }
                    else if (split3_list[0] == this.collate_keyword)
                    {
                        this.suggestion_word = split3_list[1];
                        List<string> split3_word_list = split3_list[1].Split(';').ToList();
                        //多个结果
                        if (split3_word_list.Count >= 1)
                        {
                            //推荐权值
                            List<string> split3_word2_list = split3_word_list[0].Split(',').ToList();
                            if (split3_word2_list.Count >= 1)
                            {
                                this.suggestion_type = 0;
                                this.collate_word = split3_word2_list[0];
                       
                            }
                        }
                    }
                    
                }

            }

        }

        public void set_label()
        {
            this.label_errortext.Text = this.error_word;
            this.label_type.Text = this.error_type;

            this.textBox1.Text = this.suggestion_word;
            this.textBox_suggest.Text = this.collate_word;
        }

        public void clear_lable()
        {
            this.error_type = "";
            this.error_word = "";

            this.suggestion_word = "";
            this.collate_word = "";
            this.suggestion_type = 1;
        }

        private void init()
        {

            Document doc = this.baseRibbon.m_app.ActiveDocument;

            int Comment_count = doc.Comments.Count;
            if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
            {
                this.error_type = doc.Comments[this.selectuuid].Author.ToString();
                this.error_word = doc.Comments[this.selectuuid].Scope.Text;


                string comment_str = "";
                if (doc.Comments[this.selectuuid].Range.Text != null)
                {
                    comment_str = doc.Comments[this.selectuuid].Range.Text.ToString();
                }
                this.parse_comment(comment_str);
                //this.parse_comment(doc.Comments[this.selectuuid].Range.Text.ToString());
                this.set_label();

                ShowLoaction(doc.Comments[this.selectuuid].Scope);
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToPerv(string method)
        {
            this.clear_lable();
            Document doc = this.baseRibbon.m_app.ActiveDocument;

            int Comment_count = doc.Comments.Count;
            this.selectuuid -= 1;
            if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
            {
                this.error_type = doc.Comments[this.selectuuid].Author.ToString();
                this.error_word = doc.Comments[this.selectuuid].Scope.Text;

                string comment_str = "";
                if (doc.Comments[this.selectuuid].Range.Text != null)
                {
                    comment_str = doc.Comments[this.selectuuid].Range.Text.ToString();
                }
                this.parse_comment(comment_str);
                //this.parse_comment(doc.Comments[this.selectuuid].Range.Text.ToString());
                this.set_label();

                ShowLoaction(doc.Comments[this.selectuuid].Scope);
            }
            else
            {
                this.selectuuid = 1;
                MessageBox.Show("已经是第一处了");
                //this.Close();
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.ToPerv("上一处");
        }
        private void ToNext(string method)
        {

            this.clear_lable();
            Document doc = this.baseRibbon.m_app.ActiveDocument;

            int Comment_count = doc.Comments.Count;
            this.selectuuid += 1;
            if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
            {
                this.error_type = doc.Comments[this.selectuuid].Author.ToString();
                this.error_word = doc.Comments[this.selectuuid].Scope.Text;

                string comment_str = "";
                if (doc.Comments[this.selectuuid].Range.Text != null)
                {
                    comment_str = doc.Comments[this.selectuuid].Range.Text.ToString();
                }
                this.parse_comment(comment_str);
                this.set_label();

                ShowLoaction(doc.Comments[this.selectuuid].Scope);
            }
            else
            {
                if (method == "下一处")
                {
                    this.selectuuid = doc.Comments.Count;
                    MessageBox.Show("已经是最后一处了");
                }
                else if (method == "修改")
                {
                    this.selectuuid = doc.Comments.Count;
                    MessageBox.Show("已经修改完最后一处错误");
                    this.Close();
                }
                else if (method == "清除")
                {
                    this.selectuuid = doc.Comments.Count;
                    MessageBox.Show("已经清除完最后一处错误");
                    this.Close();
                }


            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            ToNext("下一处");
        }

        /// <summary>
        /// 计算显示位置
        /// </summary>
        private void ShowLoaction(Range rg)
        {
            rg.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认要修改本次校对批注？", "修改校对批注", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                //this.clear_lable();
                Document doc = this.baseRibbon.m_app.ActiveDocument;

                int Comment_count = doc.Comments.Count;
                if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
                {
                    doc.Comments[this.selectuuid].Scope.Text = this.textBox_suggest.Text;
                    doc.Comments[this.selectuuid].Delete();
                }
                //移动到下一处
                this.selectuuid -= 1;
                ToNext("修改");
                //this.Close();
            }
        }
        //private void CheckCancelBtn(string collateWord)
        //{
        //    if (collateWord == "")
        //    {
        //        this.button5.Enabled = false;
        //        this.button1.Enabled = false;
        //        //this.difurl = 0;
        //    }
        //    else
        //    {
        //        this.button5.Enabled = true;
        //        this.button1.Enabled = true;
        //        //this.difurl = 1;
        //    }
            
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{

        //    cancelzici();
        //    ToNext("撤销");
        //}
        /// <summary>
        /// 取消字词错误
        /// </summary>
        //private void cancelzici()
        //{
        //    DialogResult dr = MessageBox.Show("确认要撤销该条校对？", "撤销该条校对", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    if (dr == DialogResult.OK)
        //    {
        //        Document doc = this.baseRibbon.m_app.ActiveDocument;

        //        int Comment_count = doc.Comments.Count;
        //        if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
        //        {
        //            this.baseRibbon.Log("get sentence:");
        //            this.baseRibbon.Log("Scope:" + doc.Comments[this.selectuuid].Scope.Start.ToString() + " " + doc.Comments[this.selectuuid].Scope.End.ToString());
        //            if (doc.Comments[this.selectuuid].Scope.Paragraphs.Count >= 1)
        //            {
        //                Paragraph pg = doc.Comments[this.selectuuid].Scope.Paragraphs[1];
        //                this.baseRibbon.Log("pg:" + pg.Range.Text.ToString());
        //            }
                    
        //            //string url = ConfigurationManager.AppSettings["ServerUrl"] + "proxy/collate/filter/add";
        //            //Dictionary<string, object> dic = new Dictionary<string, object>();
        //            //dic.Add("errorType", int.Parse(erone.errorType));
        //            //dic.Add("errorWord", this.error_word);
        //            //dic.Add("collateWord", this.collate_word);
        //            //dic.Add("sentence", erone.sentence);

        //            //var res = Utils.HttpUtils.PostData(url, dic);

        //            //if (res.Result != null)
        //            //{
        //            //    ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
        //            //    baseRibbon.Log("当前请求返回结果：" + res);

        //            //    //MessageBox.Show("errR.code:" + errR.code + "errR.msg:" + errR.msg);

        //            //    if (errR.code == "200")
        //            //    {
        //            //        MessageBox.Show("字词错误撤销成功");
        //            //        /*Range rg = ShowLoaction(erone);
        //            //        rg.End += erone.CommentLength - 1;
        //            //        rg.Select();
        //            //        rg.Text = this.label_errortext.Text;
        //            //        erone.AfterText = this.label_errortext.Text;*/

        //            //        erone.alreadyChange = true;
        //            //        erone.AfterText = erone.errorWord;
        //            //        //删除批注列表中的批注信息
        //            //        Document doc = baseRibbon.m_app.ActiveDocument;

        //            //        foreach (Comment com1 in doc.Comments)
        //            //        {
        //            //            string text = com1.Scope.Text;
        //            //            if (text == erone.errorWord)
        //            //            {
        //            //                com1.Delete();
        //            //                break;
        //            //            }
        //            //        }
        //            //    }
        //            //    this.Close();
        //            //}

        //            ////doc.Comments[this.selectuuid].Delete();


        //        }


        //    }
        //}
        /// <summary>
        ///// 取消语义错误
        ///// </summary>
        //private void cancelyuyi()
        //{
        //    DialogResult dr = MessageBox.Show("确认要撤销该条校对？", "撤销该条校对", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //    if (dr == DialogResult.OK)
        //    {
        //        ErrInfo erone = null;
        //        foreach (var one in this.List)
        //        {
        //            if (one.uuid == selectuuid)
        //            {
        //                erone = one;
        //                break;
        //            }
        //        }
        //        if (erone != null)
        //        {
        //            string url = ConfigurationManager.AppSettings["ServerUrl"] + "proxy/rs/collation/withdraw-error";
        //            baseRibbon.Log("请求地址为：" + url);
        //            baseRibbon.Log("请求参数为：" + "coreNoun=" + this.label_errortext.Text + "&assistNoun=" + erone.suggestions[0].collateWord + "&errorType=" + erone.endPos + "&suggestion=" + erone.suggestions[0].collateWord);
        //            var res = Utils.HttpUtils.DeleteWithdrawData(url, this.label_errortext.Text, erone.suggestions[0].collateWord, erone.endPos, erone.suggestions[0].collateWord);
        //            ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res);
        //            baseRibbon.Log("当前请求返回结果：" + res);
        //            if (errR.code == "1")
        //            {
        //                MessageBox.Show("语义错误撤销成功");
        //                /*Range rg = ShowLoaction(erone);
        //                rg.End += erone.CommentLength - 1;
        //                rg.Select();
        //                rg.Text = this.label_errortext.Text;
        //                erone.AfterText = this.label_errortext.Text;*/
        //                erone.alreadyChange = true;
        //                erone.AfterText = erone.errorWord;
        //                //删除批注列表中的批注信息
        //                Document doc = baseRibbon.m_app.ActiveDocument;
        //                foreach (Comment com1 in doc.Comments)
        //                {
        //                    string text = com1.Scope.Text;
        //                    if (text == erone.errorWord)
        //                    {
        //                        com1.Delete();
        //                        break;
        //                    }
        //                }
        //            }
        //            this.Close();
        //        }
        //    }
        //}

        private void Form_Result_Load(object sender, EventArgs e)
        {
            if (this.textBox_suggest.Text == "")
            {
                //this.button5.Enabled = false;
                this.button1.Enabled = false;
                //this.difurl = 0;
            }
            else
            {
                //this.button5.Enabled = true;
                this.button1.Enabled = true;
                //this.difurl = 1;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            Document doc = this.baseRibbon.m_app.ActiveDocument;

            int Comment_count = doc.Comments.Count;
            if (this.selectuuid >= 1 && this.selectuuid <= Comment_count)
            {
                doc.Comments[this.selectuuid].Delete();
            }
            //移动到下一处
            this.selectuuid -= 1;
            ToNext("清除");

        }

        private void CheckCancelBtn(object sender, EventArgs e)
        {
            if (this.textBox_suggest.Text == "")
            {
                //this.button5.Enabled = false;
                this.button1.Enabled = false;
                //this.difurl = 0;
            }
            else
            {
                //this.button5.Enabled = true;
                this.button1.Enabled = true;
                //this.difurl = 1;
            }
           
        }

    }
}
