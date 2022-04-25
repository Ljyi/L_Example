using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        public AccountController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetToken()
        {
            return _jwtHelper.CreateToken();
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //[Authorize]
        //[HttpGet]
        //public ActionResult<string> GetTest()
        //{
        //    return "Test Authorize";
        //}
    }
}
