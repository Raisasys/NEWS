using Commands;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace CommandHandlers;

public class OrbitReportCommandHandlers : CommandHandlerBase,
    ICommandHandler<SetExecutionPlanCommand>
{
    private readonly IOrbitDomainService _orbitDomainService;

    public OrbitReportCommandHandlers(IOrbitDomainService orbitDomainService)
    {
        _orbitDomainService = orbitDomainService;
    }


    public async Task Handle(SetExecutionPlanCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Set<Orbit>()
            .Include(c => c.LongTermObjectives)
            .ThenInclude(c => c.ShortTermObjectives)
            .ThenInclude(c => c.Actions)
            .ThenInclude(c=>c.Statuses)
            .Include(c => c.Years)
            .SingleOrDefaultAsync(c => c.Id == command.OrbitId, cancellationToken: cancellationToken);

        foreach (var milestoneItem in command.MilestoneItems)
        {
            var longTerm = orbit.LongTermObjectives.FirstOrDefault(l => l.Id == milestoneItem.LongTermObjectiveId);
            if (longTerm!= null)
            {
                var shortTerm = longTerm.ShortTermObjectives.FirstOrDefault(s => s.Id == milestoneItem.ShortTermObjectiveId);
                foreach (var actionItem in milestoneItem.ActionItems)
                {
                    var action = shortTerm.Actions.FirstOrDefault(a => a.Id == actionItem.Id);
                    if (action!= null)
                    {
                        action.ImportantUrgent = actionItem.ImportantUrgent;
                        action.OriginalDueDate = actionItem.OriginalDueDate;
                        action.NewDueDate = actionItem.NewDueDate;

                        if (actionItem.StartDuration != actionItem.EndDuration)
                        {
                            var startDuration = actionItem.StartDuration.ToString();
                            var startYear = int.Parse(startDuration.Substring(0, 4));
                            var startMonth = (Month)int.Parse(startDuration.Substring(4, 2));
                            var startWeek = int.Parse(startDuration.Substring(6, 1));

                            var endDuration = actionItem.EndDuration.ToString();
                            var endYear = int.Parse(endDuration.Substring(0, 4));
                            var endMonth = (Month)int.Parse(endDuration.Substring(4, 2));
                            var endWeek = int.Parse(endDuration.Substring(6, 1));

                            action.Duration = new ActionDuration(startYear, startMonth, startWeek, endYear, endMonth, endWeek);
                        }

                        if (actionItem.CurrentStatus != null &&
                            (actionItem.CurrentStatus.Direction != null || actionItem.CurrentStatus.RAG != null) &&
                            (actionItem.CurrentStatus.Direction != action.CurrentStatus?.Direction ||
                             actionItem.CurrentStatus.RAG != action.CurrentStatus?.RAG))
                        {
                            action.Statuses.Add(new ActionStatus(new Status(actionItem.CurrentStatus.RAG, actionItem.CurrentStatus.Direction)));
                        }
                    }
                }
            }
        }

        Database.Update(orbit);
        await Database.SaveChanges(cancellationToken);
    }
}