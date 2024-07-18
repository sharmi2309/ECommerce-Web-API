using static ApplnEcommerce.SD;

namespace Ecommerce_App.Models
{
    public class APIRequest
    {
        public apitype apitype { get; set; } = apitype.GET;
        public string Url { get; set; }

        public object Data { get; set; }
    }
}
