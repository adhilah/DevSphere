using DevSphere.Application.Features.Technologies.Commands.CreateTechnology;
using DevSphere.Application.Features.Technologies.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSphere.Api.Controllers;

[ApiController]
[Route("api/technologies")]
public sealed class TechnologyController : ControllerBase
{
    private readonly IMediator _mediator;

    public TechnologyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize(Roles = "Mentor")]
    [HttpPost]
    public async Task<ActionResult<CreateTechnologyResponse>> Create(
        [FromBody] CreateTechnologyRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTechnologyCommand(
            request.CategoryId,
            request.Name,
            request.Description,
            request.Slug,
            request.ImageUrl,
            request.Position
        );

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}