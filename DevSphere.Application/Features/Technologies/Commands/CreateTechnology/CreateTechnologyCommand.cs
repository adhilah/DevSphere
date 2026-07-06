using DevSphere.Application.Features.Technologies.DTOs;
using MediatR;

namespace DevSphere.Application.Features.Technologies.Commands.CreateTechnology;

public sealed record CreateTechnologyCommand(

    int CategoryId,

    string Name,

    string? Description,
    
    string? Slug,

    string? ImageUrl,

    int Position

) : IRequest<CreateTechnologyResponse>;