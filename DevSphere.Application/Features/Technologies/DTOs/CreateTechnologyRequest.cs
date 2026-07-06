namespace DevSphere.Application.Features.Technologies.DTOs;

public sealed class CreateTechnologyRequest
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? ImageUrl { get; set; }
    public int Position { get; set; }
}