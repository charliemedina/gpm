using Application.Queries;

namespace ShapeUnitTests.Application
{
    public class GetIntersectionVolumeQueryHandlerTest
    {
        private readonly Mock<IShapeRepository> _shapeRepositoryMock;

        public GetIntersectionVolumeQueryHandlerTest()
        {
            _shapeRepositoryMock = new Mock<IShapeRepository>();
        }

        [Fact]
        public async Task GetIntersectionVolume_ExistingShapes_ReturnIntersectionValue()
        {
            // Arrange
            const int id1 = 1;
            const int id2 = 2;

            var message = new GetIntersectionVolumeQuery(id1, id2);

            _shapeRepositoryMock.Setup(it => it.GetAsync(id1)).ReturnsAsync(new Shape() { ShapeId = id1 });
            _shapeRepositoryMock.Setup(it => it.GetAsync(id2)).ReturnsAsync(new Shape() { ShapeId = id2 });

            var intersectionQueryHandler = new GetIntersectionVolumeQueryHandler(_shapeRepositoryMock.Object);

            // Act
            var result = await intersectionQueryHandler.Handle(message, CancellationToken.None);

            // Assert
            Assert.True(result > 0);
            _shapeRepositoryMock.Verify(repo => repo.GetAsync(id1), Times.Once);
            _shapeRepositoryMock.Verify(repo => repo.GetAsync(id2), Times.Once);
        }

        [Fact]
        public async Task GetIntersectionVolume_Shape1DoesNoExist_ReturnZero()
        {
            // Arrange
            const int id1 = 1;
            const int id2 = 2;

            var message = new GetIntersectionVolumeQuery(id1, id2);

            _shapeRepositoryMock.Setup(it => it.GetAsync(id1)).Verifiable();

            var intersectionQueryHandler = new GetIntersectionVolumeQueryHandler(_shapeRepositoryMock.Object);

            // Act
            var result = await intersectionQueryHandler.Handle(message, CancellationToken.None);

            // Assert
            Assert.Equal(expected: 0, result);
            _shapeRepositoryMock.Verify(repo => repo.GetAsync(id1), Times.Once);
            _shapeRepositoryMock.Verify(repo => repo.GetAsync(id2), Times.Never);
        }
    }
}