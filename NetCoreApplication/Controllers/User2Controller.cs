using AutofacApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApplication.Controllers
{
    /// <summary>
    /// 多接口注入
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class User2Controller : ControllerBase
    {
        private IUserService _userService { get; set; }
        public User2Controller(IEnumerable<IUserService> userServices)
        {
            _userService = userServices.Where(zw => zw.GetType() == typeof(User2Service)).FirstOrDefault();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        //public IActionResult Index()
        //{
        //    string name = _userService.GetUserName();
        //    return Content(name);
        //}
    }
}
