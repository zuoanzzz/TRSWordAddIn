using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRSWordAddIn
{
    public partial class Form_progress : Form
    {
        String startName = "";
        BaseRibbon baseRibbon;
        public Form_progress(BaseRibbon br,String sN)
        {
            InitializeComponent();
            baseRibbon = br;
            startName = sN;
        }

        private void setShowNumber(string number)
        {
            this.label1.Text = number;
        }
        private void Form_progress_Load(object sender, EventArgs e)
        {
            if (!this.baseRibbon.bgWorker.IsBusy)              //判断是否正在运行异步操作
            {
                this.progressBar1.Maximum = 100;
                this.baseRibbon.bgWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.baseRibbon.bgWorker.RunWorkerAsync(this.startName);
            }
        }
        /// <summary>
        /// 停止执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.baseRibbon.StopThread = true;
            this.baseRibbon.bgWorker.CancelAsync();
            this.Close();
        }

        delegate void SetProgress(int value);
        delegate void SetNumber(string value);
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string state = (string)e.UserState;
            this.progressBar1.Value = e.ProgressPercentage;
            this.label1.Text = Convert.ToString(e.ProgressPercentage) + "%";
            if (e.ProgressPercentage >= this.progressBar1.Maximum)
            {
                this.baseRibbon.bgWorker.CancelAsync();
                this.Close();
            }
            
        }
    }
}
