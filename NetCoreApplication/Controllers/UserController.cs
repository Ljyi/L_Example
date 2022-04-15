using AutofacApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApplication.AutofacAttribute;

namespace NetCoreApplication.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Autowired]
        private IUserService userService { get; set; }
        /// <summary>
        /// 活动
        /// </summary>
        /// <returns></returns>
        //public IActionResult Index()
        //{
        //    string name = userService.GetUserName();
        //    return Content(name);
        //}
    }
}
