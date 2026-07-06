using MediatR;
using DevSphere.Application.Features.Technologies.DTOs;

namespace DevSphere.Application.Features.Technologies.Queries.GetTechnologies;

public record GetTechnologiesQuery : IRequest<List<TechnologyResponse>>;