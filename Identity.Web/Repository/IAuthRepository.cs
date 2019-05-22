using Identity.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Web.Repository
{
    /// <summary>
    /// 身份验证仓储
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterUser(UserModel userModel);

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserInfo> FindUser(string userName, string password);

        /// <summary>
        /// 查找AppClient信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        AppClientInfo FindClient(string clientId);

        /// <summary>
        /// 添加Token信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> AddRefreshToken(RefreshTokenInfo token);

        /// <summary>
        /// 移除token信息
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        Task<bool> RemoveRefreshToken(string refreshTokenId);

        /// <summary>
        /// 移除token信息
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> RemoveRefreshToken(RefreshTokenInfo refreshToken);

        /// <summary>
        /// 查找token信息
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        Task<RefreshTokenInfo> FindRefreshToken(string refreshTokenId);

        /// <summary>
        /// 查找所有刷新token信息
        /// </summary>
        /// <returns></returns>
        List<RefreshTokenInfo> GetAllRefreshTokens();

        /// <summary>
        /// 通过登录信息查找用户信息
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        Task<UserInfo> FindAsync(UserLoginInfo loginInfo);

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(UserInfo user);

        /// <summary>
        /// 添加用户登录信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<IdentityResult> AddLoginAsync(int userId, UserLoginInfo login);
    }
}
