namespace Dcode.Caetering.Application.ResponseUtils
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public bool IsWarning { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool NoData { get; set; }


    }
}
