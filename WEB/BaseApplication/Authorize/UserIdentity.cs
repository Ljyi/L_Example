using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WEB.BaseApplication
{
    /// <summary>
    ///供应商用户登陆后保存的一些常用信息
    /// </summary>
    public partial class UserIdentity : IIdentity
    {
        public UserIdentity()
        {
            Roles = new List<int>();
            Authorizes = new List<string>();
        }
        /// <summary>
        /// 使用的身份验证的类型。
        /// </summary>
        public virtual string AuthenticationType
        {
            get { return "SupplyCoordination.Authentication"; }
        }
        /// <summary>
        /// 标识供应商
        /// </summary>
        public virtual string Name
        {
            get
            {
                if (IsAuthenticated)
                    return UserID.ToString();
                else
                    return string.Empty;
            }
        }
        /// <summary>
        /// 是否验证了用户
        /// </summary>
        public virtual bool IsAuthenticated { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public virtual int? SupplierID { get; set; }
        /// <summary>
        /// 用户名称（同用户显示名称）
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public virtual int UserType { get; set; }
        /// <summary>
        /// 登陆名称
        /// </summary>
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 是否为Admin用户
        /// </summary>
        public virtual bool IsAdmin { get; set; }
        /// <summary>
        /// 角色ID列表
        /// </summary>
        public virtual List<int> Roles { get; set; }
        /// <summary>
        /// 权限编号列表
        /// </summary>
        public virtual List<string> Authorizes { get; set; }
        public UserIdentity(SupplyAccount user, bool? isAuthenticated) : this()
        {
            if (user != null)
            {
                UserID = user.SupplyAccountID;
                UserName = user.SupplierAccount;
                LoginName = user.SupplierAccount;
                IsAdmin = true;
                UserType = user.UserType.Value;
                SupplierID = user.SupplierID;
            }
            if (isAuthenticated.HasValue)
            {
                IsAuthenticated = isAuthenticated.Value;
            }
        }
    }
}