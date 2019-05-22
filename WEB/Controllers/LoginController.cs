using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB.BaseApplication;
using WEB.Models;

namespace WEB.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            //已经登录的，直接到默认首页
            if (HttpContext.Request.IsAuthenticated)
            {
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string userPassword, string isRemember)
        {
            if (userName == "admin" && userPassword == "111")
            {
                Person p = new Person() { Name = userName, Roles = "admin", Age = 23, Email = "xx@qq.com", Ip = "" };
                bool remenber = isRemember == null ? false : true;
                //把用户对象保存在票据里
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddTicks(FormsAuthentication.Timeout.Ticks), remenber, "");
                //加密票据
                string hashTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
                if (remenber)
                {
                    userCookie.Expires = DateTime.Now.AddTicks(FormsAuthentication.Timeout.Ticks);
                }
                Response.Cookies.Add(userCookie);

                string returnUrl = HttpUtility.UrlDecode(Request["ReturnUrl"]);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewData["Tip"] = "用户名或密码有误！";
                return View();
            }
        }
        public ActionResult Logout()
        {
            //取消Session会话
            Session.Abandon();
            //删除Forms验证票证
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual void OnAuthorizeation(AuthorizationContext filternContent)
        { 

        }
    }
}