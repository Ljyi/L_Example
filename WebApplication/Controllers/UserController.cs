using AutofacApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApplication.AutofacAttribute;

namespace NetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Autowired]
        private IUserService userService { get; set; }

        public IActionResult Index()
        {
            string name = userService.GetUserName();
            return Content(name);
        }
    }
}
