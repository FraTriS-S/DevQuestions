using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage($"{nameof(Answer.Text)} должно быть заполнено")
            .MaximumLength(5000).WithMessage($"{nameof(Answer.Text)} невалидный");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage($"{nameof(Answer.UserId)} должно быть заполнено");
    }
}