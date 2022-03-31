namespace NetCoreApplication
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;

        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger, IOperationTransient transientOperation, IOperationSingleton singletonOperation)
        {
            _logger = logger;
            _transientOperation = transientOperation;
            _singletonOperation = singletonOperation;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IOperationScoped scopedOperation)
        {
            _logger.LogInformation(" -----------------------------------------------Transient: " + _transientOperation.OperationId + "每一次都会创建一个新的实例");
            _logger.LogInformation(" -----------------------------------------------Scoped:  " + scopedOperation.OperationId + "在同一个Scope内只初始化一个实例 ，可以理解为（ 每一个request级别只创建一个实例，同一个http request会在一个 scope内）");
            _logger.LogInformation("------------------------------------------------Singleton: " + _singletonOperation.OperationId + "整个应用程序生命周期以内只创建一个实例 ");
            await _next(context);
        }
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
