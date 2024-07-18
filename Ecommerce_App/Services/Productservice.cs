using ApplnEcommerce;
using Ecommerce_App.Models;
using Ecommerce_App.Services.IServices;
using ECommerce_App.Models.DTO;

namespace Ecommerce_App.Services
{
    public class Productservice:BaseService,IProductservice
    {
        public IHttpClientFactory _httpclientfactory;
        private string producturl;

        public Productservice(IHttpClientFactory httpclientfactory, IConfiguration configuration) : base(httpclientfactory)
        {
            _httpclientfactory = httpclientfactory;
            producturl = configuration.GetValue<string>("ServiceUrls:Ecommerceapi");
        }
        public Task<T> CreateAsync<T>(ItemCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apitype = SD.apitype.POST,
                Data = dto,
                Url = producturl + "/api/Ecommerceapi/"

            });

        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apitype = SD.apitype.DELETE,
                Url = producturl + "/api/Ecommerceapi/" + id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                apitype = SD.apitype.GET,
                Url = producturl + "/api/Ecommerceapi/"

            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                apitype = SD.apitype.GET,
                Url = producturl + "/api/Ecommerceapi/" + id

            });
        }

        public Task<T> UpdateAsync<T>(ItemUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                apitype = SD.apitype.PUT,
                Data = dto,
                Url = producturl + "/api/Ecommerceapi/" + dto.Id

            });
        }
    }
}
