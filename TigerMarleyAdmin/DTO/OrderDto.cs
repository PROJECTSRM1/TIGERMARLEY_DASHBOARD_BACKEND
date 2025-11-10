namespace TigerMarleyAdmin.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? Customer { get; set; }
        public string? Product { get; set; }
        public int Quantity { get; set; }
        public string? Total { get; set; }
        public string? Status { get; set; }
        public DateTime Date { get; set; }  
    }
}
