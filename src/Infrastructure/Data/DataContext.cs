using Domain.ShapeAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shape> Shapes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}