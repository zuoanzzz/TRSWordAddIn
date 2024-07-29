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
    public partial class Form_AddError : Form
    {
        BaseRibbon baseRibbon;
        String changeText;
        String token;

        List<string> all_type = new List<string>();
        public Form_AddError(BaseRibbon br, String text, String token)
        {
            InitializeComponent();
            this.baseRibbon = br;
            changeText = text;
            this.token = token;
            this.all_type = this.baseRibbon.type_define_dict.Keys.ToList();

        }

        private void Form_AddError_Load(object sender, EventArgs e)
        {
            if (changeText.Length <= 1)
                changeText = "";
            this.textBox1.Text = changeText;
            this.textBox2.Text = "";
            //this.comboBox1.SelectedIndex = 0;
            if (this.textBox1.Text == "" || this.textBox2.Text == "" || this.textBox1.Text == this.textBox2.Text)
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }

            this.comboBox1.DataSource = this.all_type;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //DialogResult dr = MessageBox.Show("确定要将此项内容添加到黑名单词典吗？这将会修改后台词典，下次校对时，会根据此项内容进行校对。", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dr == DialogResult.OK)
            //{
            //    //调用添加字词类接口
            //    string url = ConfigurationManager.AppSettings["ServerUrl"] + "/api/collate/confuse";
            //    Dictionary<string, object> dic = new Dictionary<string, object>();

            //    dic.Add("wrongWord", this.textBox1.Text);
            //    dic.Add("rightWord", this.textBox2.Text);
            //    dic.Add("errorType", this.comboBox1.SelectedItem.ToString());

            //    var res = Utils.HttpUtils.PostData(url, dic, this.token);

            //    if (res.Result != null)
            //    {
            //        ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
            //        baseRibbon.Log("当前请求返回结果：" + res);

            //        if (errR.code == "200")
            //        {
            //            MessageBox.Show("黑名单添加成功!");

            //        }
            //    }
            //}

            //调用添加字词类接口
            string url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/confuse";
            Dictionary<string, object> dic = new Dictionary<string, object>();

            dic.Add("wrongWord", this.textBox1.Text);
            dic.Add("rightWord", this.textBox2.Text);
            dic.Add("errorType", this.comboBox1.Text);

            //MessageBox.Show("this.comboBox1.Text：" + this.comboBox1.Text);

            var res = Utils.HttpUtils.PostData(url, dic, this.token);

            if (res.Result != null)
            {
                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
                baseRibbon.Log("当前请求返回结果：" + res);

                if (errR.code == "200")
                {
                    MessageBox.Show("黑名单添加成功!");

                }
                else
                {
                    MessageBox.Show("黑名单添加失败!  code：" + errR.code);
                }
            }
            this.Close();
        }

        private void Check2addBtn(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" || this.textBox2.Text == "" || this.textBox1.Text == this.textBox2.Text)
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }

        private void Check1addBtn(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" || this.textBox2.Text == "" || this.textBox1.Text == this.textBox2.Text)
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }
    }
}
