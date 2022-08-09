using Domain.ShapeAggregate;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShapeRepository : IShapeRepository
    {
        private readonly ShapesContext _context;

        public ShapeRepository(ShapesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Shape> Add(Shape shape)
        {
            await _context.Shapes.AddAsync(shape);
            await _context.SaveChangesAsync();

            return shape;
        }

        public async Task<Shape> GetAsync(int shapeId)
        {
            return await _context
                            .Shapes
                            .Include(v => v.Vertices)
                            .FirstOrDefaultAsync(shape => shape.ShapeId == shapeId);
        }

        public async void Update(Shape shape)
        {
            var shapeToUpdate = await _context.Shapes.FindAsync(shape.ShapeId);

            if (shapeToUpdate != null)
            {
                foreach (var vertex in shape.Vertices)
                {
                    shapeToUpdate.AddVertex(vertex.X, vertex.Y, vertex.Z);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}