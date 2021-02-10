namespace BasketLibrary.Models
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }

        public static Response<T> Success(T data) { return new Response<T> { Data = data }; }
        public static Response<T> Error(string message, T data = null) { return new Response<T> { Data = data, IsError = true, ErrorMessage = message }; }
    }

    public class Response
    {
        public string Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; }

        public static Response Success(string data) { return new Response { Data = data }; }
        public static Response Error(string message, string data = null) { return new Response { Data = data, IsError = true, ErrorMessage = message }; }
    }
}