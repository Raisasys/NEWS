using Core;
using Domain;
using FluentValidation;
using Newtonsoft.Json;

namespace Commands.Customer;

public class CreateChatCompletionRequestCommand : CustomerCommandBase
{
    public Guid AIChatId{ get; set; }
    public List<ChatParameterItem> ChatParameters { get; set; }
}

public class ChatParameterItem
{
	public Guid ParameterId { get; set; }
    
	[JsonIgnore]
	public string ParamSign { get; set; }

	public string Value { get; set; }
}


public class CreateChatCompletionRequestCommandValidator : AbstractValidator<CreateChatCompletionRequestCommand>
{
    public CreateChatCompletionRequestCommandValidator()
    {
        RuleFor(x => x.AIChatId)
            .NotNull()
            .NotEmpty();
    }
}