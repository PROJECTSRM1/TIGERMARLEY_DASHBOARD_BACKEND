namespace TigerMarleyAdmin.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
