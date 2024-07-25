namespace Core;


public interface IQueryService<in TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
    where TQueryResult : IQueryResult
{
    Task<TQueryResult> Execute(TQuery query, CancellationToken cancellationToken);
}
