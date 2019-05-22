using DAL.DBBase;
using DAL.DBContext;
using Microsoft.AspNet.Identity;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WEB.Identity
{
    public class SendSupplyMailDAL
    {
        public IBaseRepository<SendSupplyMail> _p = null;
        public SendSupplyMailDAL()
        {
            _p = new BaseRepository<SendSupplyMail>();
        }
        //业务处理
    }
    public class IdentityUserStore : IUserPasswordStore<IdentityLoginUser>, IUserStore<IdentityLoginUser>, IDisposable
    {
        public Task CreateAsync(IdentityLoginUser user)
        {
            throw new NotImplementedException("不支持新增用户");
        }

        public Task DeleteAsync(IdentityLoginUser user)
        {
            throw new NotImplementedException("不支持删除用户");
        }

        public Task<IdentityLoginUser> FindByIdAsync(int userId)
        {
            //BizEntities b = new BizEntities();
            return null;//b.Sys_User.FindAsync(new object[] { userId });
        }

        public Task<IdentityLoginUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            SendSupplyMailDAL sendSupplyMailDAL = new SendSupplyMailDAL();
            var user = sendSupplyMailDAL._p.Find(p => p.Guid == "");
            IdentityLoginUser identityLoginUser = new IdentityLoginUser
            {
                UserId = user.SupplyID.Value,
                UserName = user.PurchaseNo,
                Password = user.PurchaseNo
            };

            return Task.FromResult<IdentityLoginUser>(identityLoginUser);
        }

        public Task UpdateAsync(IdentityLoginUser user)
        {
            throw new NotImplementedException("不支持删除用户");
        }

        public Task<IdentityLoginUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return null;

            return this.FindByIdAsync(int.Parse(userId));
        }

        public void Dispose() { }

        #region IUserPasswordStore接口
        /// <summary>
        /// 这个方法用不到！！！
        /// 虽然对于普通系统的明文方式这些用不到。
        /// 但是！！！MVC5之后的Identity2.0后数据库保存的密码是加密盐加密过的，这里也需要一并处理，然而1.0并不需要处理，需注意！！！
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(IdentityLoginUser user)
        {
            return Task.FromResult<string>(user.Password);
        }

        public Task<bool> HasPasswordAsync(IdentityLoginUser user)
        {
            return Task.FromResult<bool>(!string.IsNullOrWhiteSpace(user.Password));
        }

        public Task SetPasswordHashAsync(IdentityLoginUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult<int>(0);
        }

        #endregion
    }

}