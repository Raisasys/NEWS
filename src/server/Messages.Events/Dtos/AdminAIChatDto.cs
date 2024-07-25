using Commands.Customer;
using Core;
using Core.AI;
using Domain;

namespace Dtos;



public class AIChatAdminDto: AdminDto
{
	public string Title { get; set; }
	public bool Enabled { get; set; }
	public int Step { get; set; }
	public bool IsPremium { get; set; }
	public string AIModelName { get; set; }
	public string AIObjectType { get; set; }
	public int MaxTokens { get; set; }
	public string ApiKey { get; set; }
	public IList<AIChatMessageAdminDto> Messages { get; set; }
}

public class AIChatParameterAdminDto : AdminDto
{
	public string Name { get; set; }
	public string Sign { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public AIChatParameterType ParameterType { get; set; }
	public string DataSet { get; set; }
	public string[] DataSetItems { get; set; }
}


public class AIChatMessageAdminDto : AdminDto
{
	public AIChatRole Role { get; set; }
	public string Message { get; set; }
	public int Order { get; set; }
	public Guid ChatId { get; set; }
	public IList<AIChatMessageParameterAdminDto> Parameters { get; set; }
}


public class AIChatMessageParameterAdminDto : AdminDto
{
	public AIChatParameterAdminDto Parameter { get; set; }
	public Guid MessageId { get; set; }
	public string Title { get; set; }
	public int Order { get; set; }
	public string Description { get; set; }
}

