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

            if (!_context.Products.Any())
            {
                _context.Products.AddRange(new List<Product>
                {
                    new Product { Id = 1, Name = "Classic White Shirt", Category = "Essentials", Price = 1499, Stock = 24, ProductStatus = "Available" },
                    new Product { Id = 2, Name = "Denim Jacket", Category = "Limited Edition", Price = 2499, Stock = 12, ProductStatus = "Low Stock" },
                    new Product { Id = 3, Name = "Graphic Tee", Category = "Fandom Fusion", Price = 999, Stock = 50, ProductStatus = "Available" },
                    new Product { Id = 4, Name = "Tiger Hoodie", Category = "Official Merchandise", Price = 2799, Stock = 0, ProductStatus = "Out of Stock" },
                    new Product { Id = 5, Name = "Custom Cap", Category = "Customize", Price = 799, Stock = 30, ProductStatus = "Available" }
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound(new { message = "Product not found" });

            return Ok(product);
        }
    }
}
