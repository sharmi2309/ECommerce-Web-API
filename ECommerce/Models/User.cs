using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Passwordsalt {  get; set; }
        public string Role { get; set; }

        public ICollection<Order>Order { get; set; }

  

    }
}
