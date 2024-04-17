using System.Net;

namespace CoreWebAPIJWT.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessege { get; set; }

        public object Result { get; set; }
    }
}
