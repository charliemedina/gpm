using Vertex = Application.DTOs.Vertex;

namespace ShapeUnitTests.Application
{
    public class CreateShapeCommandHandlerTest
    {
        private readonly Mock<IShapeRepository> _shapeRepositoryMock;

        public CreateShapeCommandHandlerTest()
        {
            _shapeRepositoryMock = new Mock<IShapeRepository>();
        }

        [Fact]
        public async Task CreateShape_IsOk_ReturnCommandSuccess()
        {
            // Arrange
            const int id = 1;

            var message = new CreateShapeCommand(new List<Vertex>());

            _shapeRepositoryMock.Setup(it => it.Add(It.IsAny<Shape>())).ReturnsAsync(new Shape() { ShapeId = id });

            var createCommandHandler = new CreateShapeCommandHandler(_shapeRepositoryMock.Object);

            // Act
            var result = await createCommandHandler.Handle(message, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expected: id.ToString(), result.Identity);
            _shapeRepositoryMock.Verify(repo => repo.Add(It.IsAny<Shape>()), Times.Once);
        }

        [Fact]
        public async Task CreateShape_Failure_ReturnCommandFail()
        {
            // Arrange
            var message = new CreateShapeCommand(new List<Vertex>());

            _shapeRepositoryMock.Setup(it => it.Add(It.IsAny<Shape>())).Verifiable();

            var createCommandHandler = new CreateShapeCommandHandler(_shapeRepositoryMock.Object);

            // Act
            var result = await createCommandHandler.Handle(message, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            _shapeRepositoryMock.Verify(repo => repo.Add(It.IsAny<Shape>()), Times.Once);
        }
    }
}