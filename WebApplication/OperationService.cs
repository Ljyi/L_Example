namespace NetCoreApplication
{
    public class OperationService : IOperationService
    {
        private readonly IOperationTransient _transient;
        private readonly IOperationSingleton _singleton;
        private readonly IOperationScoped _scoped;
        public OperationService(IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
        {
            _transient = transientOperation;
            _singleton = singletonOperation;
            _scoped = scopedOperation;
        }
        public string GetOperation()
        {
            return _scoped.OperationId;
        }
    }
}
