using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.DTO
{
    public class OrderCreateDTO
    {
        public int Id { get; set; }
       
        [Required]
        public string Username {  get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        
        
    }
}
