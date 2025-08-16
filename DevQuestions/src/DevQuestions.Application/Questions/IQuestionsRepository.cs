using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Question> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> UpdateAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<int> GetOpenedUserQuestionsCountAsync(Guid userId, CancellationToken cancellationToken);
}