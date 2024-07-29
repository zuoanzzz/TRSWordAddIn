using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

using System.Windows.Forms;




namespace TRSWordAddIn.Utils
{
    public class URLCheck
    {
        public static string CheckUrl(string u)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 500;
            string res = "";
            /*
            if (UrlIsExist(u))
            {

            }
            else
            {
                res = "URL地址不存在，且无法访问";
                return res;
            }
            */
            if (UrlisUse(u))
            {

            }
            else
            {
                res = "URL地址不正确，返回错误";
                return res;
            }

            return res;
        }
        private static bool UrlisUse(string url)
        {
            try
            {
                //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                //myRequest.Method = "HEAD";　              //设置提交方式可以为＂ｇｅｔ＂，＂ｈｅａｄ＂等
                //myRequest.Timeout = 10000;　             //设置网页响应时间长度
                //myRequest.AllowAutoRedirect = false;//是否允许自动重定向
                //HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                //return (myResponse.StatusCode == HttpStatusCode.OK);//返回响应的状态

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("username", "demo");
                dic.Add("code", "");

                string sendata = JsonConvert.SerializeObject(dic);

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
                HttpWebResponse response;
                try
                {
                    response = request.GetResponse() as HttpWebResponse;
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
                string result = reader.ReadToEnd();

                newStream.Close();
                request.Abort();
                if (response.StatusCode == HttpStatusCode.OK && result.IndexOf("code") > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            
                
        }
        private static bool UrlIsExist(string url)
        {
            System.Uri u = null;
            try
            {
                u = new Uri(url);
            }
            catch { return false; }
            bool isExist = false;
            System.Net.HttpWebRequest r = System.Net.HttpWebRequest.Create(u) as System.Net.HttpWebRequest;
            r.Method = "HEAD";
            try
            {
                System.Net.HttpWebResponse s = r.GetResponse() as System.Net.HttpWebResponse;
                if (s.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    isExist = true;
                }
            }
            catch (System.Net.WebException x)
            {
                try
                {
                    isExist = ((x.Response as System.Net.HttpWebResponse).StatusCode != System.Net.HttpStatusCode.NotFound);
                }
                catch { isExist = (x.Status == System.Net.WebExceptionStatus.Success); }
            }
            return isExist;
        }
    }
}
