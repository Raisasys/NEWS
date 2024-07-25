using Core;

namespace Commands;

public class AddActionCommand : Command<AddActionResponse>
{
    public Guid OrbitId { get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public ActionValue[] ActionItems { get; set; }
}
public class ActionValue
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Order{ get; set; }
    public Guid WhoPersonId { get; set; }
    public DateTime? OriginalDueDate { get; set; }

    public int? StartYear { get; set; }
    public Month? StartMonth { get; set; }
    public int? StartWeek { get; set; }

    public int? EndYear { get; set; }
    public Month? EndMonth { get; set; }
    public int? EndWeek { get; set; }
    public int Important { get; set; }
    public int Urgent { get; set; }
}

public class AddActionResponse
{
    public IEnumerable<ActionValueResponse> ActionItems { get; set; }
}

public class ActionValueResponse
{
    public Guid Id { get; set; }
    public long Key { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
}
/*public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull()
            .NotEmpty();
    }
}*/