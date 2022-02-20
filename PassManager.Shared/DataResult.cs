namespace PassManager.Shared
{
    public class DataResult<T> : BaseResult<DataResult<T>>
    {
        public T ExtensionData { get; private set; }

        public DataResult<T> WithExtensionData(T data)
        {
            this.ExtensionData = data;
            return this;
        } 
    }
}
