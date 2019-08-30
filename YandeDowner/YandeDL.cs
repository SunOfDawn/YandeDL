using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YandeDowner
{
    class YandeDL
    {
        static void Main(string[] args)
        {
            DownLoader downer = new DownLoader();
            Console.Write("请输入查询tag：");
            var keyword = Console.ReadLine();
            Console.Write("请输入开始页：");
            var start = Convert.ToInt16(Console.ReadLine());
            Console.Write("请输入结束页：");
            var end = Convert.ToInt16(Console.ReadLine());

            var save_path = @AppDomain.CurrentDomain.BaseDirectory + keyword;
            FileHelper.processPath(save_path);

            downer.downLoad(save_path, keyword, start, end);
            Console.WriteLine("按任意键结束");
            Console.ReadLine();
        }
    }
}
