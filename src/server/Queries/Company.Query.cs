using Core;

namespace Queries;

public class GetCompanyByIdQuery : IQuery<CompanyDto>
{
    public Guid CompanyId { get; set; }
}

public class GetOwnersByCompanyIdQuery : IQuery<CompanyOwnerListDto>
{
    public Guid CompanyId { get; set; }
}

public class GetOwnerByCompanyIdOwnerIdQuery : IQuery<PersonDto>
{
    public Guid CompanyId { get; set; }
    public Guid OwnerId { get; set; }
}

public class GetWhosByCompanyIdQuery : IQuery<CompanyWhoListDto>
{
    public Guid CompanyId { get; set; }
}

public class GetWhoByCompanyIdWhoIdQuery : IQuery<PersonDto>
{
    public Guid CompanyId { get; set; }
    public Guid WhoId { get; set; }
}





public class GetCompanyListQuery : IQuery<CompanyListDto>
{
}
