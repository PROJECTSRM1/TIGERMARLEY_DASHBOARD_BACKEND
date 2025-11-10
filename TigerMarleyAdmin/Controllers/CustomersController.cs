using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.Models;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;

            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(new List<Customer>
                {
                    new Customer
                    {
                        Name = "Priya Sharma",
                        Email = "priya.sharma@example.com",
                        Phone = "+91 98765 43210",
                        JoinDate = new DateTime(2024, 6, 15),
                        Status = "Active"
                    },
                    new Customer
                    {
                        Name = "Aman Verma",
                        Email = "aman.verma@example.com",
                        Phone = "+91 99876 54321",
                        JoinDate = new DateTime(2024, 8, 1),
                        Status = "Inactive"
                    },
                    new Customer
                    {
                        Name = "Riya Patel",
                        Email = "riya.patel@example.com",
                        Phone = "+91 91234 56789",
                        JoinDate = new DateTime(2023, 12, 10),
                        Status = "Active"
                    },
                    new Customer
                    {
                        Name = "Sahil Mehta",
                        Email = "sahil.mehta@example.com",
                        Phone = "+91 90012 34567",
                        JoinDate = new DateTime(2025, 2, 20),
                        Status = "Active"
                    }
                });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return Ok(_context.Customers.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            return Ok(customer);
        }
    }
}
