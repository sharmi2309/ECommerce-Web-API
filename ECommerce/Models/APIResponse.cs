using System.Net;

namespace ECommerce
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object result { get; set; }
        public string Message {  get; set; }
    }
}
