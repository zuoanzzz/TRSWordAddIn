using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace TRSWordAddIn.Utils
{
    public class HttpUtils
    {
        public static async Task<string> PostData(string url, Dictionary<string, object> data, string token)
        {
            //特殊字符转义
            
            string sendata = JsonConvert.SerializeObject(data); ;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Proxy = null;
            //request.Timeout = 10000;
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            if (token != "")
            {
                request.Headers.Add("Authorization:Bearer " + token);
            }
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sendata);
            request.ContentLength = buf.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();
            //HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            HttpWebResponse response;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string result = reader.ReadToEnd();
                return result;
            }else
            {
                return null;
            }
            
        }

        public static string get_login_token(string url, string username, string code)
        {
            string token = "";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("username", username);
            dic.Add("code", code);
            var res = Utils.HttpUtils.PostData(url, dic, "");

            if (res.Result != null)
            {
                LoginResult LogR = JsonConvert.DeserializeObject<LoginResult>(res.Result);
                //MessageBox.Show(res.Result.ToString());

                if (LogR.code == "200")
                {
                    token = LogR.token;
                }

            }


            return token;
        }

        public static string GetData(string url, string token)
        {

            //访问http方法
            string strBuff = "";
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法建立，并进行强制的类型转换   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            if (token != "")
            {
                httpReq.Headers.Add("Authorization:Bearer " + token);
            }

            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换   
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容   
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理   
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以   
            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）   
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            strBuff = respStreamReader.ReadToEnd();
            return strBuff;
        }



        public static string AddErrorData(string url, string error, string right)
        {
            string sendata = "errorWord=" + error + "&rightWord=" + right;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sendata);
            request.ContentLength = buf.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();
            //HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string result = reader.ReadToEnd();
            return result;
        }

        /// <summary>
        /// 撤销错误词
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorWord"></param>
        /// <param name="rightWord"></param>
        /// <param name="sentence"></param>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string DeleteWordData(string url, string errorWord, string rightWord)
        {
            string sendata = "errorWord=" + errorWord + "&rightWord=" + rightWord;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sendata);
            request.ContentLength = buf.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string result = reader.ReadToEnd();
            return result;
        }

        //public static string Login(string url, string username, string code)
        //{
        //    string sendata = "errorWord=" + errorWord + "&rightWord=" + rightWord;
        //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        //    request.KeepAlive = false;
        //    request.Proxy = null;
        //    request.Method = "POST";
        //    request.ContentType = "application/json; charset=UTF-8";
        //    byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sendata);
        //    request.ContentLength = buf.Length;
        //    Stream newStream = request.GetRequestStream();
        //    newStream.Write(buf, 0, buf.Length);
        //    newStream.Close();
        //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        //    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
        //    string result = reader.ReadToEnd();
        //    return result;
        //}

        /// <summary>
        /// 撤销语义错误
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorWord"></param>
        /// <param name="rightWord"></param>
        /// <param name="sentence"></param>
        /// <param name="engine"></param>
        /// <returns></returns>
        public static string DeleteWithdrawData(string url, string coreNoun, string assistNoun, string errorType, string suggestion)
        {
            string sendata = "coreNoun=" + coreNoun + "&assistNoun=" + assistNoun + "&errorType=" + errorType + "&suggestion=" + suggestion;
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.KeepAlive = false;
            request.Proxy = null;
            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sendata);
            request.ContentLength = buf.Length;
            Stream newStream = request.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string result = reader.ReadToEnd();
            return result;
        }
    }

    
}