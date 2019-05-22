using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class NpoiWord
    {
        //读取word模板
        public static XWPFDocument ReadWordText(string fileName)
        {
            XWPFDocument document = null;
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    document = new XWPFDocument(file);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return document;
        }
        public static void ReplaceWord(XWPFDocument documentWord, string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                //遍历段落
                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey(para);
                }
                using (MemoryStream ms = new MemoryStream())
                {

                    doc.Write(ms);
                    using (FileStream fsWrite = new FileStream(@"D:\1.txt", FileMode.Append))
                    {
                        fsWrite.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    };
                }
            }


        }
        private static void ReplaceKey(XWPFParagraph para)
        {

            string text = para.ParagraphText;
            var runs = para.Runs;
            string styleid = para.Style;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();
                runs[i].SetText(text + 2, 0);
            }
        }
    }
}