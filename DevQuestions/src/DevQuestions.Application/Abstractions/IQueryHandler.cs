namespace DevQuestions.Application.Abstractions;

public interface IQuery;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query, CancellationToken token);
}