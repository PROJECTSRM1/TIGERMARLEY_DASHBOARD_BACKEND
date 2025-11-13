namespace TigerMarleyAdmin.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty; // formatted with ₹ symbol
        public string Date { get; set; } = string.Empty;  // formatted for display
    }
}
