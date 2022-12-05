namespace LAB6.Models;

public class QuestionModel
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
    public bool Status { get; set; }
}