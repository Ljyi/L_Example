using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WEB.Utils;

namespace WEB.BaseApplication
{
    public class AuthenticationModule : IHttpModule
    {
        private const int AUTHENTICATION_TIMEOUT = 20;

        public AuthenticationModule() { }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(Context_AuthenticateRequest);
        }

        public void Dispose()
        {
            // Nothing here	
        }

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool AuthenticateUser(string loginName, string password, ref string errorMsg, bool rememberMe = false, string currentIp = null)
        {
            try
            {
                //SupplyAccountDAL supplyAccountDAL = new SupplyAccountDAL();
                if (string.IsNullOrWhiteSpace(currentIp))
                    currentIp = GetWebClientIp();
                string curSite = HttpContext.Current.Request.Url.Host;
                SupplyAccount user = new SupplyAccount(); //supplyAccountDAL.Login(loginName, EncryptHelper.MD5Encrypt32(password));
                if (user != null)
                {
                    UserIdentity userIdentity = new UserIdentity(user, true);
                    HttpContext.Current.User = new Principal(userIdentity);
                    string accountJson = JsonConvert.SerializeObject(userIdentity);
                    //   FormsAuthentication.SetAuthCookie(user.SupplyAccountID.ToString(), rememberMe);

                    //这是一个很尴尬的写法
                    //序列化account对象
                    //   string accountJson = JsonConvert.SerializeObject(userIdentity);
                    //   创建用户票据
                    var ticket = new FormsAuthenticationTicket(1, userIdentity.UserName, DateTime.Now, DateTime.Now.AddDays(1), false, accountJson);
                    //加密
                    string encryptAccount = FormsAuthentication.Encrypt(ticket);
                    //创建cookie
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptAccount)
                    {
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL,
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath
                    };
                    //写入Cookie
                    HttpContext.Current.Response.Cookies.Remove(cookie.Name);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    return true;
                }
                else
                {
                    errorMsg = "账号或密码有误,请重新输入";
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("登陆失败 {0}:{1}", loginName, e.Message), e);
            }
        }

        /// <summary>
        ///用户注销.
        /// </summary>
        public static void Logout()
        {
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
        }
        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            try
            {
                if (app.Context.User != null && app.Context.User.Identity.IsAuthenticated)
                {
                    if (string.IsNullOrEmpty(app.Context.User.Identity.Name))
                        return;
                    FormsIdentity id = (FormsIdentity)app.Context.User.Identity;
                    FormsAuthenticationTicket authTicket = id.Ticket;
                    if (string.IsNullOrEmpty(authTicket.UserData) || authTicket.UserData.Trim() == string.Empty)
                    {
                        //  SupplyAccountDAL supplyAccountDAL = new SupplyAccountDAL();
                        //  SupplyAccount user = supplyAccountDAL.GetModel(new SupplyAccount() { SupplyAccountID = Convert.ToInt32(app.Context.User.Identity.Name) });
                        SupplyAccount user =  new SupplyAccount();
                        UserIdentity userIdentity = new UserIdentity(user, true);
                        app.Context.User = new Principal(userIdentity);
                    }
                }
            }
            catch
            {
                app.Context.User = new Principal();
                FormsAuthentication.SignOut();
            }
        }

        /// <summary>
        /// 获取web客户端ip
        /// ljy 2017年5月11日11:23:47
        /// </summary>
        /// <returns></returns>
        public static string GetWebClientIp()
        {
            string userIP = "未获取用户IP";
            try
            {
                if (HttpContext.Current == null
                 || HttpContext.Current.Request == null
                 || HttpContext.Current.Request.ServerVariables == null)
                {
                    return "";
                }
                string CustomerIP = "";
                //CDN加速后取到的IP
                CustomerIP = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (!string.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }
                CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!String.IsNullOrEmpty(CustomerIP))
                {
                    return CustomerIP;
                }
                if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                {
                    CustomerIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (CustomerIP == null)
                    {
                        CustomerIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    CustomerIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];//客户端本地IP
                }
                if (string.Compare(CustomerIP, "unknown", true) == 0 || String.IsNullOrEmpty(CustomerIP))
                {
                    return HttpContext.Current.Request.UserHostAddress;//返回客户端本地IP
                }
                return CustomerIP;
            }
            catch { }

            return userIP;

        }
    }
}