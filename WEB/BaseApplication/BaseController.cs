using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.BaseApplication
{
    [SysAuthorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            // 记录错误日志
        }
    }
}