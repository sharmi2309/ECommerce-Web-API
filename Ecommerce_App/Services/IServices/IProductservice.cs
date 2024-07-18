using ECommerce_App.Models.DTO;

namespace Ecommerce_App.Services.IServices
{
    public interface IProductservice
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(ItemCreateDTO dto);
        Task<T> UpdateAsync<T>(ItemUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
