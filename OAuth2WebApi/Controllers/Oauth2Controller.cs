using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OAuth2WebApi.Controllers
{
    public class Oauth2Controller : Controller
    {
        //[Authorize]
        public ActionResult Authorize()
        {
            if (Request.HttpMethod == "POST")
            {
                var identity = this.User.Identity as ClaimsIdentity;
                identity = new ClaimsIdentity(identity.Claims, "Bearer", identity.NameClaimType, identity.RoleClaimType);
                var authentication = HttpContext.GetOwinContext().Authentication;
                authentication.SignIn(identity);
            }
            return View();
        }
    }
}