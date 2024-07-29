using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRSWordAddIn.Utils
{
    public class SEGG
    {
        public string collateWord { get; set; }
        public string weight { get; set; }
    }

    public class ErrInfo
    {
        public string sentence { get; set; }
        public string senIdx { get; set; }
        public string senStartPos { get; set; }
        public string senEndPos { get; set; }
        public string startPos { get; set; }
        public string endPos { get; set; }
        public string errorType { get; set; }
        public string errorTypeInfo { get; set; }
        public string suggestType { get; set; }
        public string errorWord { get; set; }
        public List<SEGG> suggestions { get; set; }
        public string collateWord { get; set; }
        public string weight { get; set; }
        public string engine { get; set; }

        // 片段标识
        public string partId { get; set; }
        //段落标识
        public string pid { get; set; }
        //唯一标识
        public string uuid { get; set; }
        //全文处理
        public int totalStart { get; set; }
        public int totalEnd { get; set; }
        //批注的长度
        public int CommentLength { get; set; }
        //批注的ID
        public string CommentId { get; set; }
        public bool alreadyChange { get; set; }
        //修改后文字
        public string AfterText { get; set; }
        //可修改标示
        public bool modify { get; set; }
        
        
    }
    public class ErrResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public List<ErrInfo> data { get; set; }
    }

    public class LoginResult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }


    public class Config
    {
            //"searchValue": null,
            //"createBy": "admin",
            //"createTime": "2022-05-05 17:49:48",
            //"updateBy": "",
            //"updateTime": null,
            //"remark": null,
            //"params": {},
            //"configId": 103001,
            //"deptId": 1030,
            //"userId": 103001,
            //"projectId": null,
            //"configName": "深度校对模型",
            //"configType": "",
            //"configKey": "model.dl",
            //"configValue": "bert_collate",
            //"status": "0"
        public string searchValue { get; set; }
        public string createBy { get; set; }
        public string createTime { get; set; }
        public string updateBy { get; set; }
        public string updateTime { get; set; }
        public string remark { get; set; }
        public string config_params { get; set; }
        public string configId { get; set; }
        public string deptId { get; set; }
        public string userId { get; set; }
        public string projectId { get; set; }
        public string configName { get; set; }
        public string configType { get; set; }
        public string configKey { get; set; }
        public string configValue { get; set; }
        public string status { get; set; }
        


    }

    public class Configresult
    {
        public string code { get; set; }
        public string msg { get; set; }
        public List<Config> data { get; set; }
    }

    class ErrInfoT
    {
        public ErrInfoT() { }
        private string text;
        private string errType;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }


        public string ErrType
        {
            get { return errType; }
            set { errType = value; }
        }
        private int errLevel;

        public int ErrLevel
        {
            get { return errLevel; }
            set { errLevel = value; }
        }
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }
        private int end;

        public int End
        {
            get { return end; }
            set { end = value; }
        }

        private string colText;

        public string ColText
        {
            get { return colText; }
            set { colText = value; }
        }
        private int colStart;

        public int ColStart
        {
            get { return colStart; }
            set { colStart = value; }
        }
        private int colEnd;

        public int ColEnd
        {
            get { return colEnd; }
            set { colEnd = value; }
        }
        private int colPos;

        public int ColPos
        {
            get { return colPos; }
            set { colPos = value; }
        }

        private int corCount;

        public int CorCount
        {
            get { return corCount; }
            set { corCount = value; }
        }
        private string corSuggestion;

        public string CorSuggestion
        {
            get { return corSuggestion; }
            set { corSuggestion = value; }
        }



    }
}
