using System.ComponentModel.DataAnnotations;

namespace TigerMarleyAdmin.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public string Product { get; set; }

        public int Quantity { get; set; }

        public string? Total { get; set; }

        public string? Status { get; set; }

        public DateTime Date { get; set; }  
    }
}
