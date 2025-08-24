using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions;

public class QuestionValidationException : BadRequestException
{
    public QuestionValidationException(params Error[] errors)
        : base(errors)
    {
    }
}