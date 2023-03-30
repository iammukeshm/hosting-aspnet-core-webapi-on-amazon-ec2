using Microsoft.EntityFrameworkCore;
using ProductCRUD.API.Models;

namespace ProductCRUD.API;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }
}
