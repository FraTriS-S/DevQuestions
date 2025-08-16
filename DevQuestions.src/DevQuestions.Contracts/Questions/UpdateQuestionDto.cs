namespace DevQuestions.Contracts.Questions;

public record UpdateQuestionDto(string Title, string Text, Guid[] TagsIds);