using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShapeController : ControllerBase
{
    private readonly ILogger<ShapeController> _logger;
    private readonly IMediator _mediator;

    public ShapeController(ILogger<ShapeController> logger, IMediator mediator)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> CreateShapeAsync([FromBody] Shape shape)
    {
        var commandResult = await _mediator.Send(new CreateShapeCommand(shape.Vertices));
        if (!commandResult.IsSuccess)
        {
            _logger.LogError(commandResult.Error);

            return StatusCodes.Status500InternalServerError;
        }

        return Ok(commandResult.Identity);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetIntersectionVolume([FromQuery] int id1, [FromQuery] int id2)
    {
        try
        {
            var intersectionVolume = await _mediator.Send(new GetIntersectionVolumeQuery(id1, id2));

            return Ok(intersectionVolume);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to calculate the intersection");

            return StatusCodes.Status500InternalServerError;
        }
    }
}
