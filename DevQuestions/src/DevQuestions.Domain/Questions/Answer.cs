namespace DevQuestions.Domain.Questions;

public class Answer
{
    public Guid Id { get; init; }

    public required string Text { get; init; }

    public required Guid UserId { get; set; }

    public List<Guid> Comments { get; set; } = [];

    public required Question Question { get; set; }
}