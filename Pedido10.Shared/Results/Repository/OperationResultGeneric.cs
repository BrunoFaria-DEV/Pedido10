namespace Pedido10.Shared.Results.Repository
{
    public class OperationResultGeneric<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Result { get; set; }
    }
}