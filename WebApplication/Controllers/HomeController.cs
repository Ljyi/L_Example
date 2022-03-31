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
        public HomeController(IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
        {
            _transient = transientOperation;
            _singleton = singletonOperation;
            _scoped = scopedOperation;
        }
        // GET: api/<HomeeController>
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

        // GET api/<HomeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HomeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
