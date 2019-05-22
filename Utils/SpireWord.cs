using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Model;

namespace Utils
{
    public static class SpireWord
    {
        static Document document = new Document();
        /// <summary>
        /// 获取模板
        /// </summary>
        public static void GetDocument()
        {
            try
            {
                string wordTemplatePath = AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExport\\ContractTemplate.docx";
                document.LoadFromFile(wordTemplatePath);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                throw;
            }
        }
        public static void CreateNewWord()
        {
            document.SaveToFile("Replace.docx", FileFormat.Docx);
        }
        /// <summary>
        /// 替换模板
        /// </summary>
        /// <param name="purchaseContract"></param>
        public static void ReplaseTemplateWord(PurchaseContract purchaseContract)
        {
            try
            {
                foreach (System.Reflection.PropertyInfo p in purchaseContract.GetType().GetProperties())
                {
                    Console.WriteLine("Name:{0} Value:{1}", p.Name, p.GetValue(purchaseContract));
                    document.Replace(p.Name, p.GetValue(purchaseContract).ToString(), false, true);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
        public static void AddTable(List<PurchaseContractDetaile> purchaseOrderDetaileList)
        {
            try
            {
                Table table = document.Sections[0].Tables[0] as Table;
                int rowNum = purchaseOrderDetaileList.Count;
                DataTable dataTable = new DataTable();
                int i = 1;
                foreach (var item in purchaseOrderDetaileList)
                {
                    table.AddRow(10);
                    if (i < table.Rows.Count)
                    {
                        table[i, 0].AddParagraph().AppendText(item.采购单号);
                        table[i, 1].AddParagraph().AppendText(item.品名);
                        table[i, 2].AddParagraph().AppendText(item.SKU);
                        table[i, 3].AddParagraph().AppendText(item.产品描述);
                        table[i, 4].AddParagraph().AppendText(item.单位);
                        table[i, 5].AddParagraph().AppendText(item.数量.ToString());
                        table[i, 6].AddParagraph().AppendText(item.单价.ToString());
                        table[i, 7].AddParagraph().AppendText(item.采购成本.ToString());
                        table[i, 8].AddParagraph().AppendText(item.税金.ToString());
                        table[i, 9].AddParagraph().AppendText(item.价税合计.ToString());
                    }
                    i++;
                }
                table.AddRow(10);
                table[i, 0].AddParagraph().AppendText("合计（大写）:" + Common.ConvertToChineseRMB(2386.20M));
                table.ApplyHorizontalMerge(i, 0, 8);
                table[i, 9].AddParagraph().AppendText("2386.20");
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                throw;
            }
        }

    }
}
