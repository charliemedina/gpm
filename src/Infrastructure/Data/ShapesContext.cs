using Domain.ShapeAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ShapesContext : DbContext
    {
        public DbSet<Shape> Shapes { get; set; }

        public ShapesContext(DbContextOptions<ShapesContext> options) : base(options) { }
    }
}