using System.ComponentModel.DataAnnotations;

namespace TigerMarleyAdmin.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Orders { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Status { get; set; }
    }
}
