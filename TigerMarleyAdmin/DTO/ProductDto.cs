namespace TigerMarleyAdmin.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        //public string Category { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        //public int Stock { get; set; }
        //public string ProductStatus { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
