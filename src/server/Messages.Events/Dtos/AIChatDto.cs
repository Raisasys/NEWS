using Core;
using Domain;

namespace Dtos;

public class AIChatDto: Dto
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public int Step { get; set; }

	public List<AIChatParamDto> Parameters { get; } = new();
}

public class AIChatParamDto: Dto
{
	public AIChatParamDto(Guid id, string title, string description, AIChatParameterType parameterType, string[] dataSetItems, int order)
	{
		Id = id;
		Title = title;
		Description = description;
		ParameterType = parameterType;
		DataSetItems = dataSetItems;
		Order = order;
	}

	public Guid Id { get; }
	public string Title { get; }
	public string Description { get; }
	public AIChatParameterType ParameterType { get; }
	public string[] DataSetItems { get;  }
	public int Order { get; }
}
