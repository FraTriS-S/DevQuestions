namespace DevQuestions.Domain.Questions;

public enum QuestionStatus
{
    OPEN = 1,
    RESOLVED = 2,
}

public static class QuestionStatusExtensions
{
    public static string ToRuString(this QuestionStatus status) =>
        status switch
        {
            QuestionStatus.OPEN => "Открыт",
            QuestionStatus.RESOLVED => "Решен",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}