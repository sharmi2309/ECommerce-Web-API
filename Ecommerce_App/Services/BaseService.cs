using ApplnEcommerce;
using Ecommerce_App.Models;
using Ecommerce_App.Services.IServices;
using ECommerce_App;
using Newtonsoft.Json;
using System.Text;

namespace Ecommerce_App.Services
{
    public class BaseService : IBaseServices
    {
        public APIResponse responsemod { get; set; }
        public IHttpClientFactory httpclient { get; set; }

        public BaseService(IHttpClientFactory httpclient)
        {
            this.responsemod = new();
            this.httpclient = httpclient;
        }

        public async Task<T> SendAsync<T>(APIRequest apirequest)
        {
            try
            {
                var client = httpclient.CreateClient("web_app");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apirequest.Url);
                if (apirequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apirequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apirequest.apitype)
                {
                    case SD.apitype.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.apitype.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.apitype.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;

                }
                HttpResponseMessage apiresponse = null;
                apiresponse = await client.SendAsync(message);

                var apiContent = await apiresponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiResponse != null && (apiresponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || apiresponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception ex)

            {
                var dto = new APIResponse
                {
                    ErrorMessage = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
