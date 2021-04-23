namespace App.Entities.Results
{
    public class VoidResult
    {
        public bool IsFaulted { get; set; }

        public string ErrorMessage { get; set; }

        public static VoidResult CreateFaultedResult(string message = null)
        {
            return new VoidResult
            {
                IsFaulted = true,
                ErrorMessage = message
            };
        }

        public static VoidResult Ok()
        {
            return new VoidResult();
        }
    }
}
