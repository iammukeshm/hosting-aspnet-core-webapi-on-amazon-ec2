using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCRUD.API.Models;

namespace ProductCRUD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductsController(ProductDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null) { return NotFound(id); }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductRequest request)
        {
            var product = Product.Create(request.Name, request.Description, request.Price);
            await _context.Products.AddAsync(product);
            _context.SaveChanges();
            return Ok(product.Id);
        }
    }
}
