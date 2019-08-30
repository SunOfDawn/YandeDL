using System.IO;
using System.Net;
using System.Text;

namespace YandeDowner
{
    public static class UrlHelper
    {
        public static string getHtmlSource(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            string html = reader.ReadToEnd();
            stream.Close();
            return html;
        }
    }
}
