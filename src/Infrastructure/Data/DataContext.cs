using Domain.ShapeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Shape> Shapes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }

    public class DataContextDesignFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=ShapeDb;Integrated Security=true;Pooling=true;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
