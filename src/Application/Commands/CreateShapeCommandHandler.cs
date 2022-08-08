using Domain.ShapeAggregate;
using MediatR;

namespace Application.Commands
{
    public class CreateShapeCommandHandler : IRequestHandler<CreateShapeCommand, CommandResult>
    {
        private readonly IShapeRepository _shapeRepository;

        public CreateShapeCommandHandler(IShapeRepository shapeRepository)
        {
            _shapeRepository = shapeRepository ?? throw new ArgumentNullException(nameof(shapeRepository));
        }

        public async Task<CommandResult> Handle(CreateShapeCommand message, CancellationToken cancellationToken)
        {
            var shape = new Shape();
            foreach (var vertex in message.Vertices)
            {
                shape.AddVertex(vertex.X, vertex.Y, vertex.Z);
            }

            var newShape = await _shapeRepository.Add(shape);

            return new CommandResult(newShape != null, newShape.ShapeId, "Error while trying to create the shape");
        }
    }
}