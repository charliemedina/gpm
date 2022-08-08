namespace Domain.ShapeAggregate
{
    public interface IShapeRepository
    {
        Task<Shape> Add(Shape shape);

        void Update(Shape shape);

        Task<Shape> GetAsync(int shapeId);
    }
}