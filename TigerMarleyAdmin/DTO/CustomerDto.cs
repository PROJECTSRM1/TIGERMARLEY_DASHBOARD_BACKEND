using System;

namespace TigerMarleyAdmin.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public long OrderCount { get; set; }      // bigint → long
        public DateTime JoinDate { get; set; }    // timestamp → DateTime
        public bool IsActive { get; set; }        // boolean
    }
}
