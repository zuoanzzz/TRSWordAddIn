using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using TRSWordAddIn.Utils;
using System.ComponentModel;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using System.Threading;

namespace TRSWordAddIn
{
    public partial class BaseRibbon
    {
        public List<ErrInfo> ResultList = new List<ErrInfo>();
        public int RequestNumber = 0;
        public Microsoft.Office.Interop.Word.Application m_app;


        float pre_perSize = 0.0F;
        float collate_perSize = 0.0F;
        private float finishSize = 0.0F;
        float result_perSize = 0.0F;

        public bool StopThread = false;
        private String LogMessage = "";
        public int CommentShowStatus = 1;




        public string collate_url;
        public string login_url;
        public string login_username;
        public string login_code;
        public string token = "";

        public int MaxThread;
        public int maxCount;
        bool is_display_weight = true;
        bool is_display_by_classes = true;

        public List<string> filter_mark_list = new List<string>();
        public List<Tuple<int, int>> filter_pos = new List<Tuple<int, int>>();


        public List<string> all_classify_list = new List<string>();


        public Dictionary<string, List<List<string>>> zh_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zc_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> yy_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zy_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> gs_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> zd_collate_dict = new Dictionary<string, List<List<string>>>();
        public Dictionary<string, List<List<string>>> current_collate_dict = new Dictionary<string, List<List<string>>>();

        public Dictionary<string, string> zh_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> zc_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> yy_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> zy_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> gs_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> zd_type_collate_dict = new Dictionary<string, string>();
        public Dictionary<string, string> current_type_collate_dict = new Dictionary<string, string>();

        public Dictionary<string, List<string>> type_define_dict = new Dictionary<string, List<string>>();

        public BackgroundWorker bgWorker = new BackgroundWorker();



        TaskFactory taskFac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(int.Parse(ConfigurationManager.AppSettings["ThreadNumber"])));
        TaskFactory taskFac_b = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(int.Parse(ConfigurationManager.AppSettings["ThreadNumber"])));

        // Add by Liuc
        public List<TextParagraph> textParagraphs = new List<TextParagraph>();
        public Dictionary<string, TextPart> textParts = new Dictionary<string, TextPart>();

        public void load_app_setting()
        {
            this.collate_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/text";
            this.login_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";
            this.login_username = ConfigurationManager.AppSettings["username"];
            this.login_code = ConfigurationManager.AppSettings["code"];

            this.MaxThread = int.Parse(ConfigurationManager.AppSettings["ThreadNumber"]);
            this.maxCount = int.Parse(ConfigurationManager.AppSettings["WordCount"]);
            this.is_display_weight = bool.Parse(ConfigurationManager.AppSettings["is_display_weight"]);
            this.is_display_by_classes = bool.Parse(ConfigurationManager.AppSettings["is_display_by_classes"]);

            this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();



            string type_define_text = ConfigurationManager.AppSettings["type_define"];
            this.type_define_dict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(type_define_text);


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


            this.all_classify_list = this.zh_collate_dict.Keys.ToList();
            //this.zh_collate_list.RemoveAll(j => j == "");
            //this.zc_collate_list.RemoveAll(j => j == "");
            //this.yy_collate_list.RemoveAll(j => j == "");
            //this.zy_collate_list.RemoveAll(j => j == "");
            //this.gs_collate_list.RemoveAll(j => j == "");


            this.zh_type_collate_dict.Clear();
            foreach (string key in this.zh_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.zh_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.zh_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }

            this.zc_type_collate_dict.Clear();
            foreach (string key in this.zc_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.zc_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.zc_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }

            this.yy_type_collate_dict.Clear();
            foreach (string key in this.yy_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.yy_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.yy_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }


            this.zy_type_collate_dict.Clear();
            foreach (string key in this.zy_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.zy_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.zy_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }

            this.gs_type_collate_dict.Clear();
            foreach (string key in this.gs_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.gs_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.gs_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }


            this.zd_type_collate_dict.Clear();
            foreach (string key in this.zd_collate_dict.Keys)
            {
                if (key != "")
                {
                    foreach (List<string> one in this.zd_collate_dict[key])
                    {
                        if (Convert.ToBoolean(one[1]))
                        {
                            this.zd_type_collate_dict.Add(one[0], key);
                        }
                    }
                }
            }




        }

        private void BaseRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            try
            {
                Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
                applicationObject.WindowBeforeRightClick += new Microsoft.Office.Interop.Word.ApplicationEvents4_WindowBeforeRightClickEventHandler(Application_WindowBeforeRightClick1);

                m_app = Globals.ThisAddIn.Application;
                m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");


                bgWorker.WorkerReportsProgress = true;
                bgWorker.WorkerSupportsCancellation = true;
                bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
                //bgWorker.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgessChanged);
                bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_WorkerCompleted);

                //按钮悬停提示处理
                this.button_basefun1.ScreenTip = "对指定的各种错误类型进行校对";
                this.button_basefun2.ScreenTip = "针对字词错误进行校对";
                this.button2.ScreenTip = "针对语义错误进行校对";
                //this.button3.ScreenTip = "针对常识错误进行校对";
                this.button1.ScreenTip = "针对专业术语错误进行校对";
                this.button4.ScreenTip = "针对格式错误进行校对";
                this.button5.ScreenTip = "逐一查看批注结果，并进行错误修改或撤销批注";
                this.button6.ScreenTip = "添加错误词到黑名单词库。";
                this.button3.ScreenTip = "添加正确词到白名单词库。";

                this.button9.ScreenTip = "认同所有批注的修改意见，并对原文进行修改（只对字词错误进行修改）";
                this.button10.ScreenTip = "清除所有批注";
                this.button_version.ScreenTip = "版本";
                this.button_setting.ScreenTip = "设置";
                this.button_outResult.ScreenTip = "日志";
                //（重新）初始化配置参数
                this.load_app_setting();

                //System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            }
            catch (Exception error)
            {
                Log("error:" + error.ToString());
            }

        }

        public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string start_type = e.Argument.ToString();
            int r = 0;
            if (start_type == "StartFun_all")
            {
                r = StartFun_all();
            }
            while (this.bgWorker.CancellationPending != true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        //public void bgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        //{

        //}

        public void bgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }


        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            Log(str);
            MessageBox.Show(e.Exception.Message, "系统异常提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            Log(str);
            Exception error = e.ExceptionObject as Exception;
            MessageBox.Show(error.Message, "系统异常提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");

            return sb.ToString();
        }
        private void Application_WindowBeforeRightClick1(Word.Selection Sel, ref bool Cancel)
        {
            delete_menu_v2("校对工具");
            delete_menu();
            init_menu();
        }
        const string menu_name = "智能纠错工具";

        public float FinishSize
        {
            get
            {
                return finishSize;
            }

            set
            {
                finishSize = value;
            }
        }

        //const string menu_name = "智能纠错";
        public void init_menu()
        {
            Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            //List<int> mindex = new List<int>() { 1, 94, 126, 49, 102, 103, 133, 106, 96, 151, 49, 48, 99, 122, 138 };
            List<string> mindex2 = new List<string>() { "Text", "Comment", "Spelling", "Grammar", "Grammar (2)", "Track Changes", "Linked Text" };
            foreach (var item in mindex2)
            {
                Microsoft.Office.Core.CommandBar popupCommandBar = Globals.ThisAddIn.Application.CommandBars[item];
                //Office.CommandBar popupCommandBar = applicationObject.CommandBars[item];
                Office.CommandBarPopup one = (Office.CommandBarPopup)popupCommandBar.Controls.Add(Office.MsoControlType.msoControlPopup, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                one.Caption = menu_name;

                Office.CommandBarButton commandBarButton1 = (Office.CommandBarButton)one.Controls.Add(Office.MsoControlType.msoControlButton, System.Type.Missing, System.Type.Missing, System.Type.Missing, true);
                commandBarButton1.Caption = "查看校对结果";
                commandBarButton1.Click += this.eventHandler_Look;
                Office.CommandBarButton commandBarButton2 = (Office.CommandBarButton)one.Controls.Add(Office.MsoControlType.msoControlButton, System.Type.Missing, System.Type.Missing, System.Type.Missing, true);
                commandBarButton2.Caption = "添加错误词";
                commandBarButton2.Click += this.eventHandler_AddError;


                string text = Globals.ThisAddIn.Application.Selection.Text;
                /*if (text != null && text.Length > 1)
                {
                }
                else
                {
                    commandBarButton2.Enabled = false;
                }*/
                if (item == "Comment")
                {
                    commandBarButton2.Enabled = false;

                }
                else
                {
                    commandBarButton2.Enabled = true;

                }
            }
        }
        public void delete_menu()
        {
            Microsoft.Office.Interop.Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            foreach (Office.CommandBar one in applicationObject.CommandBars)
            {
                foreach (var a in one.Controls)
                {
                    try
                    {
                        Office.CommandBarControl b = (Office.CommandBarControl)a;
                        if (b != null && b.Caption == menu_name)
                        {
                            b.Delete();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
        public void delete_menu_v2(string menuname)
        {
            Microsoft.Office.Interop.Word.Application applicationObject = Globals.ThisAddIn.Application as Word.Application;
            foreach (Office.CommandBar one in applicationObject.CommandBars)
            {
                foreach (var a in one.Controls)
                {
                    try
                    {
                        Office.CommandBarControl b = (Office.CommandBarControl)a;
                        if (b != null && b.Caption == menuname)
                        {
                            b.Delete();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }






        private void button_version_Click(object sender, RibbonControlEventArgs e)
        {
            //m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            //Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void button_setting_Click(object sender, RibbonControlEventArgs e)
        {
            Form_settings setting = new Form_settings(this);
            setting.ShowDialog();
        }

        private void button_outResult_Click(object sender, RibbonControlEventArgs e)
        {
            String Current_dir = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            //String filename = "output" + Utils.Normal.GetDateTime() + ".log";

            String filename = Path.Combine(Current_dir, "output" + Utils.Normal.GetDateTime() + ".log");
            try
            {

                FileStream fs = new FileStream(filename, FileMode.Create);
                //Log("filename:" + filename);
                //获得字节数组
                byte[] data = System.Text.Encoding.Default.GetBytes(LogMessage);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }
            Process.Start("NOTEPAD", filename);
        }

        private void getCorrectList_server(ErrResult errR, String partId)
        {
            if (this.StopThread)
                return;

            //Log("errR.data.Count" + errR.data.Count.ToString());

            lock (this)
            {
                RequestNumber -= 1;
                if (errR.data != null)
                {
                    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

                    //插入处理队列
                    foreach (var one in errR.data)
                    {
                        one.partId = partId;

                        TextParagraph textParagraph = parsePart(one);

                        int is_filter = 0;
                        foreach (var f_pos in this.filter_pos)
                        {
                            if ((f_pos.Item1 >= textParagraph.Pg_Start && f_pos.Item1 <= textParagraph.Pg_End) || (f_pos.Item2 >= textParagraph.Pg_Start && f_pos.Item2 <= textParagraph.Pg_End))
                            {
                                //Log("filter mark"+ " " + f_pos.Item1.ToString() + " " + f_pos.Item2.ToString());
                                //Log("para startPos：" + textParagraph.Start.ToString());
                                //Log("para endPos：" + textParagraph.End.ToString());
                                //Log("para text：" + textParagraph.Text.ToString());
                                //Log("startPos：" + one.startPos);
                                //Log("endPos：" + one.endPos);
                                //Log("errorWord：" + one.errorWord);
                                //Log("errorType：" + one.errorType);
                                //Log("errorTypeInfo：" + one.errorTypeInfo);
                                //Log("collateWord：" + one.collateWord);
                                //Log("");
                                is_filter = 1;
                                break;
                            }
                        }
                        if (is_filter == 1)
                            continue;

                        //Log("para startPos：" + textParagraph.Start.ToString());
                        //Log("para endPos：" + textParagraph.End.ToString());
                        //Log("para text：" + textParagraph.Text.ToString());
                        //Log("startPos：" + one.startPos.ToString());
                        //Log("endPos：" + one.endPos.ToString());
                        //Log("sen_startPos：" + one.senStartPos.ToString());
                        //Log("sem_endPos：" + one.senEndPos.ToString());
                        //Log("errorWord：" + one.errorWord);
                        //Log("collateWord：" + one.collateWord);
                        //Log("");

                        one.pid = textParagraph.Pid;
                        int start = int.Parse(one.startPos) - textParagraph.Start;
                        int end = int.Parse(one.endPos) - textParagraph.Start;
                        one.startPos = start.ToString();
                        one.endPos = end.ToString();

                        one.uuid = System.Guid.NewGuid().ToString("N");
                        one.alreadyChange = false;
                        one.AfterText = "";
                        ResultList.Add(one);
                    }
                }
            }

            //if (RequestNumber == 0)
            //{
            //    //排序
            //    var list = ResultList.OrderByDescending(t => int.Parse(t.startPos)).ToList();
            //    //开始反向处理
            //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
            //    foreach (Paragraph pg in doc.Paragraphs)
            //    {

            //        if (StopThread)
            //        {
            //            this.deleteAllComments_v2();
            //            return;
            //        }
            //        LocateTo(pg.Range);

            //        try
            //        {


            //            foreach (var one in list)
            //            {
            //                // TextParagraph textParagraph = parsePart(one);
            //                if (pg.ID == one.pid)
            //                {
            //                    int pgStart = pg.Range.Start;
            //                    int pgEnd = pg.Range.End;
            //                    //开始处理
            //                    string errComment = "";
            //                    string errText = one.errorWord;
            //                    string errorType_Info = one.errorTypeInfo;

            //                    int errStart = pgStart + int.Parse(one.startPos);
            //                    int errEnd = pgStart + int.Parse(one.endPos);

            //                    //处理全文情况
            //                    one.totalStart = errStart;
            //                    one.totalEnd = errEnd;


            //                    if (errStart >= errEnd)
            //                    {
            //                        errStart = 0;
            //                    }
            //                    int errLength = errEnd - errStart;

            //                    if (errLength > 0)
            //                    {
            //                        errComment = "●错误类型:" + one.errorTypeInfo + "\n";
            //                        errComment += "●错误字符:" + one.errorWord + "\n";

            //                        if (one.suggestType == "0")
            //                            errComment += "●修改意见:";
            //                        else if (one.suggestType == "1")
            //                            errComment += "●修改提示:";

            //                        if (one.engine == "col")
            //                        {
            //                            for (int i = 0; i < one.suggestions.Count(); ++i)
            //                            {
            //                                errComment += one.suggestions[i].collateWord;
            //                                if (this.is_display_weight == "true")
            //                                {
            //                                    errComment += ",";
            //                                    errComment += this.get_str_weight(one.suggestions[i].weight);
            //                                }
            //                                if (i < one.suggestions.Count() - 1)
            //                                    errComment += ";";
            //                            }
            //                        }
            //                        else
            //                        {
            //                            errComment += one.collateWord;
            //                            if (this.is_display_weight == "true")
            //                            {
            //                                errComment += ",";
            //                                errComment += this.get_str_weight(one.weight);
            //                            }
            //                        }



            //                        //Log(errComment);

            //                        Range range = doc.Range(errStart, errEnd);
            //                        //range.HighlightColorIndex = WdColorIndex.wdBlue;
            //                        try
            //                        {
            //                            Comment newone = doc.Comments.Add(range, errComment);
            //                            //Log("one.errorTypeInfo:" + one.errorTypeInfo.ToString());
            //                            newone.Initial = one.errorTypeInfo;
            //                            //Log(" newone.Initial:" + newone.Initial.ToString());

            //                            one.CommentLength = newone.Initial.Length + Convert.ToString(newone.Index).Length;
            //                            one.CommentId = newone.Initial + Convert.ToString(newone.Index);
            //                            newone.Author = one.errorTypeInfo;

            //                            newone.Reference.Font.Subscript = 1;
            //                            //Log("newone.Range.Font.Subscript:" + newone.Range.Font.Subscript.ToString());



            //                        }
            //                        catch (Exception ex)
            //                        {
            //                            Log("新建批注错误！" + ex.ToString());
            //                        }

            //                    }

            //                }
            //            }

            //        }
            //        catch (Exception ex)
            //        {
            //            Log("新建批注错误！" + ex.ToString());
            //        }




            //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize2));
            //    }
            //    backgroundWorker1.ReportProgress(100);
            //    ShowResult();


            //    var List = this.ResultList.OrderBy(t => t.totalStart).ToList();

            //    Log("Count：" + List.Count.ToString());
            //    //定位到附近的错误点
            //    foreach (var one in List)
            //    {
            //        Log("ResultList：");
            //        Log("partId：" + one.partId);
            //        Log("pid：" + one.pid);
            //        Log("uuid：" + one.uuid);
            //        Log("startPos：" + one.startPos);
            //        Log("endPos：" + one.endPos);
            //        Log("errorWord：" + one.errorWord);
            //        Log("errorType：" + one.errorType);
            //        Log("errorTypeInfo：" + one.errorTypeInfo);
            //        Log("collateWord：" + one.collateWord);
            //        Log("suggestType：" + one.suggestType);

            //        Log("totalStart：" + one.totalStart);
            //        Log("totalEnd：" + one.totalEnd);
            //        Log("AfterText：" + one.AfterText);
            //        Log("CommentId：" + one.CommentId);
            //        Log("alreadyChange：" + one.alreadyChange);
            //        Log("");
            //    }


            //}

        }


        public void add_comment()
        {
            var list = this.ResultList.OrderByDescending(t => int.Parse(t.startPos)).ToList();
            Log("result_num:" + list.Count.ToString());
            //开始反向处理
            m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

            Log("add_comment");
            this.result_perSize = 20.0F / doc.Paragraphs.Count;
            foreach (Paragraph pg in doc.Paragraphs)
            {

                if (this.StopThread)
                {
                    this.deleteAllComments_v2();
                    return;
                }
                LocateTo(pg.Range);

                try
                {
                    foreach (var one in list)
                    {
                        // TextParagraph textParagraph = parsePart(one);
                        if (pg.ID == one.pid)
                        {
                            int pgStart = pg.Range.Start;
                            int pgEnd = pg.Range.End;
                            //开始处理
                            string errComment = "";
                            string errText = one.errorWord;
                            string errorType_Info = one.errorTypeInfo;

                            int errStart = pgStart + int.Parse(one.startPos);
                            int errEnd = pgStart + int.Parse(one.endPos);

                            //处理全文情况
                            one.totalStart = errStart;
                            one.totalEnd = errEnd;


                            if (errStart >= errEnd)
                            {
                                errStart = 0;
                            }
                            int errLength = errEnd - errStart;

                            if (errLength > 0)
                            {
                                errComment = "●错误类型:" + one.errorTypeInfo + "\r\n";
                                errComment += "●错误字符:" + one.errorWord + "\r\n";

                                if (one.suggestType == "0")
                                    errComment += "●修改意见:";
                                else if (one.suggestType == "1")
                                    errComment += "●修改提示:";

                                if (one.engine == "col")
                                {
                                    for (int i = 0; i < one.suggestions.Count(); ++i)
                                    {
                                        errComment += one.suggestions[i].collateWord;
                                        if (this.is_display_weight)
                                        {
                                            errComment += ",";
                                            errComment += this.get_str_weight(one.suggestions[i].weight);
                                        }
                                        if (i < one.suggestions.Count() - 1)
                                            errComment += ";";
                                    }
                                }
                                else
                                {
                                    errComment += one.collateWord;
                                    if (this.is_display_weight)
                                    {
                                        errComment += ",";
                                        errComment += this.get_str_weight(one.weight);
                                    }
                                }



                                //Log(errComment);

                                Range range = doc.Range(errStart, errEnd);
                                //range.HighlightColorIndex = WdColorIndex.wdBlue;
                                try
                                {
                                    Comment newone = doc.Comments.Add(range, errComment);
                                    one.CommentLength = newone.Initial.Length + Convert.ToString(newone.Index).Length;
                                    one.CommentId = newone.Initial + Convert.ToString(newone.Index);

                                    //Range range_test = doc.Range(0, 10);
                                    //range_test.HighlightColorIndex = Word.WdColorIndex.wdYellow;
                                    //newone.Range.HighlightColorIndex = Word.WdColorIndex.wdYellow;

                                    if (this.is_display_by_classes)
                                    {
                                        newone.Initial = this.current_type_collate_dict[one.errorTypeInfo];
                                        newone.Author = this.current_type_collate_dict[one.errorTypeInfo];
                                    }
                                    else
                                    {
                                        newone.Initial = one.errorTypeInfo;
                                        newone.Author = one.errorTypeInfo;
                                    }


                                    newone.Reference.Font.Subscript = 1;
                                    //Log("newone.Range.Font.Subscript:" + newone.Range.Font.Subscript.ToString());





                                }
                                catch (Exception ex)
                                {
                                    Log("新建批注错误！" + ex.ToString());
                                }

                            }

                        }
                    }

                }
                catch (Exception ex)
                {
                    Log("新建批注错误！" + ex.ToString());
                }
                this.bgWorker.ReportProgress(Convert.ToInt16(FinishSize += this.result_perSize));


            }

            Log("add_comment over");
            this.bgWorker.ReportProgress(100);

            //ShowResult();
            //ShowResult_comment();


            //var List = this.ResultList.OrderBy(t => t.totalStart).ToList();

            //Log("Count：" + List.Count.ToString());
            ////定位到附近的错误点
            //foreach (var one in List)
            //{
            //    Log("ResultList：");
            //    Log("partId：" + one.partId);
            //    Log("pid：" + one.pid);
            //    Log("uuid：" + one.uuid);
            //    Log("startPos：" + one.startPos);
            //    Log("endPos：" + one.endPos);
            //    Log("errorWord：" + one.errorWord);
            //    Log("errorType：" + one.errorType);
            //    Log("errorTypeInfo：" + one.errorTypeInfo);
            //    Log("collateWord：" + one.collateWord);
            //    Log("suggestType：" + one.suggestType);

            //    Log("totalStart：" + one.totalStart);
            //    Log("totalEnd：" + one.totalEnd);
            //    Log("AfterText：" + one.AfterText);
            //    Log("CommentId：" + one.CommentId);
            //    Log("alreadyChange：" + one.alreadyChange);
            //    Log("");
            //}
        }

        private void getCorrectList_v2()
        {
            //处理格式校对显示
            if (this.StopThread)
                return;

            var list = ResultList.OrderByDescending(t => int.Parse(t.senStartPos)).ToList();
            m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
            foreach (Paragraph pg in doc.Paragraphs)
            {
                if (this.StopThread)
                {
                    this.deleteAllComments_v2();
                    return;
                }
                LocateTo(pg.Range);
                foreach (var one in list)
                {

                    if (one.pid == pg.ID)
                    {
                        //Log("errComment:" + "(" + one.totalStart + "," + one.totalEnd + ")");
                        //开始处理
                        string errComment = "";

                        one.startPos = one.senStartPos;
                        one.endPos = one.senEndPos;

                        int errStart = pg.Range.Start + int.Parse(one.startPos);
                        int errEnd = pg.Range.Start + int.Parse(one.endPos);

                        one.totalStart = errStart;
                        one.totalEnd = errEnd;


                        if (errStart >= errEnd)
                        {
                            errStart = 0;
                        }
                        int errLength = errEnd - errStart;

                        if (errLength > 0)
                        {

                            errComment = "●错误类型:" + one.errorTypeInfo + "\n";
                            errComment += "●错误字符:" + one.errorWord + "\n";
                            errComment += "●修改提示:" + one.collateWord;
                            if (this.is_display_weight)
                            {
                                errComment += ",";
                                errComment += this.get_str_weight(one.weight);
                            }

                            Range range = doc.Range(errStart, errEnd);
                            //range.HighlightColorIndex = WdColorIndex.wdBlue;
                            try
                            {
                                Comment newone = doc.Comments.Add(range, errComment);
                                newone.Initial = one.errorTypeInfo;
                                one.CommentLength = newone.Initial.Length + Convert.ToString(newone.Index).Length;
                                one.CommentId = newone.Initial + Convert.ToString(newone.Index);

                                newone.Reference.Font.Subscript = 1;
                                newone.Author = one.errorTypeInfo;

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
                //backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
            }

            //MessageBox.Show("校验结束，共校验出" + ResultList.Count.ToString() + "处错误", "校对结果", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //MessageBox.Show("校验结束，共校验出" + ResultList.Count.ToString() + "处错误");
        }

        private string get_str_weight(string str)
        {
            return String.Format("{0:F}", float.Parse(str));
        }
        private void getCorrectList2(ErrResult errR, String partId, string au)
        {

            if (this.StopThread)
                return;

            //Log("errR.data.Count" + errR.data.Count.ToString());

            lock (this)
            {
                RequestNumber -= 1;
                if (errR.data != null)
                {
                    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

                    //插入处理队列
                    foreach (var one in errR.data)
                    {
                        one.partId = partId;

                        TextParagraph textParagraph = parsePart(one);

                        int is_filter = 0;
                        foreach (var f_pos in this.filter_pos)
                        {
                            if ((f_pos.Item1 >= textParagraph.Pg_Start && f_pos.Item1 <= textParagraph.Pg_End) || (f_pos.Item2 >= textParagraph.Pg_Start && f_pos.Item2 <= textParagraph.Pg_End))
                            {
                                //Log("filter mark"+ " " + f_pos.Item1.ToString() + " " + f_pos.Item2.ToString());
                                //Log("para startPos：" + textParagraph.Start.ToString());
                                //Log("para endPos：" + textParagraph.End.ToString());
                                //Log("para text：" + textParagraph.Text.ToString());
                                //Log("startPos：" + one.startPos);
                                //Log("endPos：" + one.endPos);
                                //Log("errorWord：" + one.errorWord);
                                //Log("errorType：" + one.errorType);
                                //Log("errorTypeInfo：" + one.errorTypeInfo);
                                //Log("collateWord：" + one.collateWord);
                                //Log("");
                                is_filter = 1;
                                break;
                            }
                        }
                        if (is_filter == 1)
                            continue;

                        //Log("para startPos：" + textParagraph.Start.ToString());
                        //Log("para endPos：" + textParagraph.End.ToString());
                        //Log("para text：" + textParagraph.Text.ToString());
                        //Log("startPos：" + one.startPos.ToString());
                        //Log("endPos：" + one.endPos.ToString());
                        //Log("sen_startPos：" + one.senStartPos.ToString());
                        //Log("sem_endPos：" + one.senEndPos.ToString());
                        //Log("errorWord：" + one.errorWord);
                        //Log("collateWord：" + one.collateWord);
                        //Log("");

                        one.pid = textParagraph.Pid;
                        int start = int.Parse(one.startPos) - textParagraph.Start;
                        int end = int.Parse(one.endPos) - textParagraph.Start;
                        one.startPos = start.ToString();
                        one.endPos = end.ToString();

                        one.uuid = System.Guid.NewGuid().ToString("N");
                        one.alreadyChange = false;
                        one.AfterText = "";
                        ResultList.Add(one);
                    }
                }
            }

            if (RequestNumber == 0)
            {
                //排序
                var list = ResultList.OrderByDescending(t => int.Parse(t.startPos)).ToList();
                //开始反向处理
                m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
                foreach (Paragraph pg in doc.Paragraphs)
                {

                    if (this.StopThread)
                    {
                        this.deleteAllComments_v2();
                        return;
                    }
                    LocateTo(pg.Range);

                    try
                    {


                        foreach (var one in list)
                        {
                            // TextParagraph textParagraph = parsePart(one);
                            if (pg.ID == one.pid)
                            {
                                int pgStart = pg.Range.Start;
                                int pgEnd = pg.Range.End;
                                //开始处理
                                string errComment = "";
                                string errText = one.errorWord;
                                string errorType_Info = one.errorTypeInfo;

                                int errStart = pgStart + int.Parse(one.startPos);
                                int errEnd = pgStart + int.Parse(one.endPos);

                                //处理全文情况
                                one.totalStart = errStart;
                                one.totalEnd = errEnd;


                                if (errStart >= errEnd)
                                {
                                    errStart = 0;
                                }
                                int errLength = errEnd - errStart;

                                if (errLength > 0)
                                {
                                    errComment = "●错误类型:" + one.errorTypeInfo + "\n";
                                    errComment += "●错误字符:" + one.errorWord + "\n";

                                    if (one.suggestType == "0")
                                        errComment += "●修改意见:";
                                    else if (one.suggestType == "1")
                                        errComment += "●修改提示:";

                                    if (one.engine == "col")
                                    {
                                        for (int i = 0; i < one.suggestions.Count(); ++i)
                                        {
                                            errComment += one.suggestions[i].collateWord;
                                            if (this.is_display_weight)
                                            {
                                                errComment += ",";
                                                errComment += this.get_str_weight(one.suggestions[i].weight);
                                            }
                                            if (i < one.suggestions.Count() - 1)
                                                errComment += ";";
                                        }
                                    }
                                    else
                                    {
                                        errComment += one.collateWord;
                                        if (this.is_display_weight)
                                        {
                                            errComment += ",";
                                            errComment += this.get_str_weight(one.weight);
                                        }
                                    }



                                    //Log(errComment);

                                    Range range = doc.Range(errStart, errEnd);
                                    //range.HighlightColorIndex = WdColorIndex.wdBlue;
                                    try
                                    {
                                        Comment newone = doc.Comments.Add(range, errComment);
                                        //Log("one.errorTypeInfo:" + one.errorTypeInfo.ToString());
                                        newone.Initial = one.errorTypeInfo;
                                        //Log(" newone.Initial:" + newone.Initial.ToString());

                                        one.CommentLength = newone.Initial.Length + Convert.ToString(newone.Index).Length;
                                        one.CommentId = newone.Initial + Convert.ToString(newone.Index);
                                        newone.Author = one.errorTypeInfo;

                                        newone.Reference.Font.Subscript = 1;
                                        //Log("newone.Range.Font.Subscript:" + newone.Range.Font.Subscript.ToString());



                                    }
                                    catch (Exception ex)
                                    {
                                        Log("新建批注错误！" + ex.ToString());
                                    }

                                }

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Log("新建批注错误！" + ex.ToString());
                    }



                }
                //ShowResult();


                var List = this.ResultList.OrderBy(t => t.totalStart).ToList();

                Log("Count：" + List.Count.ToString());
                //定位到附近的错误点
                foreach (var one in List)
                {
                    Log("ResultList：");
                    Log("partId：" + one.partId);
                    Log("pid：" + one.pid);
                    Log("uuid：" + one.uuid);
                    Log("startPos：" + one.startPos);
                    Log("endPos：" + one.endPos);
                    Log("errorWord：" + one.errorWord);
                    Log("errorType：" + one.errorType);
                    Log("errorTypeInfo：" + one.errorTypeInfo);
                    Log("collateWord：" + one.collateWord);
                    Log("suggestType：" + one.suggestType);

                    Log("totalStart：" + one.totalStart);
                    Log("totalEnd：" + one.totalEnd);
                    Log("AfterText：" + one.AfterText);
                    Log("CommentId：" + one.CommentId);
                    Log("alreadyChange：" + one.alreadyChange);
                    Log("");
                }


            }
        }
        private void ShowResult()
        {
            Log("统计结果！");
            Dictionary<string, int> list = new Dictionary<string, int>();

            foreach (var a in ResultList)
            {
                if (list.ContainsKey(a.errorTypeInfo))
                {
                    list[a.errorTypeInfo] += 1;
                }
                else
                {
                    list.Add(a.errorTypeInfo, 1);
                }
            }

            string msg = "校对结束，共识别出" + ResultList.Count.ToString() + "处错误\r\n";
            list.OrderByDescending(x => x.Value);
            foreach (var item in list)
            {
                msg += item.Key + " 错误共：" + item.Value.ToString() + "处\r\n";
            }
            //MessageBox.Show(msg);
            Log(msg);
            MessageBox.Show(msg, "校对结果", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }


        private void ShowResult_comment()
        {
            Log("统计结果！");
            Dictionary<string, int> result_list = new Dictionary<string, int>();
            //m_app = Globals.ThisAddIn.Application;
            //m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            Document doc = m_app.ActiveDocument;
            //string.Join(" ", this.current_collate_dict.Keys.ToList());
            Log(string.Join(" ", this.current_collate_dict.Keys.ToList()));
            Log("Comment num:" + doc.Comments.Count.ToString());
            foreach (Comment one in doc.Comments)
            {
                string str = one.Author.ToString();
                //Log(str);
                if (result_list.ContainsKey(str))
                {
                    result_list[str] += 1;
                }
                else
                {
                    if (this.is_display_by_classes)
                    {
                        if (this.current_collate_dict.ContainsKey(str))
                        {
                            result_list.Add(str, 1);
                        }
                    }
                    else
                    {
                        if (this.type_define_dict.ContainsKey(str))
                        {
                            result_list.Add(str, 1);
                        }

                    }

                }
            }
            //Dictionary<string, int> list = new Dictionary<string, int>();

            //foreach (var a in ResultList)
            //{
            //    if (list.ContainsKey(a.errorTypeInfo))
            //    {
            //        list[a.errorTypeInfo] += 1;
            //    }
            //    else
            //    {
            //        list.Add(a.errorTypeInfo, 1);
            //    }
            //}

            int error_num = 0;
            string msg = "";
            result_list.OrderByDescending(x => x.Value);
            foreach (var item in result_list)
            {
                error_num += item.Value;
                msg += item.Key + " 错误共：" + item.Value.ToString() + "处\r\n";
            }

            msg = "校对结束，共识别出" + error_num + "处错误\r\n" + msg;
            MessageBox.Show(msg, "校对结果", MessageBoxButtons.OK);
            Log(msg);
            //MessageBox.Show(msg, "校对结果", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
        /// <summary>
        /// 定位到区域
        /// </summary>
        /// <param name="range">区域
        /// </param>
        public void LocateTo(Range range)
        {
            object goFunc = WdGoToDirection.wdGoToFirst;
            object goToPage = WdGoToItem.wdGoToPage;
            object pageNum = range.Information[WdInformation.wdActiveEndPageNumber];
            m_app.Selection.GoTo(ref goToPage, ref goFunc, ref pageNum);

            object goToLine = WdGoToItem.wdGoToLine;
            object goNext = WdGoToDirection.wdGoToNext;
            object lineNum = (int)range.Information[WdInformation.wdFirstCharacterLineNumber] - 1;
            m_app.Selection.GoTo(ref goToLine, ref goNext, ref lineNum);
        }
        private bool checkblank(string text)
        {
            bool res = false;
            if (text.Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("/", string.Empty).Length == 0)
            {
                res = true;
            }
            return res;
        }


        // Add by Liuc 创建片段
        private void createPart(Paragraphs paragraphs)
        {

            textParagraphs = new List<TextParagraph>();
            textParts = new Dictionary<string, TextPart>();

            string text = "";
            int start = 0;
            int startParagraph = 0;
            int pgCount = 0;
            int partId = 0;
            this.pre_perSize = 10.0F / paragraphs.Count;
            foreach (Paragraph pg in paragraphs)
            {
                string pid = "text_" + pgCount.ToString();
                pg.ID = pid;

                TextParagraph paragraph = new TextParagraph();
                paragraph.Id = pgCount;
                paragraph.Pid = pid;
                paragraph.Text = pg.Range.Text;
                paragraph.Length = paragraph.Text.Length;
                paragraph.Start = start;
                //paragraph.Start = pg.Range.Start;
                paragraph.End = start + paragraph.Length;

                paragraph.Pg_Start = pg.Range.Start;
                paragraph.Pg_End = pg.Range.End;

                //Log("paragraph.Pid:" + paragraph.Pid.ToString());
                //Log("paragraph.Text:" + paragraph.Text.ToString());
                //Log("paragraph.Start:" + paragraph.Start.ToString());
                //Log("paragraph.End:" + paragraph.End.ToString());
                //Log("paragraph.Pg_Start:" + paragraph.Pg_Start.ToString());
                //Log("paragraph.Pg_End:" + paragraph.Pg_End.ToString());
                //Log("");

                textParagraphs.Add(paragraph);

                pgCount++;
                text += paragraph.Text;
                start = paragraph.End;

                if (start >= this.maxCount)
                {
                    TextPart textPart = new TextPart();
                    textPart.Id = partId;
                    textPart.Text = text;
                    textPart.Length = textPart.Text.Length;
                    textPart.StartParagraph = startParagraph;
                    textPart.EndParagraph = pgCount - 1;
                    textParts.Add(partId.ToString(), textPart);

                    partId++;
                    text = "";
                    start = 0;
                    startParagraph = pgCount;
                }

                this.bgWorker.ReportProgress(Convert.ToInt16(FinishSize += this.pre_perSize));
            }

            if (start != 0)
            {
                TextPart textPart = new TextPart();
                textPart.Id = partId;
                textPart.Text = text;
                textPart.Length = textPart.Text.Length;
                textPart.StartParagraph = startParagraph;
                textPart.EndParagraph = pgCount - 1;
                textParts.Add(partId.ToString(), textPart);
            }
        }
        // Add by Liuc 从片段中解析命中段落
        public TextParagraph parsePart(ErrInfo one)
        {
            try
            {
                int oneStart = int.Parse(one.startPos);
                TextPart textPart = textParts[one.partId];
                int startParagraph = textPart.StartParagraph;
                int endParagraph = textPart.EndParagraph;
                for (int i = startParagraph; i <= endParagraph; i++)
                {
                    TextParagraph textParagraph = textParagraphs[i];
                    if (oneStart >= textParagraph.Start && oneStart < textParagraph.End)
                    {
                        return textParagraph;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        //过滤col和dl中的重复部分
        public ErrResult filter_errR(ErrResult errR)
        {
            List<ErrInfo> dl_errRlist = new List<ErrInfo>();
            List<ErrInfo> date_errRlist = new List<ErrInfo>();
            ErrResult new_errR = new ErrResult();


            if (errR.data == null || errR.data.Count < 0)
            {
                return errR;
            }
            else
            {
                new_errR.code = errR.code;
                new_errR.msg = errR.msg;
                new_errR.data = new List<ErrInfo>();
                Log("errR.data:" + errR.data.Count.ToString());
                foreach (ErrInfo item in errR.data)
                {
                    Log("all:");
                    Log("item.errorTypeInfo:" + item.errorTypeInfo.ToString());
                    Log("item.errorWord:" + item.errorWord.ToString());
                    Log("item.collateWord:" + item.collateWord.ToString());


                    if (!this.current_type_collate_dict.ContainsKey(item.errorTypeInfo))
                    {
                        Log("type filter!");
                        continue;
                    }


                    if (item.engine == "col")
                    {
                        for (int i = item.suggestions.Count - 1; i >= 0; i--)
                        {
                            if (float.Parse(item.suggestions[i].weight) <= float.Parse(this.type_define_dict[item.errorTypeInfo][1]))
                            {
                                item.suggestions.Remove(item.suggestions[i]);
                            }

                        }
                        if (item.suggestions.Count > 0)
                            new_errR.data.Add(item);
                    }
                    else if (item.engine == "dl")
                    {
                        if (float.Parse(item.weight) > float.Parse(this.type_define_dict[item.errorTypeInfo][1]))
                            dl_errRlist.Add(item);
                    }
                    else if (item.engine == "date")
                    {
                        if (float.Parse(item.weight) > float.Parse(this.type_define_dict[item.errorTypeInfo][1]))
                            date_errRlist.Add(item);
                    }

                    Log("");
                }
                //add dl
                foreach (ErrInfo dl_item in dl_errRlist)
                {
                    int repeat_index = -1;

                    for (int i = 0; i < new_errR.data.Count; i++)
                    {
                        if ((int.Parse(dl_item.startPos) > int.Parse(new_errR.data[i].startPos) && int.Parse(dl_item.startPos) < int.Parse(new_errR.data[i].endPos)) || (int.Parse(dl_item.endPos) > int.Parse(new_errR.data[i].startPos) && int.Parse(dl_item.endPos) < int.Parse(new_errR.data[i].endPos)))
                        {
                            repeat_index = i;
                            break;
                        }
                    }

                    //Log("repeat_index:" + repeat_index);
                    if (repeat_index != -1)
                    {
                        if (float.Parse(new_errR.data[repeat_index].weight) < float.Parse(dl_item.weight))
                        {
                            new_errR.data[repeat_index] = dl_item;
                        }

                    }
                    else
                    {
                        new_errR.data.Add(dl_item);
                    }
                }

                //add date
                foreach (ErrInfo date_item in date_errRlist)
                {
                    int repeat_index = -1;

                    for (int i = 0; i < new_errR.data.Count; i++)
                    {
                        if ((int.Parse(date_item.startPos) > int.Parse(new_errR.data[i].startPos) && int.Parse(date_item.startPos) < int.Parse(new_errR.data[i].endPos)) || (int.Parse(date_item.endPos) > int.Parse(new_errR.data[i].startPos) && int.Parse(date_item.endPos) < int.Parse(new_errR.data[i].endPos)))
                        {
                            //Log("repeat:");
                            //Log("new_errR.data[i].errorWord:" + new_errR.data[i].errorWord.ToString());
                            //Log("new_errR.data[i].collateWord:" + new_errR.data[i].collateWord.ToString());
                            //Log("post:" + new_errR.data[i].startPos.ToString() + " " + new_errR.data[i].endPos.ToString());

                            //Log("date_item.errorWord:" + date_item.errorWord.ToString());
                            //Log("date_item.collateWord:" + date_item.collateWord.ToString());
                            //Log("post:" + date_item.startPos.ToString() + " " + date_item.endPos.ToString());

                            //Log("");

                            repeat_index = i;
                            break;
                        }
                    }

                    //Log("repeat_index:" + repeat_index);
                    if (repeat_index != -1)
                    {
                        if (float.Parse(new_errR.data[repeat_index].weight) < float.Parse(date_item.weight))
                        {
                            new_errR.data[repeat_index] = date_item;
                        }

                    }
                    else
                    {
                        new_errR.data.Add(date_item);
                    }
                }


            }
            return new_errR;
        }

        public int StartFun_all()
        {

            Log("StartFun_all");
            m_app = Globals.ThisAddIn.Application;
            m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
            this.deleteAllComments_v2();

            this.ResultList = new List<ErrInfo>();

            if (this.current_type_collate_dict.ContainsKey("序号使用不当"))
                collate_format();

            this.filter_pos.Clear();
            Bookmarks marks = doc.Bookmarks;
            foreach (Bookmark mark in marks)
            {
                //Log("mark name:" + mark.Name.ToString() + " " + mark.Range.Start.ToString() + ", " + mark.Range.End.ToString());
                //if (mark.Range.Start != mark.Range.End)
                //    Log("mark text:" + mark.Range.Text.ToString());
                //else
                //    Log("mark text:");
                //Log("");
                if (this.filter_mark_list.Contains(mark.Name))
                {
                    this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
                }
            }

            // Add by Liuc
            createPart(doc.Paragraphs);
            int partCount = textParts.Count;

            this.collate_perSize = 70.0F / partCount;
            //Log("段落拆分：" + perSize + "段");

            RequestNumber = partCount;
            int blankpart = 0;


            List<System.Threading.Tasks.Task> task_list = new List<System.Threading.Tasks.Task>();
            foreach (TextPart part in textParts.Values)
            {
                string text = part.Text;
                if (checkblank(text))
                {
                    Log("空段落过滤");
                    blankpart += 1;
                    //空段落数量与分段数量一致的情况下
                    if (blankpart == textParts.Count)
                    {
                        //关闭对话框并return
                        this.bgWorker.ReportProgress(100);
                        Log("无内容!");
                        //MessageBox.Show("无内容，校对完成");
                        return 0;
                    }
                    RequestNumber -= 1;
                    continue;
                }

                int partId = part.Id;
                int partStartParagraph = part.StartParagraph;
                int partEndParagraph = part.EndParagraph;


                System.Threading.Tasks.Task aTask = taskFac.StartNew(() =>
                {
                    try
                    {

                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic.Add("text", text);
                        dic.Add("mode", "0");

                        Log("校对开始！");
                        Log("段落：" + partId + ",当前请求地址：" + this.collate_url);
                        Log("当前请求参数mode：" + dic["mode"] + "(综合校对)");
                        Log("token:" + this.token.ToString());
                        var t1 = DateTime.Now;
                        var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);
                        if (res.Result != null)
                        {
                            Log("段落：" + partId + ",当前请求返回结果：" + res.Result);


                            ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

                            ErrResult new_errR = filter_errR(errR);
                            //ErrResult new_errR = errR;
                            getCorrectList_server(new_errR, partId.ToString());


                            var t2 = DateTime.Now;
                            Log("段落：" + partId + ",当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
                        }
                        else
                        {
                            Log("接口响应异常！");
                            //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }

                    }
                    catch (Exception ex)
                    {
                        Log("系统错误！");
                        Log(ex.ToString());
                    }

                    this.bgWorker.ReportProgress(Convert.ToInt16(FinishSize += this.collate_perSize));

                });
                //this.bgWorker.ReportProgress(Convert.ToInt16(FinishSize += this.collate_perSize));

                task_list.Add(aTask);
            }

            Log("FinishSize:" + FinishSize.ToString());

            taskFac.ContinueWhenAll(task_list.ToArray(), r =>
                {
                    Log("校对结果处理！");
                    add_comment();

                });
            return 0;


        }

        ///// <summary>
        ///// 综合校对
        ///// </summary>
        //public void StartFun1()
        //{

        //    ResultList = new List<ErrInfo>();

        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();

        //    this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();
        //    this.filter_pos.Clear();
        //    Bookmarks marks = doc.Bookmarks;
        //    foreach (Bookmark mark in marks)
        //    {
        //        //Log("mark name:" + mark.Name.ToString() + " " + mark.Range.Start.ToString() + ", " + mark.Range.End.ToString());
        //        //if (mark.Range.Start != mark.Range.End)
        //        //    Log("mark text:" + mark.Range.Text.ToString());
        //        //else
        //        //    Log("mark text:");
        //        //Log("");
        //        if (this.filter_mark_list.Contains(mark.Name))
        //        {
        //            this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
        //        }
        //    }

        //    // Add by Liuc
        //    createPart(doc.Paragraphs);
        //    int partCount = textParts.Count;

        //    perSize = 70.0F / partCount;
        //    littleperSize = 10.0F / partCount;
        //    Log("段落拆分：" + perSize + "段");

        //    RequestNumber = partCount;
        //    int blankpart = 0;
        //    foreach (TextPart part in textParts.Values)
        //    {
        //        string text = part.Text;
        //        if (checkblank(text))
        //        {
        //            Log("空段落过滤");
        //            blankpart += 1;
        //            //空段落数量与分段数量一致的情况下
        //            if(blankpart == textParts.Count)
        //            {
        //                //关闭对话框并return
        //                progressform.Close();
        //                MessageBox.Show("无内容，校对完成");
        //                return;
        //            }
        //            RequestNumber -= 1;
        //            continue;
        //        }

        //        int partId = part.Id;
        //        int partStartParagraph = part.StartParagraph;
        //        int partEndParagraph = part.EndParagraph;




        //        taskFac.StartNew(() =>
        //        {


        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", text);
        //            dic.Add("mode", "0");

        //            Log("段落：" + partId + ",当前请求地址：" + this.collate_url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(综合校对)");
        //            Log("token:" + this.token.ToString());

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);
        //            if (res.Result != null)
        //            {
        //                Log("段落：" + partId + ",当前请求返回结果：" + res.Result);
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));


        //                try
        //                {
        //                    ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

        //                    ErrResult new_errR = filter_errR(errR, threshold, this.zh_filter_list);
        //                    //ErrResult new_errR = errR;
        //                    getCorrectList2(new_errR, partId.ToString(), "综合校对");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Log("当前请求服务器处理异常");
        //                    Log("error: " + ex.ToString());
        //                    //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        //                }
        //                var t2 = DateTime.Now;
        //                Log("段落：" + partId + ",当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }
        //            else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }
        //        });
        //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize));
        //    }


        //}

        private void println(string v)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// 字词校对
        ///// </summary>
        //public void StartFun2()
        //{
        //    ResultList = new List<ErrInfo>();
        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();

        //    this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();
        //    this.filter_pos.Clear();
        //    Bookmarks marks = doc.Bookmarks;
        //    foreach (Bookmark mark in marks)
        //    {
        //        if (this.filter_mark_list.Contains(mark.Name))
        //        {
        //            this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
        //        }
        //    }


        //    // Add by Liuc
        //    createPart(doc.Paragraphs);
        //    int partCount = textParts.Count;

        //    perSize = 70.0F / partCount;
        //    littleperSize = 10.0F / partCount;
        //    Log("段落拆分：" + perSize + "段");

        //    RequestNumber = partCount;
        //    int blankpart = 0;
        //    foreach (TextPart part in textParts.Values)
        //    {
        //        string text = part.Text;
        //        if (checkblank(text))
        //        {
        //            Log("空段落过滤");
        //            blankpart += 1;
        //            //空段落数量与分段数量一致的情况下
        //            if (blankpart == textParts.Count)
        //            {
        //                //关闭对话框并return
        //                progressform.Close();
        //                MessageBox.Show("无内容，校对完成");
        //                return;
        //            }
        //            RequestNumber -= 1;
        //            continue;
        //        }

        //        int partId = part.Id;
        //        int partStartParagraph = part.StartParagraph;
        //        int partEndParagraph = part.EndParagraph;

        //        //String result = await Utils.HttpUtils.PostData("");
        //        taskFac.StartNew(() =>
        //        {

        //            float threshold = float.Parse(ConfigurationManager.AppSettings["zc_weight"]);

        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", text);
        //            dic.Add("mode", "1");

        //            Log("当前请求地址：" + this.collate_url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(字词校对)");

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);
        //            if (res != null)
        //            {
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

        //                ErrResult new_errR = filter_errR(errR, threshold, this.zc_filter_list);

        //                Log("当前请求返回结果：" + res.Result);
        //                getCorrectList2(new_errR, partId.ToString(), "字词校对");
        //                var t2 = DateTime.Now;
        //                Log("当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }
        //        });
        //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize));
        //    }
        //}
        //语义校对
        //public void StartFun3()
        //{
        //    ResultList = new List<ErrInfo>();

        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();

        //    this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();
        //    this.filter_pos.Clear();
        //    Bookmarks marks = doc.Bookmarks;
        //    foreach (Bookmark mark in marks)
        //    {
        //        if (this.filter_mark_list.Contains(mark.Name))
        //        {
        //            this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
        //        }
        //    }



        //    // Add by Liuc
        //    createPart(doc.Paragraphs);
        //    int partCount = textParts.Count;

        //    perSize = 70.0F / partCount;
        //    littleperSize = 10.0F / partCount;
        //    Log("段落拆分：" + perSize + "段");

        //    RequestNumber = partCount;
        //    int blankpart = 0;
        //    foreach (TextPart part in textParts.Values)
        //    {
        //        string text = part.Text;
        //        if (checkblank(text))
        //        {
        //            Log("空段落过滤");
        //            blankpart += 1;
        //            //空段落数量与分段数量一致的情况下
        //            if (blankpart == textParts.Count)
        //            {
        //                //关闭对话框并return
        //                progressform.Close();
        //                MessageBox.Show("无内容，校对完成");
        //                return;
        //            }
        //            RequestNumber -= 1;
        //            continue;
        //        }
        //        int partId = part.Id;
        //        int partStartParagraph = part.StartParagraph;
        //        int partEndParagraph = part.EndParagraph;

        //        Log("当前处理段落ID：" + partId);
        //        Log("当前处理段落内容：" + text);
        //        //String result = await Utils.HttpUtils.PostData("");
        //        taskFac.StartNew(() =>
        //        {
        //            float threshold = float.Parse(ConfigurationManager.AppSettings["yy_weight"]);

        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", text);
        //            dic.Add("mode", "8");

        //            Log("当前请求地址：" + this.collate_url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(语义校对)");

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);
        //            if (res != null)
        //            {
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

        //                ErrResult new_errR = filter_errR(errR, threshold, this.yy_filter_list);

        //                Log("当前请求返回结果：" + res.Result);
        //                getCorrectList2(new_errR, partId.ToString(), "语义校对");
        //                var t2 = DateTime.Now;
        //                Log("当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }
        //        });

        //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize));
        //    }
        //}
        ////常识校对
        //public void StartFun4()
        //{
        //    ResultList = new List<ErrInfo>();
        //    RequestNumber = 0;
        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();
        //    // Add by Liuc
        //    createPart(doc.Paragraphs);
        //    int partCount = textParts.Count;

        //    perSize = 70.0F / partCount;
        //    littleperSize = 10.0F / partCount;
        //    Log("段落拆分：" + perSize + "段");
        //    int blankpart = 0;
        //    foreach (TextPart part in textParts.Values)
        //    {
        //        string text = part.Text;
        //        if (checkblank(text))
        //        {
        //            Log("空段落过滤");
        //            blankpart += 1;
        //            //空段落数量与分段数量一致的情况下
        //            if (blankpart == textParts.Count)
        //            {
        //                //关闭对话框并return
        //                progressform.Close();
        //                MessageBox.Show("无内容，校对完成");
        //                return;
        //            }
        //            continue;
        //        }

        //        int partId = part.Id;
        //        int partStartParagraph = part.StartParagraph;
        //        int partEndParagraph = part.EndParagraph;
        //        RequestNumber += 1;
        //        Log("当前处理段落ID：" + part.Id);
        //        Log("当前处理段落内容：" + text);
        //        //String result = await Utils.HttpUtils.PostData("");
        //        taskFac.StartNew(() =>
        //        {
        //            String url = ConfigurationManager.AppSettings["ServerUrl"] + "proxy/collate/text";
        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", text);
        //            dic.Add("mode", "17");
        //            Log("当前请求地址：" + url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(常识校对)");

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(url, dic);
        //            if (res != null)
        //            {
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
        //                Log("当前请求返回结果：" + res.Result);
        //                getCorrectList2(errR, partId.ToString());
        //                var t2 = DateTime.Now;
        //                Log("当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //            }
        //        });
        //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize));
        //    }
        //}

        ////专业术语校对
        //public void StartFun4()
        //{
        //    ResultList = new List<ErrInfo>();

        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();

        //    this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();
        //    this.filter_pos.Clear();
        //    Bookmarks marks = doc.Bookmarks;
        //    foreach (Bookmark mark in marks)
        //    {
        //        if (this.filter_mark_list.Contains(mark.Name))
        //        {
        //            this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
        //        }
        //    }



        //    // Add by Liuc
        //    createPart(doc.Paragraphs);
        //    int partCount = textParts.Count;

        //    perSize = 70.0F / partCount;
        //    littleperSize = 10.0F / partCount;
        //    Log("段落拆分：" + perSize + "段");

        //    RequestNumber = partCount;
        //    int blankpart = 0;
        //    foreach (TextPart part in textParts.Values)
        //    {
        //        string text = part.Text;
        //        if (checkblank(text))
        //        {
        //            Log("空段落过滤");
        //            blankpart += 1;
        //            //空段落数量与分段数量一致的情况下
        //            if (blankpart == textParts.Count)
        //            {
        //                //关闭对话框并return
        //                progressform.Close();
        //                MessageBox.Show("无内容，校对完成");
        //                return;
        //            }
        //            RequestNumber -= 1;
        //            continue;
        //        }
        //        int partId = part.Id;
        //        int partStartParagraph = part.StartParagraph;
        //        int partEndParagraph = part.EndParagraph;

        //        Log("当前处理段落ID：" + partId);
        //        Log("当前处理段落内容：" + text);
        //        //String result = await Utils.HttpUtils.PostData("");
        //        taskFac.StartNew(() =>
        //        {

        //            float threshold = float.Parse(ConfigurationManager.AppSettings["zy_weight"]);

        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", text);
        //            dic.Add("mode", "18");

        //            Log("当前请求地址：" + this.collate_url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(专业术语校对)");

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(this.collate_url, dic, token);
        //            if (res != null)
        //            {
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

        //                ErrResult new_errR = filter_errR(errR, threshold, this.zy_filter_list);

        //                Log("当前请求返回结果：" + res.Result);
        //                getCorrectList2(new_errR, partId.ToString(), "专业术语校对");
        //                var t2 = DateTime.Now;
        //                Log("当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }
        //            else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }
        //        });

        //        backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += littleperSize));
        //    }
        //}

        public void collate_format()
        {
            m_app = Globals.ThisAddIn.Application;
            m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

            int pgCount = 0;
            GSThread thread = new GSThread();
            SpecThread thread1 = new SpecThread();
            thread1.init_speclist();

            foreach (Paragraph pg in doc.Paragraphs)
            {
                pg.ID = "text_" + pgCount.ToString();
                pgCount += 1;
                //Log("当前处理段落ID：" + pg.ID);
                string pgText = pg.Range.Text;
                //Log("pg.Range.Start：" + pg.Range.Start.ToString());
                //Log("当前处理段落内容：" + pgText);

                //Log("");


                thread.setP(pg.ID, pg.Range.Start, pgText);
                thread1.setP(pg.ID, pg.Range.Start, pgText);

            }
            this.ResultList.AddRange(thread.CheckResult());
            this.ResultList.AddRange(thread1.CheckResult());

        }

        ////格式校对
        //public void StartFun5()
        //{

        //    ResultList = new List<ErrInfo>();

        //    RequestNumber = 0;
        //    m_app = Globals.ThisAddIn.Application;
        //    m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
        //    Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;
        //    this.deleteAllComments_v2();
        //    int pgCount = 0;
        //    perSize = 80.0F / doc.Paragraphs.Count;
        //    Log("段落拆分：" + perSize + "段");
        //    GSThread thread = new GSThread();
        //    SpecThread thread1 = new SpecThread();
        //    thread1.init_speclist();


        //    //Bookmarks marks_ = doc.Bookmarks;
        //    //foreach (Bookmark mark in marks_)
        //    //{
        //        //Log("mark name:" + mark.Name.ToString() + " " + mark.Range.Start.ToString() + ", " + mark.Range.End.ToString());
        //        //if (mark.Range.Start != mark.Range.End)
        //        //    Log("mark text:" + mark.Range.Text.ToString());
        //        //else
        //        //    Log("mark text:");
        //        //Log("");


        //    //}

        //    foreach (Paragraph pg in doc.Paragraphs)
        //    {
        //        Log("pg.ID: " + pg.ID);
        //        Log("pg.Range: " + pg.Range.Start.ToString() + ", " + pg.Range.End.ToString());
        //        Log("pg.Range.Text: " + pg.Range.Text.ToString());
        //        Bookmarks _marks = pg.Range.Bookmarks;
        //        foreach (Bookmark mark in _marks)
        //        {
        //            Log("mark name:" + mark.Name.ToString() + " " + mark.Range.Start.ToString() + ", " + mark.Range.End.ToString());
        //            if (mark.Range.Start != mark.Range.End)
        //                Log("mark text:" + mark.Range.Text.ToString());
        //            else
        //                Log("mark text:");
        //            Log("");

        //        }
        //        Log("");

        //    }

        //    this.filter_mark_list = ConfigurationManager.AppSettings["filter_marks"].Split(';').ToList();
        //    this.filter_pos.Clear();

        //    Bookmarks marks = doc.Bookmarks;
        //    foreach (Bookmark mark in marks)
        //    {
        //        Log("mark name:" + mark.Name.ToString() + " " + mark.Range.Start.ToString() + ", " + mark.Range.End.ToString());
        //        if (mark.Range.Start != mark.Range.End)
        //            Log("mark text:" + mark.Range.Text.ToString());
        //        else
        //            Log("mark text:" );
        //        Log("");
        //        if (this.filter_mark_list.Contains(mark.Name))
        //        {
        //            this.filter_pos.Add(Tuple.Create(mark.Start, mark.End));
        //        }
        //    }

        //    float threshold = float.Parse(ConfigurationManager.AppSettings["gs_weight"]);
        //    taskFac.StartNew(() =>
        //    {
        //        foreach (Paragraph pg in doc.Paragraphs)
        //        {
        //            backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //            pg.ID = "text_" + pgCount.ToString();
        //            pgCount += 1;
        //            Log("当前处理段落ID：" + pg.ID);
        //            string pgText = pg.Range.Text;
        //            //Log("pg.Range.Start：" + pg.Range.Start.ToString());
        //            Log("当前处理段落内容：" + pgText);

        //            //Log("");

        //            int is_filter = 0;
        //            foreach (var f_pos in this.filter_pos)
        //            {
        //                if ((f_pos.Item1 >= pg.Range.Start && f_pos.Item1 <= pg.Range.End) || (f_pos.Item2 >= pg.Range.Start && f_pos.Item2 <= pg.Range.End))
        //                {
        //                    is_filter = 1;
        //                    break;
        //                }
        //            }
        //            if (is_filter == 1)
        //                continue;



        //            Dictionary<string, object> dic = new Dictionary<string, object>();
        //            dic.Add("text", pgText);
        //            dic.Add("mode", "3");

        //            Log("当前请求地址：" + this.collate_url);
        //            Log("当前请求参数mode：" + dic["mode"] + "(格式校对)");

        //            var t1 = DateTime.Now;
        //            var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);
        //            if (res != null)
        //            {
        //                ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);

        //                if (errR.data != null && errR.data.Count > 0)
        //                {
        //                    foreach (var one in errR.data)
        //                    {
        //                        one.uuid = System.Guid.NewGuid().ToString("N");
        //                        one.alreadyChange = false;
        //                        one.AfterText = "";
        //                        one.pid = pg.ID;
        //                        if (this.gs_filter_list.Contains(one.errorType))
        //                            continue;
        //                        if (float.Parse(one.suggestions[0].weight) <= threshold)
        //                            continue;

        //                        ResultList.Add(one);
        //                    }
        //                    //ResultList.AddRange(errR.data);
        //                }
        //                Log("当前请求返回结果：" + res.Result);
        //                var t2 = DateTime.Now;
        //                Log("当前请求耗时：" + (t2 - t1).Milliseconds.ToString() + "毫秒");
        //            }
        //            else
        //            {
        //                Log("当前请求服务器处理异常");
        //                backgroundWorker1.ReportProgress(Convert.ToInt16(FinishSize += perSize));
        //                //MessageBox.Show("服务器处理异常,请重试！", "异常提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //            }

        //            thread.setP(pg.ID, pg.Range.Start, pgText);
        //            thread1.setP(pg.ID, pg.Range.Start, pgText);

        //        }
        //        if (!this.gs_filter_list.Contains("100"))
        //        {
        //            ResultList.AddRange(thread.CheckResult());
        //            ResultList.AddRange(thread1.CheckResult());
        //        }
        //        Log("当前请求返回结果：" + JsonConvert.SerializeObject(ResultList));
        //        this.getCorrectList_v2();

        //    });


        //}


        private void collate__all()
        {
            this.collate_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/text";
            this.login_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";
            this.login_username = ConfigurationManager.AppSettings["username"];
            this.login_code = ConfigurationManager.AppSettings["code"];

            //Log("login_url:" + login_url);
            string cstring = Utils.URLCheck.CheckUrl(this.login_url);
            if (cstring.Length == 0)
            {
                bool is_login = backgroundLogIn();
                if (is_login)
                {
                    this.StopThread = false;
                    this.FinishSize = 0.0F;

                    taskFac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(this.MaxThread));
                    Form_progress progressform = new Form_progress(this, "StartFun_all");
                    progressform.ShowDialog();
                    ShowResult_comment();
                    this.FinishSize = 0.0F;

                }
                else
                {
                    FormLogIn();
                }

            }
            else
            {
                MessageBox.Show(cstring);
            }
        }

        //综合校对
        private void button_basefun1_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.zh_collate_dict;
            this.current_type_collate_dict = this.zh_type_collate_dict;
            collate__all();
            //this.collate_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/text";
            //this.login_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";
            //this.login_username = ConfigurationManager.AppSettings["username"];
            //this.login_code = ConfigurationManager.AppSettings["code"];

            ////Log("login_url:" + login_url);
            //string cstring = Utils.URLCheck.CheckUrl(this.login_url);
            //if (cstring.Length == 0)
            //{
            //    bool is_login = backgroundLogIn();
            //    if (is_login)
            //    {
            //        //综合校对
            //        this.StopThread = false;
            //        this.FinishSize = 0.0F;
            //        this.current_collate_dict = this.zh_collate_dict;
            //        this.current_type_collate_dict = this.zh_type_collate_dict;
            //        taskFac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(this.MaxThread));
            //        Form_progress progressform = new Form_progress(this, "StartFun_all");
            //        progressform.ShowDialog();
            //        ShowResult_comment();
            //        this.FinishSize = 0.0F;

            //    }
            //    else
            //    {
            //        FormLogIn();
            //    }

            //}
            //else
            //{
            //    MessageBox.Show(cstring);
            //}

        }

        //字词校对
        private void button_basefun2_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.zc_collate_dict;
            this.current_type_collate_dict = this.zc_type_collate_dict;
            collate__all();
        }

        //语义校对
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.yy_collate_dict;
            this.current_type_collate_dict = this.yy_type_collate_dict;
            collate__all();
        }
        ////常识校对
        //private void button3_Click(object sender, RibbonControlEventArgs e)
        //{
        //    string cstring = Utils.URLCheck.CheckUrl(ConfigurationManager.AppSettings["ServerUrl"] + "proxy/collate/text");
        //    if (cstring.Length == 0)
        //    {

        //        taskFac = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(MaxThread));
        //        mTimer = new myTimer(this);
        //        FinishSize = 0.0F;
        //        StopThread = false;
        //        this.LogMessage = "";
        //        progressform = new Form_progress(this, "StartFun4");
        //        progressform.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show(cstring);
        //    }
        //}

        //专业术语校对
        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.zy_collate_dict;
            this.current_type_collate_dict = this.zy_type_collate_dict;
            collate__all();

        }

        //格式校对
        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.gs_collate_dict;
            this.current_type_collate_dict = this.gs_type_collate_dict;
            collate__all();
        }


        //自定义校对
        private void button8_Click(object sender, RibbonControlEventArgs e)
        {
            this.current_collate_dict = this.zd_collate_dict;
            this.current_type_collate_dict = this.zd_type_collate_dict;
            collate__all();
        }



        public void Log(String msg)
        {
            String time = DateTime.Now.ToString();
            this.LogMessage += time + "   " + msg + "\r\n";
        }
        private void eventHandler_Look(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            ////Microsoft.Office.Interop.Word.Selection currentSelection = m_app.Selection;
            ////string pid = currentSelection.Paragraphs.First.ID;
            ////LookPoint(pid);
            //Document doc = m_app.ActiveDocument;
            //Microsoft.Office.Interop.Word.Selection currentSelection = m_app.Selection;
            //string pid = currentSelection.Paragraphs.First.ID;
            //string nextid = LookPoint(pid);
            //int count = 0;
            //while (nextid.Length > 2 && count <= doc.Paragraphs.Count)
            //{
            //    nextid = LookPoint(nextid);
            //    count += 1;
            //}
        }
        private void button5_Click(object sender, RibbonControlEventArgs e)
        {
            //m_app = Globals.ThisAddIn.Application;
            //m_app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            //Microsoft.Office.Interop.Word.Document doc = m_app.ActiveDocument;

            //Document doc = m_app.ActiveDocument;

            Document doc = m_app.ActiveDocument;
            Log("Comments num:" + doc.Comments.Count.ToString());

            //foreach (Comment one in doc.Comments)
            //{
            //    Log("Comment:");
            //    Log("one.Index:" + one.Index.ToString());
            //    Log("one.Creator:" + one.Creator.ToString());
            //    //Log("one.IsInk:" + one.IsInk.ToString());
            //    Log("one.Range:" + one.Range.Start.ToString() + " " + one.Range.End.ToString());
            //    Log("one.Range.Text:" + one.Range.Text.ToString());
            //    Log("one.Range.ToString():" + one.Range.Text);
            //    Log("one.Reference.Text:" + one.Reference.Text.ToString());
            //    Log("one.Scope:" + one.Scope.Start.ToString() + " " + one.Scope.End.ToString());
            //    Log("one.Scope.Text:" + one.Scope.Text.ToString());
            //    Log("one.Initial:" + one.Initial.ToString());
            //    Log("one.Author:" + one.Author.ToString());

            //    Log("doc.Comments[one.Index].Index:" + doc.Comments[one.Index].Index.ToString());

            //    Log("");
            //}


            int nextid = LookPoint();


        }

        public bool is_my_comment(string str)
        {
            if (str.IndexOf("●错误类型:") == 0)
                return true;
            else
                return false;

            //if (this.all_classify_list.Contains(Author) || this.type_define_dict.ContainsKey(Author))
            //    return true;
            //else
            //    return false;
        }
        /// <summary>
        /// 定位到最近的批注
        /// </summary>
        private int LookPoint()
        {
            int select_id = -1;
            Document doc = m_app.ActiveDocument;
            Microsoft.Office.Interop.Word.Selection currentSelection = m_app.Selection;
            foreach (Comment one in doc.Comments)
            {
                string comment_str = "";
                if (one.Range.Text != null)
                {
                    comment_str = one.Range.Text.ToString();
                }
                if (is_my_comment(comment_str))
                {
                    select_id = one.Index;
                    if (one.Scope.End >= currentSelection.Start)
                    {
                        break;
                    }
                }

            }
            if (select_id != -1)
            {
                //打开查看对话框
                Form_Result form = new Form_Result(this, select_id);
                form.ShowDialog();


            }
            else
            {
                MessageBox.Show("无校对批注！");
            }

            return select_id;
        }
        ///// <summary>
        ///// 计算显示位置版本2，新增
        ///// </summary>
        ///// <param name="erone"></param>
        ///// <returns></returns>
        //private Range ShowLoaction_v2(ErrInfo erone)
        //{
        //    TextParagraph textp = parsePart(erone);
        //    int psize = 0;
        //    int pstart = 0;

        //    pstart = erone.totalStart;
        //    psize = erone.errorWord.Length;
        //    Document doc = this.m_app.ActiveDocument;
        //    Range rg = doc.Range(pstart, pstart  + psize);
        //    rg.Select();
        //    return rg;

        //}
        /// <summary>
        /// 计算显示位置
        /// </summary>
        private Range ShowLoaction(ErrInfo erone)
        {
            //计算显示的位置：
            var List = this.ResultList.OrderBy(t => t.totalStart).ToList();

            string pid = "";
            int psize = 0;
            int pstart = 0;
            foreach (var one in List)
            {
                if (pid != one.pid)
                {
                    //新段落开始
                    pid = one.pid;
                    psize = 0;
                    //计算段落开始位置
                    Document doc = m_app.ActiveDocument;
                    foreach (Paragraph pg in doc.Paragraphs)
                    {
                        if (pg.ID == one.pid)
                        {
                            pstart = pg.Range.Start;
                        }
                    }
                }

                if (one.uuid == erone.uuid)
                {
                    Document doc = m_app.ActiveDocument;
                    Range rg = doc.Range(pstart + int.Parse(erone.startPos) + psize, pstart + int.Parse(erone.endPos) + psize);
                    //Log("psize:" + psize);
                    //Log("one:" + one.errorWord + rg.Start.ToString() + " " + rg.End.ToString() + rg.Text);
                    rg.Select();
                    return rg;
                }

                else
                {
                    if (one.alreadyChange == false)
                    {
                        //psize += one.CommentLength - 1;
                        psize += 1;
                    }
                    else
                    {
                        psize += one.AfterText.Length - one.errorWord.Length;
                    }
                }
            }
            return null;
        }
        private void button6_Click(object sender, RibbonControlEventArgs e)
        {
            AddErrorWord();
        }
        private void eventHandler_AddError(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddErrorWord();
        }
        private void AddErrorWord()
        {
            this.collate_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/text";
            this.login_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";
            this.login_username = ConfigurationManager.AppSettings["username"];
            this.login_code = ConfigurationManager.AppSettings["code"];

            string cstring = Utils.URLCheck.CheckUrl(this.login_url);
            if (cstring.Length == 0)
            {
                bool is_login = backgroundLogIn();
                if (is_login)
                {
                    //添加新错误记录
                    String text = Globals.ThisAddIn.Application.Selection.Text;
                    if (text != null)
                    {
                        Form_AddError form = new Form_AddError(this, text, this.token);
                        form.ShowDialog();
                    }
                }
                else
                {
                    FormLogIn();
                }
            }
            else
            {
                MessageBox.Show(cstring);
            }
        }

        //private void button7_Click(object sender, RibbonControlEventArgs e)
        //{
        //    AddRightWord();
        //}
        //private void eventHandler_AddRight(Office.CommandBarButton Ctrl, ref bool CancelDefault)
        //{
        //    AddRightWord();
        //}
        //private void AddRightWord()
        //{

        //    String text = Globals.ThisAddIn.Application.Selection.Text;
        //    if (text != null && text.Length > 1)
        //    {
        //        Form_AddRight form = new Form_AddRight(this, text);
        //        form.ShowDialog();
        //    }
        //}

        private void button10_Click(object sender, RibbonControlEventArgs e)
        {
            deleteAllComments_v2();
        }
        public void deleteAllComments()
        {
            this.ResultList.Clear();
            Document doc = m_app.ActiveDocument;
            //Log("delete");
            foreach (Comment one in doc.Comments)
            {
                string str = one.Author.ToString();
                str = str.Substring(str.Length - 2, 2);
                if (str == "校对")
                {
                    //Log("delete  ok");
                    one.Delete();
                }

            }
        }
        public void deleteAllComments_v2()
        {
            this.ResultList.Clear();
            Document doc = m_app.ActiveDocument;
            foreach (Comment one in doc.Comments)
            {
                string str = "";
                if (one.Range.Text != null)
                {
                    str = one.Range.Text.ToString();
                }

                this.Log("str:" + str);
                if (str.IndexOf("●错误类型:") == 0)
                {
                    one.Delete();
                }



            }
        }
        private void button9_Click(object sender, RibbonControlEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认要修改全部的字词校对批注？", "修改校对批注", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                //全部修改
                var List = this.ResultList.OrderBy(t => t.totalStart).ToList();
                foreach (var one in List)
                {
                    if (one.modify)
                    {
                        Range ran = ShowLoaction(one, List);
                        if (null != ran)
                        {
                            //ran.End += one.CommentLength - this.CommentShowStatus;
                            //使用定长修改批注
                            ran.End += 1;
                            ran.Select();
                            ////字词如果有逗号特殊处理
                            //if (one.type == "字词" && one.correct.suggestion.IndexOf(",") > -1)
                            //{
                            //    int sta = one.correct.suggestion.IndexOf(",");
                            //    ran.Text = one.correct.suggestion.Substring(0, sta);
                            //    one.AfterText = one.correct.suggestion.Substring(0, sta);
                            //}
                            //else
                            //{
                            //    ran.Text = one.correct.suggestion;
                            //    one.AfterText = one.correct.suggestion;
                            //}
                            one.alreadyChange = true;
                            //删除批注列表中的批注信息
                            Document doc = m_app.ActiveDocument;
                            foreach (Comment com1 in doc.Comments)
                            {
                                string text = com1.Scope.Text;
                                if (text == one.errorWord)
                                {
                                    com1.Delete();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        private Range ShowLoaction(ErrInfo erone, List<ErrInfo> List)
        {
            //计算显示的位置：
            string pid = "";
            int psize = 0;
            int pstart = 0;
            foreach (var one in List)
            {
                if (pid != one.pid)
                {
                    //新段落开始
                    pid = one.pid;
                    psize = 0;
                    //计算段落开始位置
                    Document doc = this.m_app.ActiveDocument;
                    foreach (Paragraph pg in doc.Paragraphs)
                    {
                        if (pg.ID == one.pid)
                        {
                            pstart = pg.Range.Start;
                        }
                    }
                }
                if (one.uuid == erone.uuid)
                {
                    Document doc = this.m_app.ActiveDocument;
                    Range rg = doc.Range(pstart + int.Parse(erone.startPos) + psize, pstart + int.Parse(erone.endPos) + psize);
                    rg.Select();
                    return rg;
                }
                else
                {
                    if (one.alreadyChange == false)
                    {
                        psize += one.CommentLength - 1;
                    }
                    else
                    {
                        psize += one.AfterText.Length - (int.Parse(one.endPos) - int.Parse(one.startPos)) - 1;
                    }
                }
            }
            return null;
        }

        private int get_current_comment()
        {
            int select_id = -1;
            Document doc = m_app.ActiveDocument;
            Microsoft.Office.Interop.Word.Selection currentSelection = m_app.Selection;
            foreach (Comment one in doc.Comments)
            {
                string comment_str = "";
                if (one.Range.Text != null)
                {
                    comment_str = one.Range.Text.ToString();
                }
                if (is_my_comment(comment_str))
                {
                    select_id = one.Index;
                    if (one.Scope.End >= currentSelection.Start)
                    {
                        break;
                    }
                }

            }
            return select_id;
        }

        private void button_prev_Click(object sender, RibbonControlEventArgs e)
        {
            Document doc = m_app.ActiveDocument;
            int comment_id = this.get_current_comment();
            comment_id -= 1;
            if (comment_id >= 1 && comment_id <= doc.Comments.Count)
            {
                Range rg = doc.Comments[comment_id].Scope;
                rg.Select();
            }
            else
            {
                MessageBox.Show("已查找到文章顶部！");
            }
        }

        private void button_next_Click(object sender, RibbonControlEventArgs e)
        {
            Document doc = m_app.ActiveDocument;
            int comment_id = this.get_current_comment();
            comment_id += 1;
            if (comment_id >= 1 && comment_id <= doc.Comments.Count)
            {
                Range rg = doc.Comments[comment_id].Scope;
                rg.Select();
            }
            else
            {
                MessageBox.Show("已查找到文章底部！");
            }

        }
        private void LookPoint_Next(int locationidx)
        {
            var List1 = this.ResultList.OrderBy(t => t.totalStart).ToList();
            int cut = 0;
            foreach (var one in List1)
            {
                if (one.alreadyChange)
                {
                    cut += 1;
                    continue;
                }
                if ((locationidx - one.totalStart) < 0 - cut)
                {
                    this.ShowLoaction(one);
                    break;
                }
            }
        }
        private void LookPoint_Prev(int locationidx)
        {
            var List1 = this.ResultList.OrderByDescending(t => t.totalStart).ToList();
            int cut = 0;
            foreach (var one in List1)
            {
                if (one.alreadyChange)
                {
                    cut += 1;
                    continue;
                }
                //计算累计偏移量
                if ((one.totalStart - locationidx + JsSize(one.pid, one.uuid) + one.errorWord.Length + 1) <= 0 - cut)
                {
                    this.ShowLoaction(one);
                    break;
                }
            }

        }
        private int JsSize(string temp_pid, string temp_uuid)
        {
            int res = 0;
            var List2 = this.ResultList.OrderBy(t => t.totalStart).ToList();
            foreach (var one in List2)
            {
                if (one.pid == temp_pid)
                {
                    if (temp_uuid == one.uuid)
                    {
                        break;
                    }
                    else
                    {
                        res += 1;
                    }
                }
            }
            return res;
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            AddRightWord();
        }

        private void AddRightWord()
        {
            this.collate_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/collate/text";
            this.login_url = ConfigurationManager.AppSettings["ServerUrl"] + "api/login";
            this.login_username = ConfigurationManager.AppSettings["username"];
            this.login_code = ConfigurationManager.AppSettings["code"];

            string cstring = Utils.URLCheck.CheckUrl(this.login_url);
            if (cstring.Length == 0)
            {
                bool is_login = backgroundLogIn();
                if (is_login)
                {
                    //添加新错误记录
                    String text = Globals.ThisAddIn.Application.Selection.Text;
                    if (text != null)
                    {
                        Form_AddRight form = new Form_AddRight(this, text, this.token);
                        form.ShowDialog();
                    }
                }
                else
                {
                    FormLogIn();
                }
            }
            else
            {
                MessageBox.Show(cstring);
            }
        }

        //自动登录认证
        //1: 
        public bool backgroundLogIn()
        {

            ////验证本地token是否可用
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("text", "");
            //dic.Add("mode", "0");
            //var res = Utils.HttpUtils.PostData(this.collate_url, dic, this.token);

            //if (res.Result != null)
            //{
            //    ErrResult errR = JsonConvert.DeserializeObject<ErrResult>(res.Result);
            //    if (errR.code == "200")
            //    {
            //        return true;
            //    }
            //}

            //获取新的token
            Utils.HttpUtils.get_login_token(this.login_url, this.login_username, this.login_code);
            this.token = Utils.HttpUtils.get_login_token(this.login_url, this.login_username, this.login_code);
            if (this.token != "")
            {
                return true;
            }
            return false;

        }

        private void button7_Click(object sender, RibbonControlEventArgs e)
        {
            FormLogIn();
        }

        //手动登录
        public void FormLogIn()
        {
            Form_login form = new Form_login();
            form.ShowDialog();
            this.load_app_setting();
        }

        private void button11_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }

    public partial class TextParagraph
    {
        private int id;
        private string pid;
        private int start;
        private int end;
        private int pg_start;
        private int pg_end;
        private int length;
        private string text;
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Pid
        {
            get
            {
                return pid;
            }

            set
            {
                pid = value;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public int Pg_Start
        {
            get
            {
                return pg_start;
            }

            set
            {
                pg_start = value;
            }
        }

        public int Pg_End
        {
            get
            {
                return pg_end;
            }

            set
            {
                pg_end = value;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }
    }

    public partial class TextPart
    {
        private int id;
        private string text;
        private int length;
        private int startParagraph;
        private int endParagraph;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public int Length
        {
            get
            {
                return length;
            }

            set
            {
                length = value;
            }
        }

        public int StartParagraph
        {
            get
            {
                return startParagraph;
            }

            set
            {
                startParagraph = value;
            }
        }

        public int EndParagraph
        {
            get
            {
                return endParagraph;
            }

            set
            {
                endParagraph = value;
            }
        }
    }
}
