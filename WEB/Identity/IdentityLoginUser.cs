using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web;

namespace WEB.Identity
{
    /// <summary>
    /// IdentityLoginUser,重写Identity.IUser,这个跟数据库中的用户表是两码事，专门做登录、权限判断用的
    /// 但是，我们可以把它跟数据库的用户表结合起来用，上面部分为数据的用户表，下面为继承成IUserIdentity（可做公司产品）
    /// </summary>
    [Serializable]
    public class IdentityLoginUser : ISerializable, IUserIdentity, IUser
    {
        #region 数据库字段
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        #endregion


        #region 公司自己的产品IUserIdentity接口
        //bool? IUserIdentity.IsRootAdmin
        //{
        //    get { return null; }
        //}
        #endregion


        #region 微软IUser接口
        string Microsoft.AspNet.Identity.IUser<string>.Id
        {
            get { return null; }
        }
        #endregion


        #region 必须实现，否则无法使用
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("UserId", this.UserId);
            info.AddValue("Password", string.Empty);
        }

        /// <summary>
        /// 反序列化的构造函数
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public IdentityLoginUser(SerializationInfo info, StreamingContext context)
        {
            this.UserId = info.GetInt32("UserId");
            this.Password = info.GetString("Password");
        }
        public IdentityLoginUser()
        {
        }
        #endregion

        public string ToJson() { return JsonConvert.SerializeObject(this); }
    }
}