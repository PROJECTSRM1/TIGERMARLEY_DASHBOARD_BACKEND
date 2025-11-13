namespace TigerMarleyAdmin.DTOs
{
    public class SupportTicketDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        //public int Quantity { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        //public bool IsActive { get; set; }
    }
}
