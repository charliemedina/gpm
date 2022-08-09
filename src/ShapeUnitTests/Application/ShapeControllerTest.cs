using Application.Controllers;
using Microsoft.Extensions.Logging;
using Shape = Application.DTOs.Shape;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Queries;

namespace ShapeUnitTests.Application
{
    public class ShapeControllerTest
    {
        private readonly Mock<ILogger<ShapeController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ShapeControllerTest()
        {
            _loggerMock = new Mock<ILogger<ShapeController>>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task CreateShape_IsOk_ReturnShapeId()
        {
            // Arrange
            var commandResult = new CommandResult(isSuccess: true, new Shape(), string.Empty);

            _mediatorMock.Setup(it => it.Send(It.IsAny<CreateShapeCommand>(), default)).ReturnsAsync(commandResult);

            var shapeController = new ShapeController(_loggerMock.Object, _mediatorMock.Object);

            // Act
            var actionResult = await shapeController.CreateShapeAsync(new Shape());

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, (actionResult as OkObjectResult)?.StatusCode);
        }

        [Fact]
        public async Task CreateShape_Failed_LogAndReturnTheError()
        {
            // Arrange
            var commandResult = new CommandResult(isSuccess: true, new Shape(), string.Empty);

            _mediatorMock.Setup(it => it.Send(It.IsAny<CreateShapeCommand>(), default)).Verifiable();

            var shapeController = new ShapeController(_loggerMock.Object, _mediatorMock.Object);

            // Act
            var actionResult = await shapeController.CreateShapeAsync(new Shape());

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (actionResult as StatusCodeResult)?.StatusCode);

            _loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString() == "Error while trying to create the shape"),
                    It.IsAny<NullReferenceException>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetIntersectionVolume_IsNotEmpty_ReturnIntersectionValue()
        {
            // Arrange
            const int intersectionResult = 10;
            _mediatorMock.Setup(it => it.Send(It.IsAny<GetIntersectionVolumeQuery>(), default)).ReturnsAsync(intersectionResult);

            var shapeController = new ShapeController(_loggerMock.Object, _mediatorMock.Object);

            // Act
            var actionResult = await shapeController.GetIntersectionVolume(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, (actionResult as OkObjectResult)?.StatusCode);
            Assert.Equal(intersectionResult, (actionResult as OkObjectResult)?.Value);
        }

        [Fact]
        public async Task GetIntersectionVolume_IsEmpty_ReturnZero()
        {
            // Arrange
            _mediatorMock.Setup(it => it.Send(It.IsAny<GetIntersectionVolumeQuery>(), default)).Verifiable();

            var shapeController = new ShapeController(_loggerMock.Object, _mediatorMock.Object);

            // Act
            var actionResult = await shapeController.GetIntersectionVolume(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, (actionResult as OkObjectResult)?.StatusCode);
            Assert.Equal(expected: 0, (actionResult as OkObjectResult)?.Value);
        }

        [Fact]
        public async Task GetIntersectionVolume_Failed_LogAndReturnTheError()
        {
            // Arrange
            _mediatorMock.Setup(it => it.Send(It.IsAny<GetIntersectionVolumeQuery>(), default)).ThrowsAsync(It.IsAny<Exception>());

            var shapeController = new ShapeController(_loggerMock.Object, _mediatorMock.Object);

            // Act
            var actionResult = await shapeController.GetIntersectionVolume(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, (actionResult as StatusCodeResult)?.StatusCode);

            _loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString() == "Error while trying to calculate the intersection"),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}