using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool isSuccessful { get; set; } = true;
        public List<String> errorMessage { get; set; }
        public object result { get; set; }
    }
}
