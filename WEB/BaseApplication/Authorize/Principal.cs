using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace WEB.BaseApplication
{/// <summary>
 /// 用户权限管理主体
 /// </summary>
 /// <remarks>
 /// 系统底层类，请勿修改
 /// </remarks>
    public class Principal : IPrincipal
    {
        private UserIdentity _userIdentity;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userIdentity">基于IIdentity的实体信息</param>
        public Principal(UserIdentity userIdentity)
        {
            _userIdentity = userIdentity;
        }
        /// <summary>
        /// 无参数构造方法
        /// </summary>
        public Principal()
        {
            _userIdentity = new UserIdentity();
        }
        /// <summary>
        /// 无参数构造方法(这是一个很悲伤的故事)
        /// </summary>
        public Principal(string UserName)
        {
            HttpRequest request = HttpContext.Current.Request;
            HttpCookie cookie = request.Cookies[FormsAuthentication.FormsCookieName];
            //解密coolie值
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            _userIdentity = JsonConvert.DeserializeObject<UserIdentity>(ticket.UserData);
        }
        #region IPrincipal Members

        /// <summary>
        /// 身份认证定义
        /// </summary>
        public IIdentity Identity
        {
            get { return _userIdentity; }
        }

        /// <summary>
        /// 判断角色ID是否是用户所属角色
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public bool IsInRole(string RoleID)
        {
            foreach (var item in _userIdentity.Roles)
            {
                if (item.ToString() == RoleID)
                    return true;
            }
            return false;
        }

        #endregion
    }
}