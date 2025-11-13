using System;

namespace TigerMarleyAdmin.Models
{
    public class Customer
    {
        public int Id { get; set; }                          // int
        public string CustomerName { get; set; } = string.Empty;  // text / varchar
        public string Email { get; set; } = string.Empty;     // text / varchar
        public string Mobile { get; set; } = string.Empty;    // text / varchar (could be bigint, but better keep as string for phone)
        public long OrderCount { get; set; }                  // bigint → long
        public DateTime JoinDate { get; set; }                // timestamp → DateTime
        public bool IsActive { get; set; }                    // boolean
    }
}
