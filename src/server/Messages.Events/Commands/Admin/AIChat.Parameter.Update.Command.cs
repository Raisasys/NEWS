using Core;
using Domain;
using FluentValidation;

namespace Commands.Admin;
public class UpdateAIChatParameterCommand : AdminCommandBase
{
	public Guid Id { get; set; }
	public string Title { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> DataSetItems{ get; set; }
    public AIChatParameterType ParameterType { get; set; }
}

public class UpdateAIChatParameterCommandValidator : AbstractValidator<UpdateAIChatParameterCommand>
{
    public UpdateAIChatParameterCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}