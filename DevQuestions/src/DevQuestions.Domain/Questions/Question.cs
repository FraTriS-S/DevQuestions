namespace DevQuestions.Domain.Questions;

public class Question
{
    public Question(
        Guid id,
        string title,
        string text,
        Guid? screenShotId,
        Guid userId,
        IEnumerable<Guid> tagsIds)
    {
        Id = id;
        Title = title;
        Text = text;
        ScreenShotId = screenShotId;
        UserId = userId;
        TagsIds = tagsIds.ToList();
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public Guid UserId { get; set; }

    public Guid? ScreenShotId { get; set; }

    public QuestionStatus Status { get; set; } = QuestionStatus.OPEN;

    public List<Guid> TagsIds { get; set; }

    public List<Answer> Answers { get; set; } = [];

    public Answer? Solution { get; set; }
}