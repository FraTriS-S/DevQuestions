namespace DevQuestions.Contracts.Questions.Dtos;

public record UpdateQuestionDto(string Title, string Text, Guid[] TagsIds);