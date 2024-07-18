using System.ComponentModel.DataAnnotations;

namespace ECommerce_App.Models.DTO
{
    public class OrderCreateDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int Quantity { get; set; }
        
    }
}
