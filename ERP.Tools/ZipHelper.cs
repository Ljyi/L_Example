using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Tools
{
    /// <summary>
    /// 压缩文件类
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="strFile">待压缩文件目录</param>
        /// <param name="strZip">压缩后的目标文件</param>
        public static void ZipFile(string strFile, string strZip)
        {
            var len = strFile.Length;
            ZipOutputStream outstream = new ZipOutputStream(File.Create(strZip));
            outstream.SetLevel(6);
            Zip(strFile, outstream, strFile);
            outstream.Finish();
            outstream.Close();
        }
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="outstream"></param>
        /// <param name="staticFile"></param>
        private static void Zip(string strFile, ZipOutputStream outstream, string staticFile)
        {
            try
            {
                Crc32 crc = new Crc32();
                //获取指定目录下所有文件和子目录文件名称
                string[] filenames = Directory.GetFileSystemEntries(strFile);
                //遍历文件
                foreach (string file in filenames)
                {
                    if (Directory.Exists(file))
                    {
                        Zip(file, outstream, staticFile);
                    }
                    //否则，直接压缩文件
                    else
                    {
                        //打开文件
                        FileStream fs = File.OpenRead(file);
                        //定义缓存区对象
                        byte[] buffer = new byte[fs.Length];
                        //通过字符流，读取文件
                        fs.Read(buffer, 0, buffer.Length);
                        //得到目录下的文件（比如:D:\Debug1\test）,test
                        string tempfile = file.Substring(staticFile.LastIndexOf("\\") + 1);
                        ZipEntry entry = new ZipEntry(tempfile);
                        entry.DateTime = DateTime.Now;
                        entry.Size = fs.Length;
                        fs.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        outstream.PutNextEntry(entry);
                        //写文件
                        outstream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
            }
        }
    }
}
