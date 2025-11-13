using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.DTOs;
using TigerMarleyAdmin.Models;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all customers
        [HttpGet("list")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.GetCustomersFromFunctionAsync(-1);

            var customerDtos = customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Email = c.Email,
                Mobile = c.Mobile,
                OrderCount = c.OrderCount,
                JoinDate = c.JoinDate,
                IsActive = c.IsActive
            }).ToList();

            return Ok(customerDtos);
        }

        // ✅ Get single customer
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customers = await _context.GetCustomersFromFunctionAsync(id);
            var customer = customers.FirstOrDefault();

            if (customer == null)
                return NotFound(new { message = "Customer not found" });

            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                Mobile = customer.Mobile,
                OrderCount = customer.OrderCount,
                JoinDate = customer.JoinDate,
                IsActive = customer.IsActive
            };

            return Ok(customerDto);
        }
    }
}
