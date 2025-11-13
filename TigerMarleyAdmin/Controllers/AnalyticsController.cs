using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAnalytics()
        {
            var totalOrders = _context.Orders.Count();
            var totalCustomers = _context.Customers.Count();

            var deliveredOrShippedOrders = _context.Orders
                .Where(o => o.Status == "Delivered" || o.Status == "Shipped")
                .ToList();

            decimal totalRevenue = 0;
            foreach (var order in deliveredOrShippedOrders)
            {
                //if (!string.IsNullOrEmpty(order.Total))
                {
                    //var numeric = new string(order.Total.Where(char.IsDigit).ToArray());
                    //if (decimal.TryParse(numeric, out var value))
                        //totalRevenue += value;
                }
            }

            var pendingOrders = _context.Orders.Count(o => o.Status == "Pending");
            var cancelledOrders = _context.Orders.Count(o => o.Status == "Cancelled");

            return Ok(new
            {
                totalOrders,
                totalCustomers,
                totalRevenue = $"₹{totalRevenue:N0}",
                pendingOrders,
                cancelledOrders
            });
        }
    }
}
