namespace App.Entities.Results
{
    public class MethodResult<T> : VoidResult
    {
        public MethodResult()
        {

        }

        public MethodResult(T payload)
        {
            Payload = payload;
        }

        public T Payload { get; set; }

        public static new MethodResult<T> CreateFaultedResult(string message = null)
        {
            return new MethodResult<T>
            {
                ErrorMessage = message,
                IsFaulted = true
            };
        }
    }
}
