using Shared;

namespace DevQuestions.Application.Questions.Fails;

public class Errors
{
    public static class General
    {
        public static Failure NotFound(Guid id) =>
            Error.Failure("record.not.found", $"Запись по Id {id} не найдена").ToErrors();
    }

    public static class Questions
    {
        public static Failure TooManyQuestions() =>
            Error.Failure("questions.too.many", "Пользователь не может открыть больше 3 вопросов").ToErrors();

        public static Failure NotEnoughRating() =>
            Error.Failure("not.enough.rating", "Недостаточно рейтинга").ToErrors();
    }
}