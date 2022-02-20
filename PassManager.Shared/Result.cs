namespace PassManager.Shared
{
    public class Result : BaseResult<Result>
    {
        public static Result Ok()
            => new Result();

        public static DataResult<T> Of<T>()
            => new DataResult<T>();

        public static Result Failed(string errorMessage)
            => new Result().WithErrorMessage(errorMessage);
    }
}
