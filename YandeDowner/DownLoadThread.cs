using System;
using System.Net;
using System.Threading;

namespace YandeDowner
{
    class DownLoadThread
    {
        public void DownLoadImage(object argu)
        {
            ThreadMethodHelper helper = (ThreadMethodHelper)argu;
            try
            {
                Console.WriteLine("DownLoad begin——" + helper.fileName);
                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(helper.url, helper.savePath + @"\" + helper.fileName);
                Console.WriteLine("DownLoad Success——" + helper.fileName);
            }
            catch (Exception e)
            {
                e.ToString();
                //请求过多一旦出现短时间内无法继续下载，直接退出
                Console.WriteLine("Error,Too Many Request——" + helper.fileName);
                return;
            }
            finally
            {
                helper.mre.Set();
            }
            return;
        }

        public class ThreadMethodHelper
        {
            public string url;
            public string savePath;
            public string fileName;
            public ManualResetEvent mre;
        }
    }
}
