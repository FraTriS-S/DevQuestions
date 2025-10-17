namespace DevQuestions.Domain.Questions;

public class Answer
{
    public Answer(Guid id, Guid userId, string text, Guid questionId)
    {
        Id = id;
        UserId = userId;
        Text = text;
        QuestionId = questionId;
        Rating = 0;
    }

    public Guid Id { get; init; }

    public string Text { get; init; }

    public Guid UserId { get; set; }

    public List<Guid> Comments { get; set; } = [];

    public long Rating { get; set; }

    public Question Question { get; set; } = null!;

    public Guid QuestionId { get; set; }
}