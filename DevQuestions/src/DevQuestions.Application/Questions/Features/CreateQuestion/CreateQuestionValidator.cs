using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.Features.CreateQuestion;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage($"{nameof(Question.Title)} должно быть заполнено")
            .MaximumLength(500)
            .WithMessage($"{nameof(Question.Title)} невалидный");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage($"{nameof(Question.Text)} должно быть заполнено")
            .MaximumLength(5000).WithMessage($"{nameof(Question.Text)} невалидный");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage($"{nameof(Question.UserId)} должно быть заполнено");
    }
}