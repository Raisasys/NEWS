using Core;

namespace Commands;

public class UpdateActionCommand : Command
{
    public Guid OrbitId { get; set; }
    public Guid LongTermObjectiveId { get; set; }
    public Guid ShortTermObjectiveId { get; set; }
    public Guid ActionId { get; set; }
    public string Title { get; set; }
    public Guid WhoPersonId { get; set; }
    public DateTime? NewDueDate { get; set; }
    public Month? StartMonth { get; set; }
    public int? StartWeek { get; set; }
    public Month? EndMonth { get; set; }
    public int? EndWeek { get; set; }
    public int Important { get; set; }
    public int Urgent { get; set; }

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