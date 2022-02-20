namespace PassManager.Shared
{
    public abstract class BaseResult<T> where T : BaseResult<T>
    {
        public bool Success { get; private set; } = true;

        public string ErrorMessage { get; private set; }

        public T WithErrorMessage(string errorMessage)
        {
            this.Success = false;
            this.ErrorMessage = errorMessage;

            return (T)this;
        }
    }
}
