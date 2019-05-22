using Identity.Web.Models;
using Identity.Web.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
/// <summary>
/// 身份验证仓储
/// </summary>
public class AuthRepository : IAuthRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    private readonly IUnitRepository _repository;

    /// <summary>
    /// 工作单元
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 用户管理
    /// </summary>
    private readonly UserManager<UserInfo> _userManager;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="userStore"></param>
    public AuthRepository(IUnitRepository repository, IUnitOfWork unitOfWork, ITaurusIdentityStore userStore)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _userManager = new UserManager<UserInfo>(userStore);
    }


    public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
    {
        var result = await _userManager.AddLoginAsync(userId, login);

        return result;
    }

    public async Task<bool> AddRefreshToken(RefreshTokenInfo token)
    {
        var existingToken = _repository.FirstOrDefault<RefreshTokenInfo>(r => r.Subject == token.Subject
        && r.AppClientId == token.AppClientId);

        if (existingToken != null)
        {
            var result = await RemoveRefreshToken(existingToken);
        }

        _repository.Insert(token);

        return _unitOfWork.SaveChanges() > 0;
    }

    public async Task<IdentityResult> CreateAsync(UserInfo user)
    {
        var result = await _userManager.CreateAsync(user);

        return result;
    }

    public async Task<UserInfo> FindAsync(UserLoginInfo loginInfo)
    {
        var user = await _userManager.FindAsync(loginInfo);

        return user;
    }

    public AppClientInfo FindClient(string clientId)
    {
        var client = _repository.FirstOrDefault<AppClientInfo>(s => s.Id == clientId);

        return client;
    }

    public Task<RefreshTokenInfo> FindRefreshToken(string refreshTokenId)
    {
        var refreshToken = _repository.FirstOrDefault<RefreshTokenInfo>(s => s.TokenId == refreshTokenId);

        return Task.FromResult(refreshToken);
    }

    public async Task<UserInfo> FindUser(string userName, string password)
    {
        var user = await _userManager.FindAsync(userName, password);

        return user;
    }

    public List<RefreshTokenInfo> GetAllRefreshTokens()
    {
        return _repository.All<RefreshTokenInfo>().ToList();
    }

    public async Task<IdentityResult> RegisterUser(UserModel userModel)
    {
        var user = new UserInfo
        {
            UserName = userModel.UserName,
            Id = 1,
            FullName = "test",
            CreateTime = DateTime.Now,
            CreateBy = Guid.Empty.ToString(),
            UpdateBy = Guid.Empty.ToString(),
            UpdateTime = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);

        return result;
    }

    public Task<bool> RemoveRefreshToken(RefreshTokenInfo refreshToken)
    {
        _repository.DeleteItem(refreshToken);

        var result = _unitOfWork.SaveChanges() > 0;
        return Task.FromResult(result);
    }

    public Task<bool> RemoveRefreshToken(string refreshTokenId)
    {
        var refreshToken = _repository.FirstOrDefault<RefreshTokenInfo>(s => s.Id == refreshTokenId);
        var result = false;
        if (refreshToken != null)
        {
            _repository.DeleteItem(refreshToken);
            result = _unitOfWork.SaveChanges() > 0;
        }

        return Task.FromResult(result);
    }
}