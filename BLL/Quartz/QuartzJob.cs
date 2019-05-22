using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Quartz
{
    public class QuartzJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Factory.StartNew(() => Console.WriteLine("executed..." + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));
            await Task.Factory.StartNew(() => Write());
        }
        public void Write(string path = "E:\\test.txt")
        {
            StreamWriter sw = new StreamWriter(path, true);
            try
            {
                //  FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
                //开始写入
                sw.WriteLine(" Hello World!!!!" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                //  fs.Close();
            }
            catch (Exception)
            {

                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
            }
        }
    }
}
