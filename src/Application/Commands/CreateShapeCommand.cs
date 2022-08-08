using System.Runtime.Serialization;
using Application.DTOs;
using MediatR;

namespace Application.Commands
{
    [DataContract]
    public class CreateShapeCommand : IRequest<CommandResult>
    {
        [DataMember]
        private readonly List<Vertex> _vertices;

        [DataMember]
        public IEnumerable<Vertex> Vertices => _vertices;

        public CreateShapeCommand()
        {
            _vertices = new List<Vertex>();
        }

        public CreateShapeCommand(List<Vertex> vertices)
        {
            _vertices = vertices;
        }
    }
}