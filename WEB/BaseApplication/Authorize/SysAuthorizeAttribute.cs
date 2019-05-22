using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.BaseApplication
{
    /// <summary>
    /// 请求验证
    /// </summary>
    public class SysAuthorizeAttribute : AuthorizeAttribute
    {
        public const string URL_Login = "/login"; //小写

        // 忽略URL
        private List<string> IgnoreURLs
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(URL_Login.ToLower());
                return value;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
            string controller = (string)filterContext.RouteData.Values["Controller"];
            string action = (string)filterContext.RouteData.Values["Action"];
            string url = urlHelper.Action(action, controller, new { id = "" }); //不验证参数
            if (IgnoreURLs.Contains(url.ToLower())) return;// 忽略URL
            // 必须登录
            if (filterContext.HttpContext.User.Identity == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(string.Format("~/Login/Index?returnUrl={0}", filterContext.HttpContext.Request.Url.PathAndQuery));
            }
            else
            {
                //(这是一个很悲伤的故事)
                Principal principal = new Principal(HttpContext.Current.User.Identity.Name);
                HttpContext.Current.User = principal;
                //权限判断
                if (!ServiceHelper.AllowedAccessUrl(url))
                {
                    //if (filterContext.HttpContext.Request.Url.Host != "localhost")
                    //{
                    //    //后期做权限验证
                    //    string message = string.Format("您没有此操作权限！");
                    //    filterContext.HttpContext.Response.Redirect(string.Format("~/Shared/Error?message={0}", message));
                    //}
                }
            }
        }
    }
}