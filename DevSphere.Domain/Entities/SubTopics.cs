namespace DevSphere.Domain.Entities;
public class SubTopic
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Slug { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int Position { get; set; }

    public DateTime? PublishedAt { get; set; }
    
    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }
    
    public Guid CreatedBy { get; set; } 

    public DateTime CreatedAt { get; set; }
    
    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }
}