using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using static YandeDowner.DownLoadThread;

namespace YandeDowner
{
    class DownLoader
    {
        static int max_thread = 10;
        static string post = "https://yande.re/post";

        public void downLoad(string savePath, string key_word, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine("Loading page data: " + i + "/" + end);
                downByPage(savePath, key_word, i);
            }
        }

        public void downByPage(string savePath, string word, int pageIndex)
        {
            string url = post + "?commit=Search&page=" + pageIndex + "&tags=" + word;
            string pageHtml = UrlHelper.getHtmlSource(url);
            string validHtml = pageHtml.Replace("\n", "").Replace("\"", "");

            string regstr = "jpeg_url:((https|http|ftp|rtsp|mms)?:\\/\\/)[^\\s]+.jpg";
            Regex reg = new Regex(regstr, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = reg.Matches(validHtml);

            List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();
            ThreadPool.SetMaxThreads(max_thread, max_thread);

            string imageUrl;
            string fileName;

            foreach (Match m in mc)
            {
                imageUrl = m.Groups[0].ToString();
                imageUrl = imageUrl.Substring(imageUrl.IndexOf(@"url:") + 4);
                fileName = Uri.UnescapeDataString(imageUrl.Substring(imageUrl.LastIndexOf(@"/") + 1));
                fileName = FileHelper.getVoidFileName(fileName);

                ManualResetEvent mre = new ManualResetEvent(false);
                tryDownFile(imageUrl, savePath, fileName, mre);
                manualEvents.Add(mre);
            }
            WaitHandle.WaitAll(manualEvents.ToArray());
        }

        public void tryDownFile(string imageUrl, string savePath, string fileName, ManualResetEvent mre)
        {
            DownLoadThread DLThread = new DownLoadThread();
            ThreadMethodHelper argu = new ThreadMethodHelper() { url = imageUrl, savePath = savePath, fileName = fileName, mre = mre };
            ThreadPool.QueueUserWorkItem(new WaitCallback(DLThread.DownLoadImage), argu);
        }
    }
}
