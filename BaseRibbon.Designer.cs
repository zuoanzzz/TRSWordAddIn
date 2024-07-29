namespace TRSWordAddIn
{
    partial class BaseRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BaseRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseRibbon));
            this.tab2 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.button_basefun1 = this.Factory.CreateRibbonButton();
            this.button_basefun2 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.button8 = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.button5 = this.Factory.CreateRibbonButton();
            this.button_prev = this.Factory.CreateRibbonButton();
            this.button_next = this.Factory.CreateRibbonButton();
            this.group6 = this.Factory.CreateRibbonGroup();
            this.box3 = this.Factory.CreateRibbonBox();
            this.button6 = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.group3 = this.Factory.CreateRibbonGroup();
            this.button9 = this.Factory.CreateRibbonButton();
            this.button10 = this.Factory.CreateRibbonButton();
            this.group4 = this.Factory.CreateRibbonGroup();
            this.box2 = this.Factory.CreateRibbonBox();
            this.button_setting = this.Factory.CreateRibbonButton();
            this.button7 = this.Factory.CreateRibbonButton();
            this.box1 = this.Factory.CreateRibbonBox();
            this.button_outResult = this.Factory.CreateRibbonButton();
            this.button_version = this.Factory.CreateRibbonButton();
            this.button11 = this.Factory.CreateRibbonButton();
            this.button12 = this.Factory.CreateRibbonButton();
            this.tab2.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.group6.SuspendLayout();
            this.box3.SuspendLayout();
            this.group3.SuspendLayout();
            this.group4.SuspendLayout();
            this.box2.SuspendLayout();
            this.box1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab2
            // 
            this.tab2.Groups.Add(this.group1);
            this.tab2.Groups.Add(this.group2);
            this.tab2.Groups.Add(this.group6);
            this.tab2.Groups.Add(this.group3);
            this.tab2.Groups.Add(this.group4);
            this.tab2.Label = "智能纠错";
            this.tab2.Name = "tab2";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button_basefun1);
            this.group1.Items.Add(this.button_basefun2);
            this.group1.Items.Add(this.button2);
            this.group1.Items.Add(this.button1);
            this.group1.Items.Add(this.button4);
            this.group1.Items.Add(this.button8);
            this.group1.Label = "纠错类型";
            this.group1.Name = "group1";
            // 
            // button_basefun1
            // 
            this.button_basefun1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_basefun1.Image = ((System.Drawing.Image)(resources.GetObject("button_basefun1.Image")));
            this.button_basefun1.Label = "综合校对";
            this.button_basefun1.Name = "button_basefun1";
            this.button_basefun1.ShowImage = true;
            this.button_basefun1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_basefun1_Click);
            // 
            // button_basefun2
            // 
            this.button_basefun2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button_basefun2.Image = ((System.Drawing.Image)(resources.GetObject("button_basefun2.Image")));
            this.button_basefun2.Label = "字词校对";
            this.button_basefun2.Name = "button_basefun2";
            this.button_basefun2.ShowImage = true;
            this.button_basefun2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_basefun2_Click);
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Label = "语义校对";
            this.button2.Name = "button2";
            this.button2.ShowImage = true;
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Label = "专业术语校对";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Label = "格式校对";
            this.button4.Name = "button4";
            this.button4.ShowImage = true;
            this.button4.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button4_Click);
            // 
            // button8
            // 
            this.button8.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.Label = "自定义校对";
            this.button8.Name = "button8";
            this.button8.ShowImage = true;
            this.button8.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button8_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.button5);
            this.group2.Items.Add(this.button_prev);
            this.group2.Items.Add(this.button_next);
            this.group2.Label = "结果查看";
            this.group2.Name = "group2";
            // 
            // button5
            // 
            this.button5.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Label = "查看结果";
            this.button5.Name = "button5";
            this.button5.ShowImage = true;
            this.button5.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button5_Click);
            // 
            // button_prev
            // 
            this.button_prev.Image = ((System.Drawing.Image)(resources.GetObject("button_prev.Image")));
            this.button_prev.Label = "上一处";
            this.button_prev.Name = "button_prev";
            this.button_prev.ShowImage = true;
            this.button_prev.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_prev_Click);
            // 
            // button_next
            // 
            this.button_next.Image = ((System.Drawing.Image)(resources.GetObject("button_next.Image")));
            this.button_next.Label = "下一处";
            this.button_next.Name = "button_next";
            this.button_next.ShowImage = true;
            this.button_next.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_next_Click);
            // 
            // group6
            // 
            this.group6.Items.Add(this.box3);
            this.group6.Items.Add(this.button3);
            this.group6.Items.Add(this.button11);
            this.group6.Items.Add(this.button12);
            this.group6.Label = "词典添加";
            this.group6.Name = "group6";
            // 
            // box3
            // 
            this.box3.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box3.Items.Add(this.button6);
            this.box3.Name = "box3";
            // 
            // button6
            // 
            this.button6.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.Label = "添加黑名单";
            this.button6.Name = "button6";
            this.button6.ShowImage = true;
            this.button6.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Label = "添加白名单";
            this.button3.Name = "button3";
            this.button3.ShowImage = true;
            this.button3.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button3_Click);
            // 
            // group3
            // 
            this.group3.Items.Add(this.button9);
            this.group3.Items.Add(this.button10);
            this.group3.Label = "结果处理";
            this.group3.Name = "group3";
            // 
            // button9
            // 
            this.button9.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button9.Enabled = false;
            this.button9.Image = ((System.Drawing.Image)(resources.GetObject("button9.Image")));
            this.button9.Label = "批量修改";
            this.button9.Name = "button9";
            this.button9.ShowImage = true;
            this.button9.Visible = false;
            this.button9.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button10.Image = ((System.Drawing.Image)(resources.GetObject("button10.Image")));
            this.button10.Label = "清理批注";
            this.button10.Name = "button10";
            this.button10.ShowImage = true;
            this.button10.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button10_Click);
            // 
            // group4
            // 
            this.group4.Items.Add(this.box2);
            this.group4.Items.Add(this.box1);
            this.group4.Label = "关于";
            this.group4.Name = "group4";
            // 
            // box2
            // 
            this.box2.Items.Add(this.button_setting);
            this.box2.Items.Add(this.button7);
            this.box2.Name = "box2";
            // 
            // button_setting
            // 
            this.button_setting.Image = ((System.Drawing.Image)(resources.GetObject("button_setting.Image")));
            this.button_setting.Label = "设置";
            this.button_setting.Name = "button_setting";
            this.button_setting.ShowImage = true;
            this.button_setting.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_setting_Click);
            // 
            // button7
            // 
            this.button7.Image = ((System.Drawing.Image)(resources.GetObject("button7.Image")));
            this.button7.Label = "登录";
            this.button7.Name = "button7";
            this.button7.ShowImage = true;
            this.button7.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button7_Click);
            // 
            // box1
            // 
            this.box1.Items.Add(this.button_outResult);
            this.box1.Items.Add(this.button_version);
            this.box1.Name = "box1";
            // 
            // button_outResult
            // 
            this.button_outResult.Image = ((System.Drawing.Image)(resources.GetObject("button_outResult.Image")));
            this.button_outResult.Label = "日志";
            this.button_outResult.Name = "button_outResult";
            this.button_outResult.ShowImage = true;
            this.button_outResult.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_outResult_Click);
            // 
            // button_version
            // 
            this.button_version.Image = ((System.Drawing.Image)(resources.GetObject("button_version.Image")));
            this.button_version.Label = "版本";
            this.button_version.Name = "button_version";
            this.button_version.ShowImage = true;
            this.button_version.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_version_Click);
            // 
            // button11
            // 
            this.button11.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button11.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button11.Label = "添加职务信息";
            this.button11.Name = "button11";
            this.button11.ShowImage = true;
            this.button11.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button12.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button12.Label = "添加专业术语";
            this.button12.Name = "button12";
            this.button12.ShowImage = true;
            // 
            // BaseRibbon
            // 
            this.Name = "BaseRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab2);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.BaseRibbon_Load);
            this.tab2.ResumeLayout(false);
            this.tab2.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.group6.ResumeLayout(false);
            this.group6.PerformLayout();
            this.box3.ResumeLayout(false);
            this.box3.PerformLayout();
            this.group3.ResumeLayout(false);
            this.group3.PerformLayout();
            this.group4.ResumeLayout(false);
            this.group4.PerformLayout();
            this.box2.ResumeLayout(false);
            this.box2.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.Office.Tools.Ribbon.RibbonTab tab2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_basefun1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button6;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button9;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button10;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group4;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_version;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_setting;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_outResult;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_basefun2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_prev;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_next;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group6;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button7;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box2;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button8;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button11;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button12;
    }

    partial class ThisRibbonCollection
    {
        internal BaseRibbon BaseRibbon
        {
            get { return this.GetRibbon<BaseRibbon>(); }
        }
    }
}
