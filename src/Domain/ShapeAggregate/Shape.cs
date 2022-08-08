using Domain.SeedWork;

namespace Domain.ShapeAggregate
{
    public class Shape : IAggregateRoot
    {
        public int ShapeId { get; set; }
        private readonly List<Vertex> _vertices;
        public IReadOnlyCollection<Vertex> Vertices => _vertices;

        public Shape()
        {
            _vertices = new List<Vertex>();
        }

        public void AddVertex(int x, int y, int z)
        {
            var vertex = Vertex.CreateVertex(x, y, z);

            _vertices.Add(vertex);
        }

        public int Intersect(Shape shape2)
        {
            var random = new Random();

            return random.Next(1, 100);
        }
    }
}