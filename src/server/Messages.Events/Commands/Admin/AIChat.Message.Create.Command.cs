using Core;
using Core.AI;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class CreateAIChatMessageCommand : AdminCommandBase
{
	public AIChatRole Role { get; set; }
	public string Message { get; set; }
	public int Order { get; set; }
	public Guid ChatId { get; set; }

	public List<MessageParameterItem> Parameters { get; set; }
}

public class MessageParameterItem
{
	public string Title { get; set; }
	public int Order { get; set; }
	public Guid ParameterId { get; set; }
}

public class CreateAIChatMessageCommandValidator : AbstractValidator<CreateAIChatMessageCommand>
{
	public CreateAIChatMessageCommandValidator()
	{
		RuleFor(x => x.Message)
			.NotNull()
			.NotEmpty();
	}
}