using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.DTOs;
using TigerMarleyAdmin.Models;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Get all inventory
        [HttpGet("list")]
        public async Task<IActionResult> GetInventory()
        {
            var items = await _context.GetInventoryFromFunctionAsync(-1);

            var inventoryDtos = items.Select(item => new InventoryDto
            {
                Id = item.Id,
                ProductName = item.ProductName,
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Price = item.Price,
                Stock = item.Stock,
                CreatedDate = item.CreatedDate.ToString("dd-MM-yyyy hh:mm:ss tt"),
                IsActive = item.IsActive
            }).ToList();

            return Ok(inventoryDtos);
        }

        // ✅ Get single inventory item
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInventoryItem(int id)
        {
            var items = await _context.GetInventoryFromFunctionAsync(id);
            var item = items.FirstOrDefault();

            if (item == null)
                return NotFound(new { message = "Inventory item not found" });

            var inventoryDto = new InventoryDto
            {
                Id = item.Id,
                ProductName = item.ProductName,
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Price = item.Price,
                Stock = item.Stock,
                CreatedDate = item.CreatedDate.ToString("dd-MM-yyyy hh:mm:ss tt"),
                IsActive = item.IsActive
            };

            return Ok(inventoryDto);
        }
    }
}
