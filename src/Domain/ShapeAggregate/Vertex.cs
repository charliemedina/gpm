namespace Domain.ShapeAggregate
{
    public class Vertex
    {
        public int VertexId { get; set; }
        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }

        public Vertex(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vertex CreateVertex(int x, int y, int z)
        {
            return new Vertex(x, y, z);
        }
    }
}