namespace DevQuestions.Contracts;

public record UpdateQuestionDto(string Title, string Text, Guid[] TagsIds);