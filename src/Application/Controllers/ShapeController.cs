using Application.Commands;
using Application.DTOs;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("shapes")]
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
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateShapeAsync([FromBody] Shape shape)
    {
        try
        {
            var commandResult = await _mediator.Send(new CreateShapeCommand(shape.Vertices));

            return Ok(commandResult.Identity);
        }
        catch (NullReferenceException ex)
        {
            _logger.LogError(ex, "Error while trying to create the shape");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [Route("{shapeId}/intersections")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetIntersectionVolume([FromRoute] int shapeId, [FromQuery] int id)
    {
        try
        {
            var intersectionVolume = await _mediator.Send(new GetIntersectionVolumeQuery(shapeId, id));

            return Ok(intersectionVolume);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while trying to calculate the intersection");

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
