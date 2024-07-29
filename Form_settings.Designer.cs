namespace TRSWordAddIn
{
    partial class Form_settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_thread = new System.Windows.Forms.TextBox();
            this.txt_wordCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mark_name_TextBox = new System.Windows.Forms.TextBox();
            this.insert_Button = new System.Windows.Forms.Button();
            this.delete_Button = new System.Windows.Forms.Button();
            this.filter_marks_listBox = new System.Windows.Forms.ListBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.label_check_weight = new System.Windows.Forms.Label();
            this.checkbox_weight = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.treeView3 = new System.Windows.Forms.TreeView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.treeView4 = new System.Windows.Forms.TreeView();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.treeView5 = new System.Windows.Forms.TreeView();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.treeView7 = new System.Windows.Forms.TreeView();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.treeView6 = new System.Windows.Forms.TreeView();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(704, 494);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(477, 494);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "后台服务器URL：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(246, 43);
            this.textBox1.Margin = new System.Windows.Forms.Padding(6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(493, 35);
            this.textBox1.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(72, 131);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(154, 24);
            this.label11.TabIndex = 5;
            this.label11.Text = "请求线程数：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(68, 194);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(178, 24);
            this.label12.TabIndex = 6;
            this.label12.Text = "分段字数控制：";
            // 
            // txt_thread
            // 
            this.txt_thread.Location = new System.Drawing.Point(246, 115);
            this.txt_thread.Margin = new System.Windows.Forms.Padding(6);
            this.txt_thread.Name = "txt_thread";
            this.txt_thread.Size = new System.Drawing.Size(196, 35);
            this.txt_thread.TabIndex = 7;
            // 
            // txt_wordCount
            // 
            this.txt_wordCount.Location = new System.Drawing.Point(246, 189);
            this.txt_wordCount.Margin = new System.Windows.Forms.Padding(6);
            this.txt_wordCount.Name = "txt_wordCount";
            this.txt_wordCount.Size = new System.Drawing.Size(196, 35);
            this.txt_wordCount.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 18;
            this.label2.Text = "过滤书签:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "书 签 名:";
            // 
            // mark_name_TextBox
            // 
            this.mark_name_TextBox.Location = new System.Drawing.Point(204, 34);
            this.mark_name_TextBox.Margin = new System.Windows.Forms.Padding(6);
            this.mark_name_TextBox.Multiline = true;
            this.mark_name_TextBox.Name = "mark_name_TextBox";
            this.mark_name_TextBox.Size = new System.Drawing.Size(175, 36);
            this.mark_name_TextBox.TabIndex = 20;
            // 
            // insert_Button
            // 
            this.insert_Button.Location = new System.Drawing.Point(416, 27);
            this.insert_Button.Margin = new System.Windows.Forms.Padding(6);
            this.insert_Button.Name = "insert_Button";
            this.insert_Button.Size = new System.Drawing.Size(100, 46);
            this.insert_Button.TabIndex = 21;
            this.insert_Button.Text = "添加";
            this.insert_Button.UseVisualStyleBackColor = true;
            this.insert_Button.Click += new System.EventHandler(this.insert_Button_Click);
            // 
            // delete_Button
            // 
            this.delete_Button.Location = new System.Drawing.Point(416, 88);
            this.delete_Button.Margin = new System.Windows.Forms.Padding(6);
            this.delete_Button.Name = "delete_Button";
            this.delete_Button.Size = new System.Drawing.Size(100, 46);
            this.delete_Button.TabIndex = 22;
            this.delete_Button.Text = "删除";
            this.delete_Button.UseVisualStyleBackColor = true;
            this.delete_Button.Click += new System.EventHandler(this.delete_Button_Click);
            // 
            // filter_marks_listBox
            // 
            this.filter_marks_listBox.FormattingEnabled = true;
            this.filter_marks_listBox.ItemHeight = 24;
            this.filter_marks_listBox.Location = new System.Drawing.Point(202, 99);
            this.filter_marks_listBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.filter_marks_listBox.Name = "filter_marks_listBox";
            this.filter_marks_listBox.Size = new System.Drawing.Size(178, 292);
            this.filter_marks_listBox.TabIndex = 23;
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(249, 494);
            this.button_reset.Margin = new System.Windows.Forms.Padding(6);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(150, 46);
            this.button_reset.TabIndex = 24;
            this.button_reset.Text = "重置";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // label_check_weight
            // 
            this.label_check_weight.AutoSize = true;
            this.label_check_weight.Location = new System.Drawing.Point(68, 270);
            this.label_check_weight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_check_weight.Name = "label_check_weight";
            this.label_check_weight.Size = new System.Drawing.Size(262, 24);
            this.label_check_weight.TabIndex = 27;
            this.label_check_weight.Text = "是否显示校对结果分数:";
            // 
            // checkbox_weight
            // 
            this.checkbox_weight.AutoSize = true;
            this.checkbox_weight.Checked = true;
            this.checkbox_weight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_weight.Location = new System.Drawing.Point(364, 267);
            this.checkbox_weight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkbox_weight.Name = "checkbox_weight";
            this.checkbox_weight.Size = new System.Drawing.Size(28, 27);
            this.checkbox_weight.TabIndex = 28;
            this.checkbox_weight.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage9);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(30, 150);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1018, 562);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 29;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkbox_weight);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label_check_weight);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.txt_thread);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txt_wordCount);
            this.tabPage1.Location = new System.Drawing.Point(154, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabPage1.Size = new System.Drawing.Size(860, 554);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(362, 329);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(28, 27);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(68, 332);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 24);
            this.label4.TabIndex = 29;
            this.label4.Text = "是否按粗略类型添加批注:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.mark_name_TextBox);
            this.tabPage2.Controls.Add(this.insert_Button);
            this.tabPage2.Controls.Add(this.delete_Button);
            this.tabPage2.Controls.Add(this.filter_marks_listBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(154, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(860, 554);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "书签过滤";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.tabPage3.Controls.Add(this.treeView1);
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Location = new System.Drawing.Point(154, 4);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Size = new System.Drawing.Size(860, 554);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "综合校对";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.Indent = 30;
            this.treeView1.Location = new System.Drawing.Point(4, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(852, 476);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeView2);
            this.tabPage4.Location = new System.Drawing.Point(154, 4);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Size = new System.Drawing.Size(860, 554);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "字词校对";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // treeView2
            // 
            this.treeView2.AllowDrop = true;
            this.treeView2.CheckBoxes = true;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView2.Indent = 30;
            this.treeView2.Location = new System.Drawing.Point(4, 5);
            this.treeView2.Name = "treeView2";
            this.treeView2.ShowNodeToolTips = true;
            this.treeView2.Size = new System.Drawing.Size(852, 476);
            this.treeView2.TabIndex = 3;
            this.treeView2.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.treeView3);
            this.tabPage5.Location = new System.Drawing.Point(154, 4);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage5.Size = new System.Drawing.Size(860, 554);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "语义校对";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // treeView3
            // 
            this.treeView3.AllowDrop = true;
            this.treeView3.CheckBoxes = true;
            this.treeView3.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView3.Indent = 30;
            this.treeView3.Location = new System.Drawing.Point(4, 5);
            this.treeView3.Name = "treeView3";
            this.treeView3.ShowNodeToolTips = true;
            this.treeView3.Size = new System.Drawing.Size(852, 476);
            this.treeView3.TabIndex = 3;
            this.treeView3.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.treeView4);
            this.tabPage6.Location = new System.Drawing.Point(154, 4);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage6.Size = new System.Drawing.Size(860, 554);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "专业术语校对";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // treeView4
            // 
            this.treeView4.AllowDrop = true;
            this.treeView4.CheckBoxes = true;
            this.treeView4.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView4.Indent = 30;
            this.treeView4.Location = new System.Drawing.Point(4, 5);
            this.treeView4.Name = "treeView4";
            this.treeView4.ShowNodeToolTips = true;
            this.treeView4.Size = new System.Drawing.Size(852, 476);
            this.treeView4.TabIndex = 3;
            this.treeView4.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.treeView5);
            this.tabPage7.Location = new System.Drawing.Point(154, 4);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage7.Size = new System.Drawing.Size(860, 554);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "格式校对";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // treeView5
            // 
            this.treeView5.AllowDrop = true;
            this.treeView5.CheckBoxes = true;
            this.treeView5.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView5.Indent = 30;
            this.treeView5.Location = new System.Drawing.Point(4, 5);
            this.treeView5.Name = "treeView5";
            this.treeView5.ShowNodeToolTips = true;
            this.treeView5.Size = new System.Drawing.Size(852, 476);
            this.treeView5.TabIndex = 4;
            this.treeView5.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.treeView7);
            this.tabPage9.Controls.Add(this.button4);
            this.tabPage9.Controls.Add(this.button3);
            this.tabPage9.Controls.Add(this.treeView6);
            this.tabPage9.Location = new System.Drawing.Point(154, 4);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(860, 554);
            this.tabPage9.TabIndex = 8;
            this.tabPage9.Text = "自定义校对";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // treeView7
            // 
            this.treeView7.AllowDrop = true;
            this.treeView7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView7.Indent = 15;
            this.treeView7.Location = new System.Drawing.Point(507, 8);
            this.treeView7.Name = "treeView7";
            this.treeView7.ShowNodeToolTips = true;
            this.treeView7.Size = new System.Drawing.Size(345, 476);
            this.treeView7.TabIndex = 27;
            this.treeView7.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView6_ItemDrag);
            this.treeView7.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView6_DragDrop);
            this.treeView7.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView6_DragEnter);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(381, 113);
            this.button4.Margin = new System.Windows.Forms.Padding(6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(117, 46);
            this.button4.TabIndex = 26;
            this.button4.Text = "移除";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(381, 28);
            this.button3.Margin = new System.Windows.Forms.Padding(6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 46);
            this.button3.TabIndex = 25;
            this.button3.Text = "新增";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // treeView6
            // 
            this.treeView6.AllowDrop = true;
            this.treeView6.CheckBoxes = true;
            this.treeView6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView6.Indent = 30;
            this.treeView6.Location = new System.Drawing.Point(3, 5);
            this.treeView6.Name = "treeView6";
            this.treeView6.ShowNodeToolTips = true;
            this.treeView6.Size = new System.Drawing.Size(369, 476);
            this.treeView6.TabIndex = 5;
            this.treeView6.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView6_AfterLabelEdit);
            this.treeView6.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.editTreeView_AfterCheck);
            this.treeView6.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView6_ItemDrag);
            this.treeView6.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView6_DragDrop);
            this.treeView6.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView6_DragEnter);
            this.treeView6.DoubleClick += new System.EventHandler(this.treeView6_DoubleClick);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.dataGridView1);
            this.tabPage8.Location = new System.Drawing.Point(154, 4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(860, 554);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "过滤阈值";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 37;
            this.dataGridView1.Size = new System.Drawing.Size(854, 469);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 562);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_settings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Form_settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage9.ResumeLayout(false);
            this.tabPage8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_thread;
        private System.Windows.Forms.TextBox txt_wordCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mark_name_TextBox;
        private System.Windows.Forms.Button insert_Button;
        private System.Windows.Forms.Button delete_Button;
        private System.Windows.Forms.ListBox filter_marks_listBox;
        private System.Windows.Forms.Button button_reset;
        private System.Windows.Forms.Label label_check_weight;
        private System.Windows.Forms.CheckBox checkbox_weight;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TreeView treeView3;
        private System.Windows.Forms.TreeView treeView4;
        private System.Windows.Forms.TreeView treeView5;
        private System.Windows.Forms.TreeView treeView6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TreeView treeView7;
    }
}