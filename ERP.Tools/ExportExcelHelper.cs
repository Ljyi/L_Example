using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.Tools
{
    /// <summary>
    /// 说明:导出Excel
    /// </summary>
    public abstract class ExcelBaseResult<T> : ActionResult
    {
        #region 属性
        /// <summary>
        /// 数据实体
        /// </summary>
        public IList<T> Entity { get; set; }
        /// <summary>
        /// 下载文件名称(不包含扩展名)
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// ContentType
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string ExtName { get; set; }
        /// <summary>
        /// 获取下载文件全名
        /// </summary>
        public string FullName { get { return FileName + ExtName; } }

        public string FilePath { get; set; }
        #endregion

        #region 构造函数
        public ExcelBaseResult(IList<T> entity, string fileName, string title)
        {
            this.Entity = entity;
            this.FileName = fileName;
            this.Title = title;
        }
        #endregion

        #region 抽象方法
        public abstract MemoryStream GetExcelStream(string filePath);
        #endregion
    }

    public class FilePathResult : FileResult
    {
        string contentType;
        string fileDownloadName;
        public FilePathResult(string fileDownloadName, string contentType) : base(contentType)
        {
        }
    }

    //FileResult进行下载
    public class ExportExcelHelper<T>
    {
        /// <summary>
        /// List&lt;T&gt;转化为Excel文件,并返回FileStreamResult   03版本
        /// </summary>
        /// <param name="list">需要转化的List&lt;T&gt;</param> 
        /// <param name="headerList">Excel标题行的List列表</param>
        /// <param name="fileName">Excel的文件名</param>
        /// <param name="sortList">指定导出List&lt;T&gt中哪些属性,并按顺序排序</param>
        /// <param name="isCompress">是否压缩</param>
        /// <returns></returns>
        public static FileResult ExportListToExcel_MvcResult(IList<T> list, string fileName, IList<string> headerList = null, IList<string> sortList = null, bool isCompress = false, int pagingSize = 65535)
        {
            //无数据情况
            if (list == null || list.Count == 0)
            {
                //服务器路径
                var serverPath = HttpContext.Current.Server.MapPath("~/Export/Temp");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                fileName = string.Format("无数据_{0}.txt", DateTime.Now.ToString("MMddhhmmss"));
                string noDataFileName = Path.Combine(serverPath, fileName);
                using (FileStream fs2 = File.Create(noDataFileName))
                {
                    fs2.Close();
                }

                return new FilePathResult(noDataFileName, "text/plain") { FileDownloadName = fileName };
            }
            var cnt = (list.Count / pagingSize + (list.Count % pagingSize != 0 ? 1 : 0));
            //如果是压缩，或者条数大于pagingSize，则也为压缩
            isCompress = isCompress || cnt > 1;
            var generateFileName = ExportListToExcel(list, fileName, headerList, sortList, isCompress, pagingSize);
            if (isCompress)
            {
                return new FilePathResult(generateFileName, "application/x-zip-compressed") { FileDownloadName = fileName + ".zip" };
            }
            return new FilePathResult(generateFileName, "application/ms-excel") { FileDownloadName = fileName + ".xls" };
        }

        private static string ExportListToExcel(IList<T> list, string fileName, IList<string> headerList = null, IList<string> sortList = null, bool isCompress = false, int pagingSize = 65535)
        {
            try
            {
                //服务器路径
                var serverPath = HttpContext.Current.Server.MapPath("~/Export/Temp");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                var listFileNames = new List<string>();
                int pageIndex = 0;

                #region 生成文件名
                var currentUserID = 1;// ServiceHelper.GetCurrentUser().UserID;
                var random = new Random().Next(10000, 50000);
                var date = DateTime.Now.ToString("MMddhhmmss");
                #endregion

                //选取65536是因为Excel2003的Sheet的最大行，如果是列数有很多很多而导致内存溢出的，可以降低这个值

                pageIndex++;
                #region 生成文件名
                if (fileName == null)
                {
                    fileName = currentUserID + "_" + date + "_" + random + "_" + pageIndex;
                }
                if (!fileName.EndsWith(".xls") && !fileName.EndsWith(".xlsx"))
                {
                    fileName += ".xls";
                }
                var curExcelName = Path.Combine(serverPath, fileName);
                #endregion
                #region 创建临时文件
                using (var ms = new MemoryStream())
                {
                    var workbook = new HSSFWorkbook();   //创建Excel工作部   
                    workbook.Write(ms);//将Excel写入流
                    ms.Flush();
                    ms.Position = 0;
                    FileStream dumpFile = new FileStream(curExcelName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    ms.WriteTo(dumpFile);//将流写入文件
                    ms.Close();
                    dumpFile.Close();
                }
                #endregion
                #region 打开文件，写内容
                using (FileStream fs = File.Open(curExcelName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    IWorkbook workbook = new HSSFWorkbook(fs);
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.BorderTop = BorderStyle.Thin;
                    cellStyle.BorderBottom = BorderStyle.Thin;
                    cellStyle.BorderLeft = BorderStyle.Thin;
                    cellStyle.BorderRight = BorderStyle.Thin;
                    cellStyle.TopBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.BottomBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.RightBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.LeftBorderColor = HSSFColor.Grey50Percent.Index;
                    ISheet sheet = workbook.CreateSheet(string.Format("sheet{0}", pageIndex));
                    IRow row = sheet.CreateRow(0);
                    int count = 0;
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                    //如果没有自定义的行首,那么采用反射集合的属性名做行首
                    if (headerList == null)
                    {
                        for (int i = 0; i < properties.Count; i++) //生成sheet第一行列名 
                        {
                            ICell cell = row.CreateCell(count++);
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(String.IsNullOrEmpty(properties[i].DisplayName) ? properties[i].Name : properties[i].DisplayName);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < headerList.Count; i++) //生成sheet第一行列名 
                        {
                            ICell cell = row.CreateCell(count++);
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(headerList[i]);
                        }
                    }
                    //将数据导入到excel表中
                    if (list == null) return "";
                    var sheetList = list.ToList();
                    for (int i = 0; i < sheetList.Count; i++)
                    {
                        IRow rows = sheet.CreateRow(i + 1);
                        count = 0;
                        object value = null;
                        //如果自定义导出属性及排序字段为空,那么走反射序号的方式
                        if (sortList == null)
                        {
                            for (int j = 0; j < properties.Count; j++)
                            {
                                ICell cell = rows.CreateCell(count++);
                                cell.CellStyle = cellStyle;
                                value = properties[j].GetValue(sheetList[i]);
                                if (value == null)
                                    cell.SetCellValue("");
                                else
                                {
                                    setCellValue(cell, properties[j].PropertyType.Name.ToLower(), value, () =>
                                    {
                                        return Nullable.GetUnderlyingType(properties[j].PropertyType).Name.ToLower();
                                    });
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < sortList.Count; j++)
                            {
                                ICell cell = rows.CreateCell(count++);
                                cell.CellStyle = cellStyle;
                                var currentProperty = properties[sortList[j]];
                                value = currentProperty.GetValue(sheetList[i]);
                                if (value == null)
                                    cell.SetCellValue("");

                                //根据反射来获取类型
                                setCellValue(cell, currentProperty.PropertyType.Name.ToLower(), value, () =>
                                {
                                    return Nullable.GetUnderlyingType(currentProperty.PropertyType).Name.ToLower();
                                });
                            }
                        }
                        sheet.ForceFormulaRecalculation = true;         //保存excel文档
                    }
                    using (FileStream fs2 = File.Create(curExcelName))
                    {
                        workbook.Write(fs2);
                        fs2.Close();
                    }

                    listFileNames.Add(curExcelName);
                }
                #endregion
                #region 处理文件名
                if (isCompress || listFileNames.Count > 1)
                {
                    var fzName = listFileNames.First().Replace(".xls", ".zip").Replace(".xlsx", ".zip");
                    // ZipHelper.Zip(listFileNames, fzName);
                    return fzName;
                }
                return listFileNames.First();
                #endregion
            }
            catch (Exception ex)
            {
                throw ex.InnerException != null ? ex.InnerException : ex;
            }
        }
        private static void setCellValue(ICell cell, string typeName, object value, Func<string> getNullnderlyingType)
        {
            try
            {
                if (typeName == "nullable`1")
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        cell.SetCellValue(string.Empty);
                    }
                    else
                    {
                        setCellValue(cell, getNullnderlyingType(), value, null);
                    }
                }
                else
                {
                    switch (typeName)
                    {
                        case "int":
                        case "int32":
                            cell.SetCellValue(Convert.ToInt32(value));
                            break;
                        case "long":
                        case "int64":
                            cell.SetCellValue(Convert.ToInt64(value));
                            break;
                        case "decimal":
                        case "float":
                        case "single":
                            cell.SetCellValue(Convert.ToDouble(value));
                            break;
                        case "datetime":
                            cell.SetCellValue(Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));
                            break;
                        case "bool":
                        case "boolean":
                            cell.SetCellValue(Convert.ToBoolean(value));
                            break;
                        default:
                            cell.SetCellValue(Convert.ToString(value));
                            break;
                    }
                }
            }
            catch (Exception)
            {
                cell.SetCellValue(string.Empty);
            }
        }
    }

    //通过流
    public class ExportExcelHelpers<T> : ExcelBaseResult<T> where T : new()
    {
        public ExportExcelHelpers(IList<T> entity, string fileName, string title) : base(entity, fileName, title)
        {
            ContentType = "application/vnd.ms-excel";
            ExtName = ".xls";
        }
        /// <summary>
        /// List&lt;T&gt;转化为Excel文件,并返回FileStreamResult   03版本
        /// </summary>
        /// <param name="list">需要转化的List&lt;T&gt;</param> 
        /// <param name="headerList">Excel标题行的List列表</param>
        /// <param name="fileName">Excel的文件名</param>
        /// <param name="sortList">指定导出List&lt;T&gt中哪些属性,并按顺序排序</param>
        /// <param name="isCompress">是否压缩</param>
        /// <returns></returns>
        public static void ExportListToExcelResult(IList<T> list, string fileName, IList<string> headerList = null, IList<string> sortList = null, bool isCompress = false, int pagingSize = 65535)
        {
            //无数据情况
            if (list == null || list.Count == 0)
            {
                //服务器路径
                var serverPath = HttpContext.Current.Server.MapPath("~/Export/Temp");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                fileName = string.Format("无数据_{0}.txt", DateTime.Now.ToString("MMddhhmmss"));
                string noDataFileName = Path.Combine(serverPath, fileName);
                using (FileStream fs2 = File.Create(noDataFileName))
                {
                    fs2.Close();
                }
            }
            var cnt = (list.Count / pagingSize + (list.Count % pagingSize != 0 ? 1 : 0));
            //如果是压缩，或者条数大于pagingSize，则也为压缩
            isCompress = isCompress || cnt > 1;
            var generateFileName = ExportListToExcel(list, fileName, headerList, sortList, isCompress, pagingSize, null);
        }

        private static string ExportListToExcel(IList<T> list, string fileName, IList<string> headerList = null, IList<string> sortList = null, bool isCompress = false, int pagingSize = 65535, string fileNameNew = null)
        {
            try
            {
                //服务器路径
                var serverPath = HttpContext.Current.Server.MapPath("~/Export/Temp");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                var listFileNames = new List<string>();
                int pageIndex = 0;

                #region 生成文件名
                var currentUserID = 1;// ServiceHelper.GetCurrentUser().UserID;
                var random = new Random().Next(10000, 50000);
                var date = DateTime.Now.ToString("MMddhhmmss");
                #endregion

                //选取65536是因为Excel2003的Sheet的最大行，如果是列数有很多很多而导致内存溢出的，可以降低这个值

                pageIndex++;
                #region 生成文件名
                if (fileNameNew == null)
                {
                    fileName = currentUserID + "_" + date + "_" + random + "_" + pageIndex;
                }
                else
                {
                    fileName = fileNameNew + "_" + date + "_" + random + "_" + pageIndex;
                }
                if (!fileName.EndsWith(".xls") && !fileName.EndsWith(".xlsx"))
                {
                    fileName += ".xls";
                }
                var curExcelName = Path.Combine(serverPath, fileName);
                #endregion
                #region 创建临时文件
                using (var ms = new MemoryStream())
                {
                    var workbook = new HSSFWorkbook();   //创建Excel工作部   
                    workbook.Write(ms);//将Excel写入流
                    ms.Flush();
                    ms.Position = 0;
                    FileStream dumpFile = new FileStream(curExcelName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    ms.WriteTo(dumpFile);//将流写入文件
                    ms.Close();
                    dumpFile.Close();
                }
                #endregion
                #region 打开文件，写内容
                using (FileStream fs = File.Open(curExcelName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    IWorkbook workbook = new HSSFWorkbook(fs);
                    ICellStyle cellStyle = workbook.CreateCellStyle();
                    cellStyle.BorderTop = BorderStyle.Thin;
                    cellStyle.BorderBottom = BorderStyle.Thin;
                    cellStyle.BorderLeft = BorderStyle.Thin;
                    cellStyle.BorderRight = BorderStyle.Thin;
                    cellStyle.TopBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.BottomBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.RightBorderColor = HSSFColor.Grey50Percent.Index;
                    cellStyle.LeftBorderColor = HSSFColor.Grey50Percent.Index;
                    ISheet sheet = workbook.CreateSheet(string.Format("sheet{0}", pageIndex));
                    IRow row = sheet.CreateRow(0);
                    int count = 0;
                    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                    //如果没有自定义的行首,那么采用反射集合的属性名做行首
                    if (headerList == null)
                    {
                        for (int i = 0; i < properties.Count; i++) //生成sheet第一行列名 
                        {
                            ICell cell = row.CreateCell(count++);
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(String.IsNullOrEmpty(properties[i].DisplayName) ? properties[i].Name : properties[i].DisplayName);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < headerList.Count; i++) //生成sheet第一行列名 
                        {
                            ICell cell = row.CreateCell(count++);
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(headerList[i]);
                        }
                    }
                    //将数据导入到excel表中
                    if (list == null) return "";
                    var sheetList = list.ToList();
                    for (int i = 0; i < sheetList.Count; i++)
                    {
                        IRow rows = sheet.CreateRow(i + 1);
                        count = 0;
                        object value = null;
                        //如果自定义导出属性及排序字段为空,那么走反射序号的方式
                        if (sortList == null)
                        {
                            for (int j = 0; j < properties.Count; j++)
                            {
                                ICell cell = rows.CreateCell(count++);
                                cell.CellStyle = cellStyle;
                                value = properties[j].GetValue(sheetList[i]);
                                if (value == null)
                                    cell.SetCellValue("");
                                else
                                {
                                    SetCellValue(cell, properties[j].PropertyType.Name.ToLower(), value, () =>
                                    {
                                        return Nullable.GetUnderlyingType(properties[j].PropertyType).Name.ToLower();
                                    });
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < sortList.Count; j++)
                            {
                                ICell cell = rows.CreateCell(count++);
                                cell.CellStyle = cellStyle;
                                var currentProperty = properties[sortList[j]];
                                value = currentProperty.GetValue(sheetList[i]);
                                if (value == null)
                                    cell.SetCellValue("");

                                //根据反射来获取类型
                                SetCellValue(cell, currentProperty.PropertyType.Name.ToLower(), value, () =>
                                {
                                    return Nullable.GetUnderlyingType(currentProperty.PropertyType).Name.ToLower();
                                });
                            }
                        }
                        sheet.ForceFormulaRecalculation = true;         //保存excel文档
                    }
                    using (FileStream fs2 = File.Create(curExcelName))
                    {
                        workbook.Write(fs2);
                        fs2.Close();
                    }

                    listFileNames.Add(curExcelName);
                }
                #endregion
                #region 处理文件名
                if (isCompress || listFileNames.Count > 1)
                {
                    var fzName = listFileNames.First().Replace(".xls", ".zip").Replace(".xlsx", ".zip");
                    // ZipHelper.Zip(listFileNames, fzName);
                    return fzName;
                }
                return listFileNames.First();
                #endregion
            }
            catch (Exception ex)
            {
                throw ex.InnerException != null ? ex.InnerException : ex;
            }
        }
        private static void SetCellValue(ICell cell, string typeName, object value, Func<string> getNullnderlyingType)
        {
            try
            {
                if (typeName == "nullable`1")
                {
                    if (string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        cell.SetCellValue(string.Empty);
                    }
                    else
                    {
                        SetCellValue(cell, getNullnderlyingType(), value, null);
                    }
                }
                else
                {
                    switch (typeName)
                    {
                        case "int":
                        case "int32":
                            cell.SetCellValue(Convert.ToInt32(value));
                            break;
                        case "long":
                        case "int64":
                            cell.SetCellValue(Convert.ToInt64(value));
                            break;
                        case "decimal":
                        case "float":
                        case "single":
                            cell.SetCellValue(Convert.ToDouble(value));
                            break;
                        case "datetime":
                            cell.SetCellValue(Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));
                            break;
                        case "bool":
                        case "boolean":
                            cell.SetCellValue(Convert.ToBoolean(value));
                            break;
                        default:
                            cell.SetCellValue(Convert.ToString(value));
                            break;
                    }
                }
            }
            catch (Exception)
            {
                cell.SetCellValue(string.Empty);
            }
        }
        public override MemoryStream GetExcelStream(string filePath)
        {
            MemoryStream ms = new MemoryStream();
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Read);
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, (int)file.Length);
            ms.Write(bytes, 0, (int)file.Length);
            file.Close();
            ms.Close();
            return ms;

        }
    }

    /// <summary>
    /// 导出
    /// </summary>
    public class ExportExcelHelper
    {
        public delegate void ExportKeyHandler(string keyValue);
        /// <summary>
        /// 完成导出返回键值
        /// </summary>
        public event ExportKeyHandler ExportKeyHandle;
        public class SubTable
        {
            public DataTable dt;
            public List<string> cols;
            public int iRow;
        }
        /// <summary>
        /// 多表模板导出
        /// </summary>
        /// <param name="keyField">主键列名</param>
        /// <param name="excelTemplatePath">导出模板路径</param>
        /// <param name="saveDir">保存模板路径</param>
        /// <param name="fileNameField">导出文件名</param>
        /// <param name="dtMain">主表</param>
        /// <param name="tables">副表,按主键区分</param>
        public void ExportToXls(string keyField, string excelTemplatePath, string saveDir, string fileNameField, DataTable dtMain, DataTable[] tables)
        {
            var subTables = tables.Select(o => new SubTable()
            {
                dt = o,
                cols = GetDtCols(o),
                iRow = -1,
            }).ToList();
            List<string> supCols = GetDtCols(dtMain);
            foreach (var drMain in dtMain.AsEnumerable())
            {
                IWorkbook workbook = new XSSFWorkbook();
                using (FileStream fs = File.Open(excelTemplatePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    workbook = new XSSFWorkbook(fs);
                    fs.Close();
                }
                var sheet = workbook.GetSheetAt(0);
                var keyID = drMain[keyField].ToString();
                System.Diagnostics.Trace.WriteLine(string.Format("开始写入主键:{0}", keyID));
                //遍历全表替换主参
                //int iRow = -1;
                for (int i = 0; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    if (row == null)
                        continue;
                    System.Diagnostics.Trace.WriteLine(string.Format("开始更新行{0}", i));
                    for (int j = 0; j <= row.LastCellNum; j++)
                    {
                        var cell = row.GetCell(j);
                        if (cell == null
                            || cell.CellType != CellType.String
                            || string.IsNullOrWhiteSpace(cell.StringCellValue)
                            )
                            continue;
                        System.Diagnostics.Trace.WriteLine(string.Format("开始更新列{0}", j));
                        bool isNext = false;//剪枝标识
                        foreach (var col in supCols)
                        {
                            var pat = string.Format("{{{0}}}", col);
                            if (cell.StringCellValue.Contains(pat))
                            {
                                if (drMain[col] is string)
                                {
                                    cell.SetCellValue(cell.StringCellValue.Replace(pat, drMain[col].ToString()));
                                }
                                else
                                {
                                    SetCellDataRowValue(cell, drMain[col]);
                                    isNext = true;
                                    break;
                                }
                            }
                        }
                        if (isNext)
                            break;

                        System.Diagnostics.Trace.WriteLine("副表插入位定位");
                        foreach (var subTable in subTables)
                        {
                            if (subTable.iRow < 0)
                            {
                                foreach (var col in subTable.cols)
                                {
                                    var pat = string.Format("{{{0}}}", col);
                                    if (cell.StringCellValue.Contains(pat))
                                    {
                                        subTable.iRow = i;//标记行
                                        break;
                                    }
                                }
                                if (subTable.iRow >= 0)
                                    break;
                            }
                        }
                    }
                }

                int rowAdd = 0;//累计新增行
                System.Diagnostics.Trace.WriteLine("副表插入");
                foreach (var subTable in subTables.Where(o => o.iRow >= 0).OrderBy(o => o.iRow))
                {
                    var subDrs = subTable.dt.AsEnumerable().Where(dr => keyID.Equals(dr[keyField].ToString()));
                    int rowCnt = subDrs.Count();
                    if (rowCnt == 0)
                    {
                        var rowRemove = subTable.iRow + rowAdd + 1;
                        var row = sheet.GetRow(rowRemove);
                        sheet.RemoveRow(row);
                    }
                    int subRowAdd = 0;
                    foreach (var subDr in subDrs)
                    {
                        var rowMod = subTable.iRow + rowAdd;
                        if (subRowAdd != rowCnt - 1)//非最后一行,需先复制
                        {
                            try
                            {
                                var lastRowNum = sheet.LastRowNum;
                                sheet.ShiftRows(rowMod + 1, lastRowNum, 1, true, true);
                                sheet.CopyRow(rowMod, rowMod + 1);
                                rowAdd++; subRowAdd++;
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(string.Format("新增明细行失败{0}", ex.Message));
                            }
                        }
                        var row = sheet.GetRow(rowMod);
                        for (int j = 0; j <= row.LastCellNum; j++)
                        {
                            var cell = row.GetCell(j);
                            if (cell == null)
                                continue;

                            foreach (var col in subTable.cols)
                            {
                                var pat = string.Format("{{{0}}}", col);
                                if (cell.CellType == CellType.String
                                    && !string.IsNullOrWhiteSpace(cell.StringCellValue)
                                    && cell.StringCellValue == pat
                                    )
                                {
                                    SetCellDataRowValue(cell, subDr[col]);
                                    break;
                                }
                            }
                        }
                    }
                }

                XSSFFormulaEvaluator.EvaluateAllFormulaCells(workbook);
                System.Diagnostics.Trace.WriteLine("导出到excel");
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }
                var saveExcelPath = Path.Combine(saveDir
                    , string.Format("{0}.xlsx", drMain[fileNameField])
                    );
                FileStream saveFs = File.OpenWrite(saveExcelPath);
                workbook.Write(saveFs);
                saveFs.Close();
                System.Diagnostics.Trace.WriteLine("导出完成");

                if (ExportKeyHandle != null)
                    ExportKeyHandle(keyID);
            }
        }
        private static List<string> GetDtCols(DataTable dtSup)
        {
            var supCols = new List<string>();
            foreach (DataColumn col in dtSup.Columns)
            {
                supCols.Add(col.ColumnName);
            }

            return supCols;
        }
        private static void SetCellDataRowValue(ICell cell, object datarowVal)
        {
            var strVal = datarowVal.ToString();
            if (string.IsNullOrWhiteSpace(strVal))
            {
                cell.SetCellValue("");
            }
            else if (datarowVal is int
                     || datarowVal is long
                     || datarowVal is double
                     || datarowVal is float
                     || datarowVal is decimal)
            {
                cell.SetCellValue(double.Parse(strVal));
            }
            else if (datarowVal is DateTime)
            {
                cell.SetCellValue(DateTime.Parse(strVal));
            }
            else
            {
                cell.SetCellValue(strVal);
            }
        }
    }
}
