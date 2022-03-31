using Microsoft.AspNetCore.Mvc;
using NetCoreApplication;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetCoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IOperationTransient _transient;
        private readonly IOperationSingleton _singleton;
        private readonly IOperationScoped _scoped;
        private IServiceProvider _provider;
        public HomeController(IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation, IServiceProvider provider)
        {
            _transient = transientOperation;
            _singleton = singletonOperation;
            _scoped = scopedOperation;
            _provider = provider;
        }
        // 依赖注入
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine(
           $"scope: { _scoped.OperationId }," +
           $"transient: {_transient.OperationId}, " +
           $"singleton: {_singleton.OperationId}");
            return new string[] {
                $"scope1: { _scoped.OperationId }",
                $"transient1: {_transient.OperationId}",
                $"singleton1: {_singleton.OperationId}",
                "--------------------------------------------------------",
                $"scope2: { _scoped.OperationId }",
                $"transient2: {_transient.OperationId}",
                $"singleton2: {_singleton.OperationId}",
            };
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("provider")]
        [HttpGet]
        public IEnumerable<string> Get(int id)
        {
            var Scoped1 = _provider.GetService<Operation>();
            var transient = _provider.GetService(typeof(IOperationTransient));
            var singleton = _provider.GetService(typeof(IOperationSingleton));
            var scoped = _provider.GetService(typeof(IOperationScoped));
            return new string[] {
                $"scope1: { _scoped.OperationId }",
                $"transient1: {_transient.OperationId}",
                $"singleton1: {_singleton.OperationId}",
                "--------------------------------------------------------",
                $"scope2: {((Operation)scoped).OperationId }",
                $"transient2: {((Operation)transient).OperationId}",
                $"singleton2: {((Operation)singleton).OperationId}",
            };
        }
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("provideobject")]
        [HttpGet]
        public IEnumerable<string> provideobject()
        {
            var transient = _provider.GetService(typeof(IOperationTransient));
            var singleton = _provider.GetService(typeof(IOperationSingleton));
            var scoped = _provider.GetService(typeof(IOperationScoped));
            return new string[] {
                $"scope1: { _scoped.OperationId }",
                $"transient1: {_transient.OperationId}",
                $"singleton1: {_singleton.OperationId}",
                "--------------------------------------------------------",
                $"scope2: {((Operation)scoped).OperationId }",
                $"transient2: {((Operation)transient).OperationId}",
                $"singleton2: {((Operation)singleton).OperationId}",
                "--------------------------------------------------------",
                "scope1与scope2是否相等:"+( _scoped.Equals(scoped)?"是":"否"),
                "transient1与transient2是否相等:"+( _transient.Equals(transient)?"是":"否"),
                "singleton1与singleton2是否相等:"+( _singleton.Equals(singleton)?"是":"否"),
            };
        }


        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("sayhello")]
        [HttpGet]
        public string SayHello()
        {
            var service = _provider.GetRequiredService<IHelloService>();
            return service.SayHello(); ;
        }

        // PUT api/<HomeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
