using Core;

namespace Queries;

public class ConsultantSimpleDto : IDto
{
    public Guid Id { get; set; }
    public string PicProfile { get; set; }
    public UserValue User { get; set; }
    public string Name => User.Name;
    public string Email => User.Email;
    public int ParticipantsCount { get; set; }
}
public class ConsultantDto : IDto
{
    public Guid Id { get; set; }
    public string PicProfile { get; set; }
    public string FirstName{ get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}


public class ConsultantListDto : IListDto<ConsultantSimpleDto>
{
    public IEnumerable<ConsultantSimpleDto> Items { get; set; }
}

