using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nlog.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger logger;
        public UsersController(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<UsersController>();

            #region 测试日志
            logger.LogTrace("开发阶段调试，可能包含敏感程序数据", 1);
            logger.LogDebug("开发阶段短期内比较有用，对调试有益。");
            logger.LogInformation("你访问了首页。跟踪程序的一般流程。");
            logger.LogWarning("警告信息！因程序出现故障或其他不会导致程序停止的流程异常或意外事件。");
            logger.LogError("错误信息。因某些故障停止工作");
            logger.LogCritical("程序或系统崩溃、遇到灾难性故障！！！");
            #endregion
        }
        // GET api/values
        [Route("api/Users/Get")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            logger.LogTrace("开发阶段调试，可能包含敏感程序数据", 1);
            logger.LogDebug("开发阶段短期内比较有用，对调试有益。");
            logger.LogInformation("你访问了首页。跟踪程序的一般流程。");
            logger.LogWarning("警告信息！因程序出现故障或其他不会导致程序停止的流程异常或意外事件。");
            logger.LogError("错误信息。因某些故障停止工作");
            logger.LogCritical("程序或系统崩溃、遇到灾难性故障！！！");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
