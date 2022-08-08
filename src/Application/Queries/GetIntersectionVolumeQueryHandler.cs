using Domain.ShapeAggregate;
using MediatR;

namespace Application.Queries
{
    public class GetIntersectionVolumeQueryHandler : IRequestHandler<GetIntersectionVolumeQuery, int>
    {
        private readonly IShapeRepository _shapeRepository;

        public GetIntersectionVolumeQueryHandler(IShapeRepository shapeRepository)
        {
            _shapeRepository = shapeRepository;
        }

        public async Task<int> Handle(GetIntersectionVolumeQuery message, CancellationToken cancellationToken)
        {
            var shape1 = await _shapeRepository.GetAsync(message.ShapeId1);

            if (shape1 is null)
            {
                return 0;
            }

            var shape2 = await _shapeRepository.GetAsync(message.ShapeId2);

            if (shape2 is null)
            {
                return 0;
            }

            return shape1.Intersect(shape2);
        }
    }
}