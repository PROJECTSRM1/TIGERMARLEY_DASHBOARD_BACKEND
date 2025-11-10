using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.DTO;
using TigerMarleyAdmin.Models;


namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;

            if (!_context.Orders.Any())
            {
                _context.Orders.AddRange(new List<Order>
                {
                    new Order { Id = 101, Customer = "Priya Sharma", Product = "Classic White Shirt", Quantity = 2, Total = "₹2998", Status = "Delivered", Date = DateTime.Parse("2025-10-28") },
                    new Order { Id = 102, Customer = "Aman Verma", Product = "Denim Jacket", Quantity = 1, Total = "₹2499", Status = "Pending", Date = DateTime.Parse("2025-10-30") },
                    new Order { Id = 103, Customer = "Riya Patel", Product = "Tiger Hoodie", Quantity = 3, Total = "₹8397", Status = "Shipped", Date = DateTime.Parse("2025-10-29") },
                    new Order { Id = 104, Customer = "Sahil Mehta", Product = "Graphic Tee", Quantity = 4, Total = "₹3996", Status = "Cancelled", Date = DateTime.Parse("2025-10-27") }
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetOrders()
        {
            var orders = _context.Orders
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Customer = o.Customer,
                    Product = o.Product,
                    Quantity = o.Quantity,
                    Total = o.Total,
                    Status = o.Status,
                    Date = o.Date
                })
                .ToList();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrder(int id)
        {
            var order = _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Customer = o.Customer,
                    Product = o.Product,
                    Quantity = o.Quantity,
                    Total = o.Total,
                    Status = o.Status,
                    Date = o.Date
                })
                .FirstOrDefault();

            if (order == null)
                return NotFound(new { message = "Order not found" });

            return Ok(order);
        }
    }
}
