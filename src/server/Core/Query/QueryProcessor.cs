using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class QueryProcessor
{
    public static async Task<TQueryResult> Execute<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken = default) 
        where TQuery: IQuery<TQueryResult>
        where TQueryResult : IQueryResult
    {
        var queryService = Context.Current.GetService<IQueryService<TQuery, TQueryResult>>();
        return await queryService.Execute(query, cancellationToken);
    }

    public static void InstallQueryService<TQueryService>(this IServiceCollection services)
        where TQueryService : QueryService
    {
        var queryServiceTypes = typeof(TQueryService).Assembly.GetTypes()
            .Where(i =>
                i.IsClass &&
                !i.IsAbstract &&
                i.BaseType is { IsGenericType: false } &&
                i.BaseType == typeof(QueryService)).ToList();

        foreach (var queryServiceType in queryServiceTypes)
        {
            var queryServiceInterfaceTypes = queryServiceType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryService<,>)).ToList();

            foreach (var queryServiceInterfaceType in queryServiceInterfaceTypes)
            {
                services.AddScoped(queryServiceInterfaceType, queryServiceType);
            }
        }
    }


}