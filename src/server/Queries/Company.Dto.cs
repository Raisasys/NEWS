using Core;

namespace Queries;

public class CompanySimpleDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //public PersonDto Owner { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
}
public class CompanyDto : CompanySimpleDto
{
    public IEnumerable<PersonDto> Personnel { get; set; }
}


public class CompanyListDto : IListDto<CompanySimpleDto>
{
    public IEnumerable<CompanySimpleDto> Items { get; set; }
}


public class CompanyOwnerListDto : IListDto<PersonDto>
{
    public IEnumerable<PersonDto> Items { get; set; }
}

public class CompanyWhoListDto : IListDto<PersonDto>
{
    public IEnumerable<PersonDto> Items { get; set; }
}

public class PersonDto : IDto
{
    public Guid Id { get; set; }
    public UserValue User { get; set; }
    public string Name => User.Name;
    public string Email => User.Email;
    public PersonRole Role{ get; set; }
}
