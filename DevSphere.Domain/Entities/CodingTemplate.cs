namespace DevSphere.Domain.Entities;

public class CodeTemplate
{
    public int Id { get; set; }
    public int TechnologyId { get; set; }
    public int QuestionId { get; set; }
    public string StarterCode { get; set; } = string.Empty;
    public string SolutionCode { get; set; } = string.Empty;
    public DateTime? PublishedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid CreatedBy { get; set; } 
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}