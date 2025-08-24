using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions;

public class TooManyQuestionsException : BadRequestException
{
    public TooManyQuestionsException()
        : base(Errors.Questions.TooManyQuestions)
    {
    }
}