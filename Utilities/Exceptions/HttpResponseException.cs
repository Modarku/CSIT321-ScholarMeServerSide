namespace ScholarMeServer.Utilities.Exceptions
{
    // Utilize for handling validation errors in business logic (services)
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int statusCode, object? value = null) =>
            (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; }

        public object? Value { get; }
    }
}
