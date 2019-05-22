using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Tools
{
    /// <summary>
    ///     数据辅助操作类
    /// </summary>
    internal static class DBErrorType
    {/// <summary>
     ///     由错误码返回指定的自定义SqlException异常信息
     /// </summary>
     /// <param name="number"> </param>
     /// <returns> </returns>
        public static string GetSqlExceptionMessage(int errorCode)
        {
            string errorMsg = string.Empty;
            switch (errorCode)
            {
                case 2:
                    errorMsg = "连接数据库超时，请检查网络连接或者数据库服务器是否正常。";
                    break;
                case 17:
                    errorMsg = "SqlServer服务不存在或拒绝访问。";
                    break;
                case 17142:
                    errorMsg = "SqlServer服务已暂停，不能提供数据服务。";
                    break;
                case 2812:
                    errorMsg = "指定存储过程不存在。";
                    break;
                case 208:
                    errorMsg = "指定名称的表不存在。";
                    break;
                case 4060: //数据库无效。
                    errorMsg = "所连接的数据库无效。";
                    break;
                case 18456: //登录失败
                    errorMsg = "使用设定的用户名与密码登录数据库失败。";
                    break;
                case 547:
                    errorMsg = "外键约束，无法保存数据的变更。";
                    break;
                case 2627:
                    errorMsg = "主键重复，无法插入数据。";
                    break;
                case 2601:
                    errorMsg = "未知错误。";
                    break;
            }
            return errorMsg;
        }
    }
}
