namespace Core;

public interface IQueryResult { }
public interface IDto : IQueryResult { }

public interface IListDto<out TDto> : IQueryResult where TDto : IDto
{
    IEnumerable<TDto> Items { get; }
}

public interface IPagedListDto<out TDto> : IListDto<TDto> where TDto : IDto
{
    Pagination Pagination { get; }
}

public record PagedResponse<TDto>(IEnumerable<TDto> Items, Pagination Pagination): IPagedListDto<TDto> where TDto : IDto
{
    public Pagination Pagination { get; } = Pagination;
    public IEnumerable<TDto> Items { get; } = Items;
}


public interface IQuery<out TQueryResult> : IRequest<TQueryResult> where TQueryResult : IQueryResult { }

public interface IStreamQuery<out TQueryResult> : IStreamRequest<TQueryResult> where TQueryResult : IQueryResult { }