using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WEB.BaseApplication
{
    public class ServiceHelper
    {
        public static UserIdentity GetCurrentUser()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return null;
            IPrincipal user = context.User;
            if (user == null)
                return null;
            return (user.Identity as UserIdentity);
        }
        // 是否管理员
        public static bool IsAdmin()
        {
            return GetCurrentUser().IsAdmin;
        }
        //权限判断
        public static bool AllowedAuthorizes(string authorize)
        {
            UserIdentity user = GetCurrentUser();
            if (user.IsAdmin) return true;
            return user.Authorizes.Contains(authorize);
        }
        //权限判断
        public static bool AllowedAccessUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || url.Trim() == string.Empty) return true;
            url = url.Trim();
            UserIdentity user = GetCurrentUser();
            if (user.IsAdmin) return true;
            //查找用户权限
            return true;
        }

    }
}