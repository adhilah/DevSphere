namespace DevSphere.Domain.Entities;
public class TestCase
{
    public int Id { get; set; }
    public int TechnologyId { get; set; }
    public string Slug { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int Position { get; set; }

    public DateTime? PublishedAt { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
    
    public Guid CreatedBy { get; set; } 

    public DateTime CreatedAt { get; set; }
    
    public Guid? UpdatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }
}