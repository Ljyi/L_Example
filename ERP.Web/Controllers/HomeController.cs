using ERP.Model;
using ERP.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                UserService userService = new UserService();
               // Task<int> adds = userService.Add();
                List<User> users = userService.GetUsers();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
            return View();
        }
    }
}