using Shared;

namespace DevQuestions.Application.Questions.Fails;

public partial class Errors
{
    public static class Questions
    {
        public static Shared.Errors TooManyQuestions =>
            Error.Failure("questions.too.many", "Пользователь не может открыть больше 3 вопросов").ToErrors();
    }
}