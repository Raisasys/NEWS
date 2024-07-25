using Core;
using Core.AI;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class UpdateAIChatMessageCommand : AdminCommandBase
{
	public AIChatRole Role { get; set; }
	public string Message { get; set; }
	public int Order { get; set; }
	public Guid Id { get; set; }

	public List<MessageParameterItem> Parameters { get; set; }
}


public class UpdateAIChatMessageCommandValidator : AbstractValidator<UpdateAIChatMessageCommand>
{
	public UpdateAIChatMessageCommandValidator()
	{
		RuleFor(x => x.Message)
			.NotNull()
			.NotEmpty();
	}
}