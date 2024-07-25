using Core;

namespace Queries;

public class GetConsultantByIdQuery : IQuery<ConsultantDto>
{
    public Guid ConsultantId { get; set; }
}

public class GetConsultantListQuery : IQuery<ConsultantListDto>
{
}
