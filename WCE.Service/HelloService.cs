using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCE.Service
{
    public class HelloService : IHelloService
    {
        public string SayHello(string name)
        {
            return "你好，我是：" + name;
        }
    }
    public class UpLoadService : IUpLoadService
    {
        public void UploadFile(FileUploadMessage request)
        {
            string uploadFolder = @"C:\kkk\";
            string savaPath = request.SavePath;
            string dateString = DateTime.Now.ToShortDateString() + @"\";
            string fileName = request.FileName;
            Stream sourceStream = request.FileData;
            FileStream targetStream = null;
            if (!sourceStream.CanRead)
            {
                throw new Exception("数据流不可读!");
            }
            if (savaPath == null) savaPath = @"Photo\";
            if (!savaPath.EndsWith("\\")) savaPath += "\\";

            uploadFolder = uploadFolder + savaPath + dateString;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string filePath = Path.Combine(uploadFolder, fileName);
            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int bufferLen = 4096;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
        }

    }
}
