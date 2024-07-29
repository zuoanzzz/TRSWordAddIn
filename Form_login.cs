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
    public partial class Form_login : Form
    {
        string username = ConfigurationManager.AppSettings["username"];
        string code = ConfigurationManager.AppSettings["code"];
        string url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";

        public Form_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string cstring = Utils.URLCheck.CheckUrl(url);
            if (cstring.Length == 0)
            {
                string token = Utils.HttpUtils.get_login_token(url, this.textBox1.Text, this.textBox2.Text);

                if (token == "")
                {
                    MessageBox.Show("用户名或安全码不对，请重试！");
                }
                else
                {
                    MessageBox.Show("认证成功!");
                    Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    cfa.AppSettings.Settings["username"].Value = this.textBox1.Text;
                    cfa.AppSettings.Settings["code"].Value = this.textBox2.Text;
                    cfa.Save();
                    System.Configuration.ConfigurationManager.RefreshSection("appSettings");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(cstring);
                this.Close();
            }


        }

        private void Form_login_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = username;
            this.textBox2.Text = code;
            if (this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                this.button1.Enabled = false;
                this.button3.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
                this.button3.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                this.button1.Enabled = false;
                this.button3.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
                this.button3.Enabled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "" || this.textBox2.Text == "")
            {
                this.button1.Enabled = false;
                this.button3.Enabled = false;
            }
            else
            {
                this.button1.Enabled = true;
                this.button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            cfa.AppSettings.Settings["username"].Value = this.textBox1.Text;
            cfa.AppSettings.Settings["code"].Value = this.textBox2.Text;
            cfa.Save();
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");

        }
    }
}
