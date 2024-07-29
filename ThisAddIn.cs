using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using TRSWordAddIn.Utils;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TRSWordAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            try
            {
                Globals.ThisAddIn.Application.ActiveWindow.View.MarkupMode = Microsoft.Office.Interop.Word.WdRevisionsMode.wdInLineRevisions;
                //Log.Info("start");
                //注册快捷键功能
                hotKeyClass.Start();
            }
            catch { }
        }
        const string menu_name = "智能纠错工具";
        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            try
            {
                hotKeyClass.Stop();

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
                        catch
                        {

                        }
                    }
                }
            }
            catch { }
            try
            {
                GC.Collect();
                Marshal.FinalReleaseComObject(Globals.ThisAddIn.Application);
                GC.Collect();
            }
            catch { }
        }
        #region VSTO 生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            try
            {
                this.Startup += new System.EventHandler(ThisAddIn_Startup);
                this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            }
            catch { }
        }
        
        #endregion
    }
}
