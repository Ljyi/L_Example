using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NCore.Web.DbContextCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NCore.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//添加特性，代表是一个Web API控制器类
    public class BlogController : ControllerBase
    {
        private readonly ModelContext _context;
        /// <summary>
        /// 实例化一个EF上下文，进行数据库操作。开始初始入库一条数据
        /// </summary>
        /// <param name="context"></param>
        public BlogController(ModelContext context)
        {
            _context = context;
            if (_context.Blogs.Count() == 0)
            {
                _context.Blogs.Add(new Blog { Url = "Item1" });
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// 获取所有事项
        /// GET: api/Todo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }
        /// <summary>
        /// 根据id，获取一条事项
        /// GET: api/Todo/5。  id 是参数，代表路由合并
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetTodoItem(long id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return blog;
        }

        /// <summary>
        /// 返回固定的字符串格式
        /// </summary>
        /// <returns></returns>
        [HttpGet("Message")]
        public ContentResult Message()
        {
            return Content("hello");
        }
    }
}