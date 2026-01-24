using System.Net;

namespace Magic_villa.Model
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCodes { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
