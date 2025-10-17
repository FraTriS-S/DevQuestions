using System.Data;
using DevQuestions.Application.Database;

namespace DevQuestions.Infrastructure.PostgreSql;

public class TransactionManager : ITransactionManager
{
    public Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
}