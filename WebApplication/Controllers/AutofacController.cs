using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreApplication.Repository;

namespace NetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutofacController : ControllerBase
    {
        /// <summary>
        ///不使用IOC，通过构造函数进行依赖
        /// GET api/<HomeeController>/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SqlRepository sql = new SqlRepository();
            DBBase db = new DBBase(sql);
            db.Search("SELECT * FORM USER");
            return "value";
        }
    }
}
