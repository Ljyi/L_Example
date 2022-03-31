using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NetCoreApplication
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationScoped _scopedOperation;

        public IndexModel(ILogger<IndexModel> logger,
                          IOperationTransient transientOperation,
                          IOperationScoped scopedOperation,
                          IOperationSingleton singletonOperation)
        {
            _logger = logger;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }

        public void OnGet()
        {
            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + _scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);
        }
    }
}
