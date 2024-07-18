using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models.DTO
{
    public class OrderDTO
    {
        
        public int OrderId { get; set; }
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ItemsDTO Item { get; set; }
        public UserDTO User { get; set; }



    }
}
