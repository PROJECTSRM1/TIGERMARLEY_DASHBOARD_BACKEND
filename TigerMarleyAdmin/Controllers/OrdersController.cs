using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.DTOs;
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
        }

        // ✅ Get all orders
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.GetOrdersFromFunctionAsync(-1);

            var orderDtos = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                Customer = order.CustomerName,
                Product = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price,
                Status = order.Status,
                Total = $"₹{order.Total:F2}",
                Date = order.OrderDate.ToString("dd-MM-yyyy hh:mm:ss tt") // ✅ nicely formatted date
            }).ToList();

            return Ok(orderDtos);
        }

        // ✅ Get single order by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var orders = await _context.GetOrdersFromFunctionAsync(id);
            var order = orders.FirstOrDefault();

            if (order == null)
                return NotFound(new { message = "Order not found" });

            var orderDto = new OrderDto
            {
                Id = order.Id,
                Customer = order.CustomerName,
                Product = order.ProductName,
                Quantity = order.Quantity,
                Price = order.Price,
                Status = order.Status,
                Total = $"₹{order.Total:F2}",
                Date = order.OrderDate.ToString("dd-MM-yyyy hh:mm:ss tt")
            };

            return Ok(orderDto);
        }
    }
}
