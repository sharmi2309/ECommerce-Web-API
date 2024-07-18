using Ecommerce_App.Models;
using ECommerce_App;

namespace Ecommerce_App.Services.IServices
{
    public interface IBaseServices
    {
        APIResponse responsemod { get; set; }
        Task<T> SendAsync<T>(APIRequest apirequest);
    }
}
