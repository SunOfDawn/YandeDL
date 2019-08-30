using System.IO;
using System.Text;

namespace YandeDowner
{
    public static class FileHelper
    {
        public static void processPath(string path)
        {
            if (!Directory.Exists(@path))
            {
                Directory.CreateDirectory(@path);
            }
        }
        public static string getVoidFileName(string fileName)
        {
            StringBuilder rBuilder = new StringBuilder(fileName);
            foreach (char rInvalidChar in Path.GetInvalidFileNameChars())
            {
                rBuilder.Replace(rInvalidChar.ToString(), string.Empty);
            }
            return rBuilder.ToString();
        }
    }
}
