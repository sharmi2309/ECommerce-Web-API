namespace ECommerce_App.Models.DTO
{
    public class ItemCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
