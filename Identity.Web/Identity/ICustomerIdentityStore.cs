using Identity.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Web.Identity
{
    /// <summary>
    /// 身份验证Store接口
    /// </summary>
    public interface ICustomerIdentityStore :
         IUserLoginStore<UserInfo, int>,
         IUserClaimStore<UserInfo, int>,
         IUserRoleStore<UserInfo, int>,
         IUserPasswordStore<UserInfo, int>,
         IUserSecurityStampStore<UserInfo, int>,
         IQueryableUserStore<UserInfo, int>,
         IUserEmailStore<UserInfo, int>,
         IUserPhoneNumberStore<UserInfo, int>,
         IUserTwoFactorStore<UserInfo, int>,
         IUserLockoutStore<UserInfo, int>,
         IUserStore<UserInfo, int>
    {
    }
}
