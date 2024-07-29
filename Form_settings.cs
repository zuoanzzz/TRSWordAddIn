using System;
//using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace TRSWordAddIn
{
    public partial class Form_settings : Form
    {
        
        BaseRibbon baseRibbon;
        bool is_display = true;
        bool is_display_by_classes = true;


        //功能一览：是否能进行编辑
        string type_define_text = "";
        public Dictionary<string, List<string>> type_define_dict = new Dictionary<string, List<string>>();
        DataTable mode_dt = new DataTable();

        //功能分类
        public Dictionary<string, List<List<string>>> st_collate_dict = new Dictionary<string, List<List<string>>>();
        public string st_collate_dict_text = "";

        public Dictionary<string, List<List<string>>> zh_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zc_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> yy_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zy_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> gs_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zd_collate_dict = new Dictionary<string, List<List<string>>>();


        public List<string> in_zd_node = new List<string>();
        public List<string> not_in_zd_node = new List<string>();
        public TreeNode not_node = new TreeNode();


        public List<string> mark_list;
        public Form_settings(BaseRibbon br)
        {
            InitializeComponent();
            this.baseRibbon = br;


        }


        private void dict_to_treeView()
        {
            this.treeView1.Nodes.Clear();
            foreach (var key in this.zh_collate_dict.Keys)
            {
                TreeNode node = this.treeView1.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.zh_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            this.treeView1.ExpandAll();

            //==================================

            this.treeView2.Nodes.Clear();
            foreach (var key in this.zc_collate_dict.Keys)
            {
                TreeNode node = this.treeView2.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.zc_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            this.treeView2.ExpandAll();

            //==================================

            this.treeView3.Nodes.Clear();
            foreach (var key in this.yy_collate_dict.Keys)
            {
                TreeNode node = this.treeView3.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.yy_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            this.treeView3.ExpandAll();

            //==================================

            this.treeView4.Nodes.Clear();
            foreach (var key in this.zy_collate_dict.Keys)
            {
                TreeNode node = this.treeView4.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.zy_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            this.treeView4.ExpandAll();

            //==================================

            this.treeView5.Nodes.Clear();
            foreach (var key in this.gs_collate_dict.Keys)
            {
                TreeNode node = this.treeView5.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.gs_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            this.treeView5.ExpandAll();


            //==================================

            //this.baseRibbon.Log("zd_collate_dict:" + JsonConvert.SerializeObject(this.zd_collate_dict));

            foreach (var key in this.zd_collate_dict.Keys)
            {
                for (int i = this.zd_collate_dict[key].Count - 1; i >=0; --i)
                {
                    if (!this.type_define_dict.ContainsKey(this.zd_collate_dict[key][i][0]))
                    {
                        this.zd_collate_dict[key].Remove(this.zd_collate_dict[key][i]);
                    }
                }

            }
            //this.baseRibbon.Log("zd_collate_dict:" + JsonConvert.SerializeObject(this.zd_collate_dict));


            this.treeView6.Nodes.Clear();
            foreach (var key in this.zd_collate_dict.Keys)
            {
                TreeNode node = this.treeView6.Nodes.Add(key);
                bool is_checked = false;
                foreach (var one in this.zd_collate_dict[key])
                {
                    node.Nodes.Add(one[0]).Checked = bool.Parse(one[1]);
                    node.Checked = true;
                    is_checked = is_checked || bool.Parse(one[1]);

                }
                node.Checked = is_checked;
            }
            //this.treeView6.LabelEdit = true;
            this.treeView6.ExpandAll();


            //==================================
            this.in_zd_node.Clear();
            this.not_in_zd_node.Clear();


            foreach (var key in this.zd_collate_dict.Keys)
            {
                foreach (var one in this.zd_collate_dict[key])
                {
                    this.in_zd_node.Add(one[0]);
                }

            }

            this.treeView7.Nodes.Clear();
            this.not_node = this.treeView7.Nodes.Add("未分配");

            foreach (var key in this.type_define_dict.Keys)
            {
                if (this.in_zd_node.Contains(key))
                {

                }
                else
                {
                    this.not_in_zd_node.Add(key);
                    this.not_node.Nodes.Add(key);
                }
            }
            this.treeView7.ExpandAll();

        }


        private void treeView_to_dict()
        {
            this.zh_collate_dict.Clear();
            foreach(TreeNode node in this.treeView1.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach(TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.zh_collate_dict.Add(node.Text, tmp);
            }

            //==========================
            this.zc_collate_dict.Clear();
            foreach (TreeNode node in this.treeView2.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach (TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.zc_collate_dict.Add(node.Text, tmp);
            }

            //==========================
            this.yy_collate_dict.Clear();
            foreach (TreeNode node in this.treeView3.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach (TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.yy_collate_dict.Add(node.Text, tmp);
            }

            //==========================
            this.zy_collate_dict.Clear();
            foreach (TreeNode node in this.treeView4.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach (TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.zy_collate_dict.Add(node.Text, tmp);
            }

            //==========================
            this.gs_collate_dict.Clear();
            foreach (TreeNode node in this.treeView5.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach (TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.gs_collate_dict.Add(node.Text, tmp);
            }

            //==========================
            this.zd_collate_dict.Clear();
            foreach (TreeNode node in this.treeView6.Nodes)
            {
                List<List<string>> tmp = new List<List<string>>();
                foreach (TreeNode one in node.Nodes)
                {
                    List<string> tmp1 = new List<string>();
                    tmp1.Add(one.Text);
                    tmp1.Add(one.Checked.ToString());
                    tmp.Add(tmp1);

                }
                this.zd_collate_dict.Add(node.Text, tmp);
            }
        }

        private void init_dataGrid()
        {

            this.mode_dt.Clear();

            foreach (var key in this.type_define_dict.Keys)
            {


                if (this.type_define_dict[key].Count == 2)
                {
                    DataRow dr = this.mode_dt.NewRow();
                    dr["错误类型"] = key;
                    dr["说明"] = this.type_define_dict[key][0];
                    dr["过滤阈值"] = this.type_define_dict[key][1];
                    this.mode_dt.Rows.Add(dr);
                }


            }


        }
        private void Form_settings_Load(object sender, EventArgs e)
        {

            this.textBox1.Text = ConfigurationManager.AppSettings["ServerUrl"];
            this.txt_thread.Text = ConfigurationManager.AppSettings["ThreadNumber"];
            this.txt_wordCount.Text = ConfigurationManager.AppSettings["WordCount"];
            this.is_display = Convert.ToBoolean(ConfigurationManager.AppSettings["is_display_weight"]);
            this.checkbox_weight.Checked = this.is_display;
            this.is_display_by_classes = Convert.ToBoolean(ConfigurationManager.AppSettings["is_display_by_classes"]);
            this.checkBox1.Checked = this.is_display_by_classes;

            string mark_text = ConfigurationManager.AppSettings["filter_marks"];
            this.mark_list = mark_text.Split(';').ToList();

            foreach (var mark in this.mark_list)
            {
                if (mark != "")
                    this.filter_marks_listBox.Items.Add(mark);
            }
            //功能一览
            this.type_define_text = ConfigurationManager.AppSettings["type_define"];
            this.type_define_dict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(this.type_define_text);

            this.mode_dt.Columns.Add("错误类型", typeof(string));
            this.mode_dt.Columns.Add("说明", typeof(string));
            this.mode_dt.Columns.Add("过滤阈值", typeof(string));
            this.init_dataGrid();
            this.dataGridView1.DataSource = this.mode_dt;
            this.dataGridView1.Columns["错误类型"].ReadOnly = true;
            this.dataGridView1.Columns["说明"].ReadOnly = true;


            //功能分类
            string tmp_json_text = ConfigurationManager.AppSettings["zh_collate"];
            this.zh_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);

            tmp_json_text = ConfigurationManager.AppSettings["zc_collate"];
            this.zc_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);

            tmp_json_text = ConfigurationManager.AppSettings["yy_collate"];
            this.yy_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);

            tmp_json_text = ConfigurationManager.AppSettings["zy_collate"];
            this.zy_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);

            tmp_json_text = ConfigurationManager.AppSettings["gs_collate"];
            this.gs_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);

            tmp_json_text = ConfigurationManager.AppSettings["zd_collate"];
            this.zd_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(tmp_json_text);


            //string tmp_json_text = ConfigurationManager.AppSettings["zh_collate"];
            //this.zh_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(tmp_json_text);

            //foreach (var key in this.zh_collate_dict.Keys)
            //{
            //    TreeNode node = this.treeView1.Nodes.Add(key);
            //    foreach(var one in this.zh_collate_dict[key])
            //    {
            //        node.Nodes.Add(one).Checked = true;
            //        node.Checked = true;
            //    }
            //}
            //this.treeView1.ExpandAll();

            this.dict_to_treeView();
 

            


        }

        private string CheckURL(string url)
        {
            string res = "";
            string lastone = url.Substring(url.Length - 1, 1);
            string lasttwo = url.Substring(url.Length - 2, 1);
            if(lastone != "/")
            {
                res = url + "/";
                return res;
            }
            if(lastone == "/" && lasttwo == "/")
            {
                res = url.Substring(0, url.Length - 1);
                return res;
            }
            return url;
        }

        //检查输入的过滤阈值是否合法
        private bool Check_threshold(string thread)
        {
            float numFloat;
            if (!float.TryParse(thread, out numFloat))
            {
                MessageBox.Show("请输入0~1的过滤阈值");
                return false;
            }
            else
            {
                if (numFloat < 0 || numFloat > 1.0)
                {
                    MessageBox.Show("请输入0~1的过滤阈值");
                    return false;
                }
            }
            return true;


        }

        //检查输入的文字是否合法
        private bool Check_str(string str)
        {
            if (str.Replace(" ", "") == "")
            {
                MessageBox.Show("校对类型不能为空！");
                return false;
            }
            return true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["ServerUrl"].Value = CheckURL(this.textBox1.Text);
            cfa.AppSettings.Settings["ThreadNumber"].Value = this.txt_thread.Text;
            cfa.AppSettings.Settings["WordCount"].Value = this.txt_wordCount.Text;


            bool is_close = true;

            //if (!Check_threshold(this.textBox_zc_threshold.Text) || !Check_threshold(this.textBox_zh_threshold.Text) || !Check_threshold(this.textBox_yy_threshold.Text) || !Check_threshold(this.textBox_zy_threshold.Text))
            //{
            //    is_close = false;
            //}

            //if (is_close)
            //{
            //    cfa.AppSettings.Settings["zc_weight"].Value = this.textBox_zc_threshold.Text;
            //    cfa.AppSettings.Settings["zh_weight"].Value = this.textBox_zh_threshold.Text;
            //    cfa.AppSettings.Settings["yy_weight"].Value = this.textBox_yy_threshold.Text;
            //    cfa.AppSettings.Settings["zy_weight"].Value = this.textBox_zy_threshold.Text;
            //    cfa.AppSettings.Settings["gs_weight"].Value = this.textBox_gs_threshold.Text;
            //}


            this.mark_list.Clear();

            for (int i = 0; i < this.filter_marks_listBox.Items.Count; ++i )
            {
                this.mark_list.Add(this.filter_marks_listBox.Items[i].ToString());
            }
            cfa.AppSettings.Settings["filter_marks"].Value = string.Join(";",this.mark_list);



            //cfa.AppSettings.Settings["zh_filter"].Value = string.Join(";", this.baseRibbon.zh_filter_list);
            //cfa.AppSettings.Settings["zc_filter"].Value = string.Join(";", this.baseRibbon.zc_filter_list);
            //cfa.AppSettings.Settings["yy_filter"].Value = string.Join(";", this.baseRibbon.yy_filter_list);
            //cfa.AppSettings.Settings["zy_filter"].Value = string.Join(";", this.baseRibbon.zy_filter_list);
            //cfa.AppSettings.Settings["gs_filter"].Value = string.Join(";", this.baseRibbon.gs_filter_list);

            if (this.checkbox_weight.Checked == true)
                cfa.AppSettings.Settings["is_display_weight"].Value = "True";
            else
                cfa.AppSettings.Settings["is_display_weight"].Value = "False";

            if (this.checkBox1.Checked == true)
                cfa.AppSettings.Settings["is_display_by_classes"].Value = "True";
            else
                cfa.AppSettings.Settings["is_display_by_classes"].Value = "False";



            //保存功能一览内容
            this.dataGridView1.EndEdit();

            this.type_define_dict.Clear();
            this.type_define_text = "";
            foreach (DataRow dataRow in this.mode_dt.Rows)
            {
                if (dataRow == null)
                {
                    continue;
                }
                string errorTypeInfo = dataRow["错误类型"].ToString();
                string description = dataRow["说明"].ToString();
                string threshold = dataRow["过滤阈值"].ToString();
                if (errorTypeInfo == "" && description == "" && threshold == "")
                {
                    continue;
                }
                if (!Check_str(errorTypeInfo) || !Check_threshold(threshold))
                {
                    is_close = false;
                }

                List<string> tmp = new List<string>();
                tmp.Add(description);
                tmp.Add(threshold);
                this.type_define_dict.Add(errorTypeInfo, tmp);

            }
            if (is_close)
            {
                this.type_define_text = JsonConvert.SerializeObject(this.type_define_dict);
                cfa.AppSettings.Settings["type_define"].Value = this.type_define_text;
                //MessageBox.Show();
                //this.baseRibbon.Log(this.mode_define_text);

            }

            this.baseRibbon.Log("type_define:" + this.type_define_text);

            this.treeView_to_dict();

            string tmp_dict_text = JsonConvert.SerializeObject(this.zh_collate_dict);
            cfa.AppSettings.Settings["zh_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("zh_collate:" + tmp_dict_text);

            tmp_dict_text = JsonConvert.SerializeObject(this.zc_collate_dict);
            cfa.AppSettings.Settings["zc_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("zc_collate:" + tmp_dict_text);

            tmp_dict_text = JsonConvert.SerializeObject(this.yy_collate_dict);
            cfa.AppSettings.Settings["yy_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("yy_collate:" + tmp_dict_text);

            tmp_dict_text = JsonConvert.SerializeObject(this.zy_collate_dict);
            cfa.AppSettings.Settings["zy_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("zy_collate:" + tmp_dict_text);

            tmp_dict_text = JsonConvert.SerializeObject(this.gs_collate_dict);
            cfa.AppSettings.Settings["gs_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("gs_collate:" + tmp_dict_text);

            tmp_dict_text = JsonConvert.SerializeObject(this.zd_collate_dict);
            cfa.AppSettings.Settings["zd_collate"].Value = tmp_dict_text;
            this.baseRibbon.Log("zd_collate:" + tmp_dict_text);
            

            cfa.Save();
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
            this.baseRibbon.load_app_setting();
            if (is_close)
            {
                this.Close();
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void insert_Button_Click(object sender, EventArgs e)
        {
            if (this.mark_name_TextBox.Text != "")
            {
                if (!this.filter_marks_listBox.Items.Contains(this.mark_name_TextBox.Text))
                {
                    this.filter_marks_listBox.Items.Add(this.mark_name_TextBox.Text);
                }
            }
            this.mark_name_TextBox.Text = "";
        }

        private void delete_Button_Click(object sender, EventArgs e)
        {
            this.filter_marks_listBox.Items.Remove(this.filter_marks_listBox.SelectedItem);
            
        }


        private void button_reset_Click(object sender, EventArgs e)
        {
            try
            {
                string cstring = Utils.URLCheck.CheckUrl(this.baseRibbon.login_url);
                if (cstring.Length != 0)
                {
                    MessageBox.Show(cstring);
                    return;
                }

                bool is_login = this.baseRibbon.backgroundLogIn();
                //MessageBox.Show("is_login:" + is_login.ToString());
                if (is_login)
                {
                    this.baseRibbon.Log("GetData:");
                    string url = ConfigurationManager.AppSettings["ServerUrl"] + "api/biz/deptConfig/dict";
                    string res = Utils.HttpUtils.GetData(url, this.baseRibbon.token);
                    this.baseRibbon.Log(res);
                    JObject config = JObject.Parse(res);
                    
                    var code = config["code"];


                    if (int.Parse(code.ToString()) == 200)
                    {
                        var datas = config["data"];

                        
                        foreach (var data in datas)
                        {
                            //线程数量
                            if (data["configKey"].ToString() == "thread.size")
                            {
                                this.txt_thread.Text = data["configValue"].ToString();
                            }
                            //分片字数
                            else if (data["configKey"].ToString() == "thread.word.count")
                            {
                                this.txt_wordCount.Text = data["configValue"].ToString();
                            }
                            //过滤书签
                            else if (data["configKey"].ToString() == "filter.bookmark")
                            {
                                this.mark_list.Clear();
                                this.filter_marks_listBox.Items.Clear();
                                this.mark_list = data["configValue"].ToString().Split(';').ToList();

                                foreach (var mark in this.mark_list)
                                {
                                    if (mark != "")
                                        this.filter_marks_listBox.Items.Add(mark);
                                }
                            }
                            //错误类型配置
                            else if (data["configKey"].ToString() == "config.error.type")
                            {
                                //this.baseRibbon.Log("==================");
                                //标准类型树
                                this.st_collate_dict.Clear();
                                this.type_define_dict.Clear();


                                var type_tree = data["configValue"];
                                JArray type_tree_list = JArray.Parse(type_tree.ToString());

                                foreach (var type in type_tree_list)
                                {
                                    string type_name = type["name"].ToString();
                                    //this.baseRibbon.Log("type:" + type);
                                    var children = type["children"];
                                    //this.baseRibbon.Log("children type:" + children.Type.ToString());
                                    //this.baseRibbon.Log("children:" + children);
                                    
                                    List<List<string>> tmp = new List<List<string>>();
                                    foreach (var cd in children)
                                    {
                                        string cd_name = cd["name"].ToString();
                                        string cd_desc = cd["desc"].ToString();
                                        string cd_threshold = cd["threshold"].ToString();

                                        //this.baseRibbon.Log("cd_name:" + cd_name.ToString());
                                        //this.baseRibbon.Log("cd_desc:" + cd_desc.ToString());
                                        //this.baseRibbon.Log("cd_threshold:" + cd_threshold.ToString());

                                        List<string> tmp1 = new List<string>();
                                        tmp1.Add(cd_name);
                                        tmp1.Add(false.ToString());
                                        tmp.Add(tmp1);

                                        List<string> tmp2 = new List<string>();
                                        tmp2.Add(cd_desc);
                                        tmp2.Add(cd_threshold);
                                        this.type_define_dict.Add(cd_name, tmp2);

                                    }
                                    this.st_collate_dict.Add(type_name, tmp);


                                }
                                //刷新错误类型表dataGrid
                                this.init_dataGrid();

                                this.st_collate_dict_text = JsonConvert.SerializeObject(this.st_collate_dict);

                            }
                        }

                        foreach (var data in datas)
                        {
                            //综合校对模式
                            if (data["configKey"].ToString() == "mode.default")
                            {
                                //this.baseRibbon.Log("");
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                                var configValue = data["configValue"];
                                Dictionary<string, List<string>> configValue_json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(configValue.ToString());
                                Dictionary<string, List<List<string>>> tmp_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(this.st_collate_dict_text);

                                foreach (var key in tmp_collate_dict.Keys)
                                {
                                    if (configValue_json.ContainsKey(key))
                                    {
                                        for (int i = 0; i < tmp_collate_dict[key].Count; ++i)
                                        {
                                            if (configValue_json[key].Contains(tmp_collate_dict[key][i][0]))
                                            {
                                                tmp_collate_dict[key][i][1] = true.ToString();
                                            }
                                        }
                                    }

                                }
                                this.zh_collate_dict = tmp_collate_dict;
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                            }
                            //字词校对模式
                            else if (data["configKey"].ToString() == "mode.lexical")
                            {
                                //this.baseRibbon.Log("");
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                                var configValue = data["configValue"];
                                Dictionary<string, List<string>> configValue_json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(configValue.ToString());
                                Dictionary<string, List<List<string>>> tmp_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(this.st_collate_dict_text);

                                foreach (var key in tmp_collate_dict.Keys)
                                {
                                    if (configValue_json.ContainsKey(key))
                                    {
                                        for (int i = 0; i < tmp_collate_dict[key].Count; ++i)
                                        {
                                            if (configValue_json[key].Contains(tmp_collate_dict[key][i][0]))
                                            {
                                                tmp_collate_dict[key][i][1] = true.ToString();
                                            }
                                        }
                                    }

                                }
                                this.zc_collate_dict = tmp_collate_dict;
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                            }
                            //语义校对模式
                            else if (data["configKey"].ToString() == "mode.semantic")
                            {
                                //this.baseRibbon.Log("");
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                                var configValue = data["configValue"];
                                Dictionary<string, List<string>> configValue_json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(configValue.ToString());
                                Dictionary<string, List<List<string>>> tmp_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(this.st_collate_dict_text);

                                foreach (var key in tmp_collate_dict.Keys)
                                {
                                    if (configValue_json.ContainsKey(key))
                                    {
                                        for (int i = 0; i < tmp_collate_dict[key].Count; ++i)
                                        {
                                            if (configValue_json[key].Contains(tmp_collate_dict[key][i][0]))
                                            {
                                                tmp_collate_dict[key][i][1] = true.ToString();
                                            }
                                        }
                                    }

                                }
                                this.yy_collate_dict = tmp_collate_dict;
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));
                            }
                            //专业术语校对模式
                            else if (data["configKey"].ToString() == "mode.white")
                            {
                                //this.baseRibbon.Log("");
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                                var configValue = data["configValue"];
                                Dictionary<string, List<string>> configValue_json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(configValue.ToString());
                                Dictionary<string, List<List<string>>> tmp_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(this.st_collate_dict_text);

                                foreach (var key in tmp_collate_dict.Keys)
                                {
                                    if (configValue_json.ContainsKey(key))
                                    {
                                        for (int i = 0; i < tmp_collate_dict[key].Count; ++i)
                                        {
                                            if (configValue_json[key].Contains(tmp_collate_dict[key][i][0]))
                                            {
                                                tmp_collate_dict[key][i][1] = true.ToString();
                                            }
                                        }
                                    }

                                }
                                this.zy_collate_dict = tmp_collate_dict;
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));
                            }
                            //格式校对模式
                            else if (data["configKey"].ToString() == "mode.format")
                            {
                                //this.baseRibbon.Log("");
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));

                                var configValue = data["configValue"];
                                Dictionary<string, List<string>> configValue_json = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(configValue.ToString());
                                Dictionary<string, List<List<string>>> tmp_collate_dict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(this.st_collate_dict_text);

                                foreach (var key in tmp_collate_dict.Keys)
                                {
                                    if (configValue_json.ContainsKey(key))
                                    {
                                        for (int i = 0; i < tmp_collate_dict[key].Count; ++i)
                                        {
                                            if (configValue_json[key].Contains(tmp_collate_dict[key][i][0]))
                                            {
                                                tmp_collate_dict[key][i][1] = true.ToString();
                                            }
                                        }
                                    }

                                }
                                this.gs_collate_dict = tmp_collate_dict;
                                //this.baseRibbon.Log("st_collate_dict:" + JsonConvert.SerializeObject(this.st_collate_dict));
                            }
                        }
                        

                        this.dict_to_treeView();

                    }
                    else
                    {
                        MessageBox.Show("获取数据失败");
                    }

                }
                else
                {
                    this.baseRibbon.FormLogIn();
                }



                //var datas = dyn["data"];
  
                //foreach (var data in datas)
                //{
                //this.txt_thread.Text = ConfigurationManager.AppSettings["ThreadNumber"];
                //    //this.txt_wordCount.Text = ConfigurationManager.AppSettings["WordCount"];

                //    //this.textBox_zc_threshold.Text = ConfigurationManager.AppSettings["zc_weight"];
                //    //this.textBox_zh_threshold.Text = ConfigurationManager.AppSettings["zh_weight"];
                //    //this.textBox_yy_threshold.Text = ConfigurationManager.AppSettings["yy_weight"];
                //    if (data["configKey"].ToString() == "thread.size")
                //    {
                //        this.txt_thread.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "thread.word.count")
                //    {
                //        this.txt_wordCount.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "threshold.default")
                //    {
                //        this.textBox_zh_threshold.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "threshold.lexical")
                //    {
                //        this.textBox_zc_threshold.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "threshold.semantic")
                //    {
                //        this.textBox_yy_threshold.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "threshold.white")
                //    {
                //        this.textBox_zy_threshold.Text = data["configValue"].ToString();
                //    }
                //    else if (data["configKey"].ToString() == "filter.bookmark")
                //    {
                //        this.mark_list.Clear();
                //        this.filter_marks_listBox.Items.Clear();
                //        this.mark_list = data["configValue"].ToString().Split(';').ToList();

                //        foreach (var mark in this.mark_list)
                //        {
                //            if (mark != "")
                //                this.filter_marks_listBox.Items.Add(mark);
                //        }
                //    }
                //    else if (data["configKey"].ToString() == "plugin.value.show")
                //    {
                //        this.get_check(data["configValue"].ToString());
                //        this.checkbox_weight.Checked = this.is_display;
                //    }

                //}
            }
            catch (Exception ex)
            {
                this.baseRibbon.Log(ex.ToString());
                MessageBox.Show("获取数据失败");

            }


        }


        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush _Brush = new SolidBrush(Color.Black);//单色画刷
            RectangleF _TabTextArea = (RectangleF)tabControl1.GetTabRect(e.Index);//绘制区域
            StringFormat _sf = new StringFormat();//封装文本布局格式信息
            _sf.LineAlignment = StringAlignment.Center;
            _sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(tabControl1.Controls[e.Index].Text, SystemInformation.MenuFont, _Brush, _TabTextArea, _sf);

            Font fntTab = new Font(e.Font, FontStyle.Bold);
            Brush bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.Control, SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
            Brush bshFore = Brushes.Black;
            if (e.Index == this.tabControl1.SelectedIndex)
            {
                //fntTab = new Font(e.Font, FontStyle.Bold);
                bshBack = new SolidBrush(Color.AliceBlue);
                //bshFore = Brushes.BurlyWood;
            }
            //else
            //{
            //    fntTab = e.Font;
            //    bshBack = new SolidBrush(Color.White);
            //    bshFore = new SolidBrush(Color.Black);
            //}
            string tabName = this.tabControl1.TabPages[e.Index].Text;
            StringFormat sftTab = new StringFormat();
            e.Graphics.FillRectangle(bshBack, e.Bounds);
            Rectangle recTab = e.Bounds;
            recTab = new Rectangle(recTab.X, recTab.Y + 4, recTab.Width, recTab.Height - 4);
            e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.mode_dt.Rows.Add();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
 
            //int k = this.dataGridView1.SelectedRows.Count;
            //if (MessageBox.Show(this.dataGridView1.Rows.Count.ToString() + "您确认要删除这" + Convert.ToString(k) + "项吗？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)//给出提示
            //{

            //}

            foreach (DataGridViewRow dataRow in this.dataGridView1.SelectedRows)
            {
                this.dataGridView1.Rows.Remove(dataRow);
            }
            
        }

        //当用户开始拖动节点时发生
        private void treeView6_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        //在将对象拖入控件的边界时发生
        private void treeView6_DragEnter(object sender, DragEventArgs e)
        {
            //判断拖动的是否为树节点
            if (e.Data.GetDataPresent(typeof(TreeNode)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        //在完成拖放操作时发生
        private void treeView6_DragDrop(object sender, DragEventArgs e)
        {
            string Moveid = "", Dropid = "";

            TreeView trv = sender as TreeView;

            TreeNode myNode = null;
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                //获得移动节点
                myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
                //获得移动节点的NodeId
                //MessageBox.Show("myNode.Level:" + myNode.Level.ToString());
                Moveid = (string)myNode.Text.ToString();
                if (myNode.Level == 0)
                {
                    return;
                }

            }
            else
            {
                MessageBox.Show("error");
            }

            //将树节点的位置计算成工作区坐标。
            Point Position = new Point();
            Position.X = e.X;
            Position.Y = e.Y;
            Position = trv.PointToClient(Position);

            //const Single scrollRegion = 20;
            //if ((Position.Y + scrollRegion) > this.treeView6.Height)
            //{
            //    // Call the API to scroll down
            //    SendMessage(this.treeView6.Handle, (int)277, (int)1, 0);
            //}
            //else if (Position.Y < (this.treeView6.Top + scrollRegion))
            //{
            //    // Call thje API to scroll up
            //    SendMessage(this.treeView6.Handle, (int)277, (int)0, 0);
            //}


            //检索目标节点
            TreeNode DropNode = trv.GetNodeAt(Position);
            //MessageBox.Show("DropNode.Level:" + DropNode.Level.ToString());
            if (DropNode != null)
            {
                if (DropNode.Level == 1)
                {
                    DropNode = DropNode.Parent;
                }
            }
            //if (DropNode != null && DropNode.Parent ==)
            //{
            //    DropNode = DropNode.Parent;
            //    if (DropNode.Level == 0)
            //    {
            //        return;
            //    }
            //}
  
            // 1.目标节点不是空。2.目标节点不是被拖拽接点的子节点。3.目标节点不是被拖拽节点本身
            if (DropNode != null && DropNode.Parent != myNode && DropNode != myNode)
            {
                //临时节点
                TreeNode tempNode = myNode;
                // 将被拖拽节点从原来位置删除。
                myNode.Remove();
                // 在目标节点下增加被拖拽节点
                DropNode.Nodes.Add(tempNode);
                //目标节点的NodeId值
                Dropid = (string)DropNode.Text.ToString();



            }
            //// 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
            //if (DropNode == null)
            //{

            //    TreeNode DragNode = myNode;
            //    myNode.Remove();
            //    trv.Nodes.Add(DragNode);
            //}
        }

        private void editTreeView_AfterCheck(object sender, TreeViewEventArgs e)//勾选事件
        {
            //MessageBox.Show("change");
            if (e.Action == TreeViewAction.ByMouse)
            //当该事件是由鼠标点击触发时才发生，否则设置该结点的Checked为true也会导致该事件发生
            {
                if (e.Node.Checked)//勾选结点时
                {
                    setParentNodeChecked(e.Node);//勾选所有祖先结点
                    setChildNodeChecked(e.Node);//勾选所有子节点
                }
                else//取消勾选时
                {
                    setChildNodeCancel(e.Node);//取消所有子节点
                    setParentNodeCancel(e.Node);//处理祖先结点，需判断
                    
                }
            }
        }

        private void setChildNodeCancel(TreeNode node)//取消所有子节点的选择
        {
            foreach (TreeNode a in node.Nodes)
            {
                if (a != null)
                {
                    a.Checked = false;
                    setChildNodeCancel(a);
                }
            }
        }

        private void setParentNodeCancel(TreeNode node)//取消祖先结点选择
        {
            if(node.Parent!=null&& judegChildChecked(node.Parent))
            {
                TreeNode parent;
                node.Parent.Checked = false;
                parent = node.Parent;
                setParentNodeCancel(parent);
            }
        }

        private void setParentNodeChecked(TreeNode t)//选择所有祖先结点
        {
            TreeNode parent = t.Parent;
            while(parent!=null && parent.Checked == false)
            {
                parent.Checked = true;
                parent = parent.Parent;
            }
        }
        private void setChildNodeChecked(TreeNode t)//勾选所有子节点
        {
            
            foreach(TreeNode a in t.Nodes)
            {
                if (a != null)
                {
                    a.Checked = true;
                    setChildNodeChecked(a);
                }
            }
            
        }
        private bool judegChildChecked(TreeNode t)//判断其子节点是否有勾选状态
        {
            foreach(TreeNode a in t.Nodes)
            {
                if(a != null && a.Checked == true)
                {
                    return false;
                }
            }
            return true;
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.tabControl1.SelectedIndex == 8)
            //{
            //    //MessageBox.Show("tab change");
            //    this.dataGridView1.EndEdit();

            //    this.type_define_dict.Clear();
            //    this.type_classify_text = "";
            //    foreach (DataRow dataRow in this.mode_dt.Rows)
            //    {
            //        if (dataRow == null)
            //        {
            //            continue;
            //        }
            //        string errorTypeInfo = dataRow["错误类型"].ToString();
            //        string description = dataRow["说明"].ToString();
            //        string threshold = dataRow["过滤阈值"].ToString();
            //        string type = dataRow["分类类别"].ToString();
            //        if (errorTypeInfo == "" && description == "" && threshold == "")
            //        {
            //            continue;
            //        }
            //        if (!Check_str(errorTypeInfo) || !Check_threshold(threshold))
            //        {
            //            this.tabControl1.SelectedIndex = 7;
            //            return;
            //        }

            //        List<string> tmp = new List<string>();
            //        tmp.Add(description);
            //        tmp.Add(threshold);
            //        tmp.Add(type);
            //        this.type_define_dict.Add(errorTypeInfo, tmp);

            //    }


            //    this.dict_convert();
            //    this.listBox2.Items.Clear();
            //    foreach (var key in this.type_define_dict.Keys)
            //    {
            //        if (this.type_define_dict[key].Count == 3)
            //        {
            //            if (this.type_define_dict[key][2] == "")
            //            {
            //                this.listBox2.Items.Add(key);
            //            }
            //        }

            //    }

            //    List<string> box_list = this.classify_mode_dict[this.comboBox1.SelectedItem.ToString()];
            //    box_list.RemoveAll(j => j == "");

            //    this.listBox1.Items.Clear();
            //    //this.baseRibbon.Log("box_list_count:" + box_list.Count.ToString());
            //    foreach (string one in box_list)
            //    {
            //        //this.baseRibbon.Log("add:" + one.ToString());
            //        this.listBox1.Items.Add(one);
            //    }
            //}



        }

        private void treeView6_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {

            if (e.Label != null && e.Label.Trim().Length == 0)
            {
                MessageBox.Show("类型名不能为空！");
                e.CancelEdit = true;
                return;
            }

            foreach (TreeNode node in treeView6.Nodes)
            {
                if (e.Label == node.Text)
                {
                    MessageBox.Show("类型名不能重复！");
                    e.CancelEdit = true;
                }
            }

            this.treeView6.LabelEdit = false;

            
        }

        private void treeView6_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView6.SelectedNode != null)
            {
                if (this.treeView6.SelectedNode.Parent == null)
                {
                    this.treeView6.LabelEdit = true;
                    //this.treeView6.SelectedNode.Tag = this.treeView6.SelectedNode.Text;
                    this.treeView6.SelectedNode.BeginEdit();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.treeView6.LabelEdit = true;
            TreeNode node = this.treeView6.Nodes.Add("新增类" + this.treeView6.Nodes.Count.ToString());
            node.BeginEdit();
            node.Checked = true;
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (this.treeView6.SelectedNode != null)
            {
                if (this.treeView6.SelectedNode.Parent != null)
                {
                    this.not_node.Nodes.Add(this.treeView6.SelectedNode.Text);
                    this.in_zd_node.Remove(this.treeView6.SelectedNode.Text);
                    this.not_in_zd_node.Add(this.treeView6.SelectedNode.Text);
                }
                else
                {
                    foreach (TreeNode node in this.treeView6.SelectedNode.Nodes)
                    {
                        this.not_node.Nodes.Add(node.Text);
                        this.in_zd_node.Remove(node.Text);
                        this.not_in_zd_node.Remove(node.Text);
                    }
                }
            }

            this.treeView6.SelectedNode.Remove();
            
        }




    }


}
