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
    public partial class Form_AddRight : Form
    {
        BaseRibbon baseRibbon;
        String changeText;
        String token;

        List<string> entity_type = new List<string>() {"其他", "人名", "地名", "组织机构", "职务名称", "法规条约", "武器装备", "会议活动", "重大事件"};
        public Form_AddRight(BaseRibbon br, String text, String token)
        {
            InitializeComponent();
            this.baseRibbon = br;
            changeText = text;
            this.token = token;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DialogResult dr = MessageBox.Show("确定要将此项内容添加到白名单词库吗？这将会修改后台词库，下次校对时，会根据此项内容过滤误报。", "确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dr == DialogResult.OK)
            //{
            //    string url = ConfigurationManager.AppSettings["ServerUrl"] + "/api/collate/knowledge";
            //    Dictionary<string, object> dic = new Dictionary<string, object>();
            //    dic.Add("word", this.textBox1.Text);
      
            //    var res = Utils.HttpUtils.PostData(url, dic, this.token);

            //    if (res.Result != null)
            //    {
            //        ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
            //        baseRibbon.Log("当前请求返回结果：" + res);

  
            //        if (errR.code == "200")
            //        {
            //            MessageBox.Show("白名单添加成功!");
                       
            //        }
  
            //    }
         
            //}

            string url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/knowledge";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("word", this.textBox1.Text);
            dic.Add("entityType", this.comboBox1.Text);

            //MessageBox.Show("this.comboBox1.Text：" + this.comboBox1.Text);

            var res = Utils.HttpUtils.PostData(url, dic, this.token);

            if (res.Result != null)
            {
                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
                baseRibbon.Log("当前请求返回结果：" + res);


                if (errR.code == "200")
                {
                    MessageBox.Show("白名单添加成功!");

                }
                else
                {
                    MessageBox.Show("白名单添加失败!  code：" + errR.code);
                }

            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_AddRight_Load(object sender, EventArgs e)
        {
            if (changeText.Length <= 1)
                changeText = "";
            this.textBox1.Text = changeText;

            //this.comboBox1.SelectedIndex = 0;
            if (this.textBox1.Text == "" )
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }

            this.comboBox1.DataSource = this.entity_type;
        }

        private void CheckText(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "")
            {
                this.button1.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
