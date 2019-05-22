using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TestConsoleApplication.Excel
{
    public static class HtmlToExcel
    {
        public static void Print()
        {
            //接收需要导出的数据
            List<Product> list = new List<Product>() {
                new Product() {
                 Id=1,
                 SalePrice=12,
                 Summary=12,
                 Title="cessss",
                 VipPrice=11
                },
                new Product() {
                 Id=2,
                 SalePrice=2,
                 Summary=2,
                 Title="1231eqwreada",
                 VipPrice=1
                }
            };

            //命名导出表格的StringBuilder变量
            StringBuilder sHtml = new StringBuilder(string.Empty);

            //打印表头
            sHtml.Append("<table border=\"1\" width=\"100%\">");
            sHtml.Append("<tr height=\"40\"><td colspan=\"5\" align=\"center\" style='font-size:24px'><b>XXXXXXX报价表" + "</b></td></tr>");

            //打印列名
            sHtml.Append("<tr height=\"20\" align=\"center\" style='background-color:#CD0000'><td>编号</td><td>商品名称</td><td>市场价</td><td>VIP价格</td><td>说明</td></tr>");

            //循环读取List集合 
            for (int i = 0; i < list.Count; i++)
            {
                sHtml.Append("<tr height=\"20\" align=\"left\"><td style='background-color:#8DEEEE'>" + list[i].Id + "</td><td>" + list[i].Title + "</td><td style='background-color:#8DEEEE'>￥" + list[i].SalePrice + "</td><td style='color:#F00;background-color:#8DEEEE;'>￥" + list[i].VipPrice + "</td><td>" + list[i].Summary + "</td></tr>");
            }

            //打印表尾
            sHtml.Append("<tr height=\"40\"><td align=\"center\" colspan=\"5\" style='background-color:#CD0000;font-size:24px'><b>XXXXXXXX</a> </b></td></tr>");
            sHtml.Append("</table>");

            //调用输出Excel表的方法
            ExportToExcel("application/ms-excel", "XXXXXX报价表.xls", sHtml.ToString());
        }
        //输入HTTP头，然后把指定的流输出到指定的文件名，然后指定文件类型
        public static void ExportToExcel(string FileType, string FileName, string ExcelContent)
        {
            System.Web.HttpContext.Current.Response.ContentType = FileType;
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.Charset = "utf-8";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.HttpContext.Current.Response.Output.Write(ExcelContent.ToString());
            /*乱码BUG修改 20140505*/
            //如果采用以上代码导出时出现内容乱码，可将以下所注释的代码覆盖掉上面【System.Web.HttpContext.Current.Response.Output.Write(ExcelContent.ToString());】即可实现。
            //System.Web.HttpContext.Current.Response.Write("<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=utf-8\"/>" + ExcelContent.ToString());
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }

    }
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? VipPrice { get; set; }
        public int Summary { get; set; }
    }
}
