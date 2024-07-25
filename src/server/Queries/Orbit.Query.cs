using Core;

namespace Queries;

public class GetOrbitByIdQuery : IQuery<OrbitDto>
{
    public Guid OrbitId { get; set; }
}

public class GetOrbitListByCompanyIdQuery : IQuery<OrbitListDto>
{
    public Guid CompanyId { get; set; }
}

public class GetOrbitListQuery : IQuery<OrbitListDto>
{
}

public class GetOrbitYearListByOrbitIdQuery : IQuery<OrbitYearListDto>
{
    public Guid OrbitId { get; set; }
}
