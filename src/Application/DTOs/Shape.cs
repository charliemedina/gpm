namespace Application.DTOs
{
    public class Shape
    {
        public List<Vertex> Vertices { get; set; } = new();
    }

    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}