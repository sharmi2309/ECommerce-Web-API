using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ECommerce.Repository.IRepository;
using System.Text.Json.Serialization;

namespace ECommerce.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
           

        [ForeignKey("Item")]
        public int Id { get; set; }
        public Items Item { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
       
       
       



    }
}
