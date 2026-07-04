namespace DevSphere.Domain.Entities;
public class McqOption
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public bool isCorrect { get; set; }
    public int Position { get; set; }
    //public DateTime? PublishedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid CreatedBy { get; set; } 
    public DateTime CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
}