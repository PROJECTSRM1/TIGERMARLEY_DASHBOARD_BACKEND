using Microsoft.AspNetCore.Mvc;
using TigerMarleyAdmin.Data;
using TigerMarleyAdmin.Models;

namespace TigerMarleyAdmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // 🔹 GET all products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.GetProductsFromFunctionAsync(-1);
            return Ok(products);
        }

        // 🔹 GET product by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var products = await _context.GetProductsFromFunctionAsync(id);
            var product = products.FirstOrDefault();

            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }
    }
}
