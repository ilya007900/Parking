namespace Parking_Domain.FunctionalExtensions
{
    public class Result
    {
        public bool IsSuccess { get; }

        public string ErrorMessage { get; }

        private Result(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Success()
        {
            return new Result(true, default);
        }

        public static Result Failure(string message)
        {
            return new Result(false, message);
        }
    }
}