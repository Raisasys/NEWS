using Commands;
using Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CommandHandlers;

public class OrbitCommandHandlers : CommandHandlerBase,
    ICommandHandler<CreateOrbitCommand, CreateOrbitResponse>,
    ICommandHandler<AddLongTermObjectiveCommand, AddLongTermObjectiveResponse>,
    ICommandHandler<AddShortTermObjectiveCommand, AddShortTermObjectiveResponse>,
    ICommandHandler<SetShortTermObjectiveStatusCommand, SetShortTermObjectiveStatusResponse>,
    ICommandHandler<AddActionCommand, AddActionResponse>,
    ICommandHandler<SetActionStatusCommand, SetActionStatusResponse>,
    ICommandHandler<AddActionCommentCommand, AddActionCommentResponse>,
    ICommandHandler<AddConsultantCommand, AddConsultantResponse>,
    ICommandHandler<AttachObjectivesCommand, AttachObjectivesResponse>,
    ICommandHandler<AttachLongTermObjectiveCommand, AttachObjectivesResponse>
{
    private readonly IOrbitDomainService _orbitDomainService;

    public OrbitCommandHandlers(IOrbitDomainService orbitDomainService)
    {
        _orbitDomainService = orbitDomainService;
    }


    public async Task<CreateOrbitResponse> Handle(CreateOrbitCommand command, CancellationToken cancellationToken)
    {
        var company = await Database.Find<Company>(command.CompanyId);
        var orbit = _orbitDomainService.CreateOrbit(command.Title,new OwnerCompany(company.Id,company.Name),command.StartYear,command.StartMonth,command.YearDuration);
        await Database.SaveChanges(cancellationToken);
        return new CreateOrbitResponse
        {
            OrbitId= orbit.Id,
            OrbitKey = orbit.Key,
            Years = orbit.Years.Select(y=> new YearValue
            {
                YearId = y.Id, 
                //StartMonth = y.StartMonth,EndMonth = y.EndMonth, 
                Value = y.Value,YearKey = y.Key
            }),
            //MainParticipantConsultantId = orbit.MainConsultant.ConsultantId
        };
    }
    

    public async Task<AddLongTermObjectiveResponse> Handle(AddLongTermObjectiveCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        foreach (var longTermObjectiveValue in command.ObjectiveItems)
            orbit.AddLongTermObjective(longTermObjectiveValue.Id, longTermObjectiveValue.Title, longTermObjectiveValue.Order);
        await Database.SaveChanges(cancellationToken);
        return new AddLongTermObjectiveResponse
        {
            ObjectiveItems = orbit.LongTermObjectives.Select(i => new LongTermObjectiveValueResponse
            {
                Id = i.Id,
                Key = i.Key,
                Title = i.Title,
                Order = i.Order
            })
        };
    }

    public async Task<AddShortTermObjectiveResponse> Handle(AddShortTermObjectiveCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var longTermObj = orbit.LongTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);

        foreach (var shortTermObjectiveItem in command.ShortTermObjectiveItems)
        {
            var year = orbit.Years.FirstOrDefault(i => i.Id == shortTermObjectiveItem.YearId);
            longTermObj.AddShortTermObjective(shortTermObjectiveItem.Id, year,shortTermObjectiveItem.Title, shortTermObjectiveItem.OwnerPersonId);
        }
            
        await Database.SaveChanges(cancellationToken);
        return new AddShortTermObjectiveResponse
        {
            ObjectiveItems = longTermObj.ShortTermObjectives.Select(i => new ShortTermObjectiveValueResponse
            {
                Id = i.Id,
                Key = i.Key,
                Title = i.Title,
                PersonId= i.Owner.PersonId,
                YearId = i.Year.Id
            })
        };
    }

    public async Task<SetShortTermObjectiveStatusResponse> Handle(SetShortTermObjectiveStatusCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var longTermObj = orbit.LongTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var shortTermObj = longTermObj.ShortTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var state = shortTermObj.SetStatus(command.Month, command.Direction, command.RAG);
        await Database.SaveChanges(cancellationToken);
        return new SetShortTermObjectiveStatusResponse
        {
            StatusId = state.Id
        };
    }

    public async Task<AddActionResponse> Handle(AddActionCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var longTermObj = orbit.LongTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var shortTermObj = longTermObj.ShortTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);

        foreach (var actionItem in command.ActionItems)
        {
            shortTermObj.AddAction(
                actionItem.Id,
                actionItem.Title,actionItem.Order, actionItem.OriginalDueDate, actionItem.WhoPersonId, 
                actionItem.Important, actionItem.Urgent,
                actionItem.StartYear, actionItem.StartMonth, actionItem.StartWeek, actionItem.EndYear, actionItem.EndMonth, actionItem.EndWeek);
        }
        await Database.SaveChanges(cancellationToken);
        return new AddActionResponse
        {
            ActionItems = shortTermObj.Actions.Select(i=> new ActionValueResponse
            {
                Id = i.Id,
                Order = i.Order,
                Title = i.Title,
                Key = i.Key
            })
        };
    }

    public async Task<SetActionStatusResponse> Handle(SetActionStatusCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var longTermObj = orbit.LongTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var shortTermObj = longTermObj.ShortTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var action = shortTermObj.Actions.FirstOrDefault(i => i.Id == command.ActionId);
        var state = action.SetStatus(command.Direction, command.RAG);
        await Database.SaveChanges(cancellationToken);
        return new SetActionStatusResponse
        {
            StatusId = state.Id
        };
    }

    public async Task<AddActionCommentResponse> Handle(AddActionCommentCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var longTermObj = orbit.LongTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var shortTermObj = longTermObj.ShortTermObjectives.FirstOrDefault(i => i.Id == command.LongTermObjectiveId);
        var action = shortTermObj.Actions.FirstOrDefault(i => i.Id == command.ActionId);
        var comment = action.AddComment(command.Comment);
        await Database.SaveChanges(cancellationToken);
        return new AddActionCommentResponse
        {
            CommentId= comment.Id
        };
    }

    public async Task<AddConsultantResponse> Handle(AddConsultantCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Find<Orbit>(command.OrbitId);
        var consultant = await Database.Find<Consultant>(command.ConsultantId);
        var participantConsultant = new ParticipantConsultant(consultant.Id,command.Permission);
        orbit.Consultants.Add(participantConsultant);
        await Database.SaveChanges(cancellationToken);
        return new AddConsultantResponse
        {
            ParticipantConsultantId = participantConsultant.Id
        };
    }

    public async Task<AttachObjectivesResponse> Handle(AttachObjectivesCommand command, CancellationToken cancellationToken)
    {
        var orbit = await Database.Set<Orbit>()
            .Include(c=>c.LongTermObjectives)
            .ThenInclude(c => c.ShortTermObjectives)
            .ThenInclude(c=>c.Actions)
            .Include(c=>c.Years)
            .SingleOrDefaultAsync(c=>c.Id == command.OrbitId, cancellationToken: cancellationToken);

        foreach (var longTermObjectiveValue in command.LongTermObjectiveItems)
        {
            var longTerm = orbit.LongTermObjectives.FirstOrDefault(l => l.Id == longTermObjectiveValue.Id);
            if (longTerm != null)
            {
                UpdateLongTerm(orbit, longTerm,longTermObjectiveValue);
            }
            else
            {
                AddLongTerm(orbit, longTermObjectiveValue);
            }
        }
        await Database.SaveChanges(cancellationToken);

        return new AttachObjectivesResponse();
    }

    public async Task<AttachObjectivesResponse> Handle(AttachLongTermObjectiveCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var orbit = await Database.Set<Orbit>()
                .Include(c => c.LongTermObjectives)
                .ThenInclude(c => c.ShortTermObjectives)
                .ThenInclude(c => c.Actions)
                .Include(c => c.Years)
                .SingleOrDefaultAsync(c => c.Id == command.OrbitId, cancellationToken: cancellationToken);


            var longTerm = orbit.LongTermObjectives.FirstOrDefault(l => l.Id == command.LongTermObjectiveItem.Id);
            if (longTerm != null)
            {
                UpdateLongTerm(orbit, longTerm, command.LongTermObjectiveItem);
            }
            else
            {
                AddLongTerm(orbit, command.LongTermObjectiveItem);
            }

            await Database.SaveChanges(cancellationToken);

            return new AttachObjectivesResponse();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    private void UpdateLongTerm(Orbit orbit, LongTermObjective longTerm, LongTermObjectiveValueItem longTermObjectiveValue)
    {
        longTerm.Title = longTermObjectiveValue.Title;
        longTerm.Order = longTermObjectiveValue.Order;

        foreach (var shortTermObjectiveValue in longTermObjectiveValue.ShortTermObjectiveItems)
        {
            var shortTerm = longTerm.ShortTermObjectives.FirstOrDefault(c => c.Id == shortTermObjectiveValue.Id);
            if (shortTerm != null)
            {
                UpdateShortTerm(orbit, shortTerm, shortTermObjectiveValue);
            }
            else
            {
                AddShortTerm(orbit, longTerm, shortTermObjectiveValue);
            }
        }
    }
    private void AddLongTerm(Orbit orbit, LongTermObjectiveValueItem longTermObjectiveValue)
    {
        var longTerm = orbit.AddLongTermObjective(longTermObjectiveValue.Id, longTermObjectiveValue.Title, longTermObjectiveValue.Order);

        foreach (var shortTermObjectiveValue in longTermObjectiveValue.ShortTermObjectiveItems)
        {
            AddShortTerm(orbit, longTerm, shortTermObjectiveValue);
        }
    }
    private void UpdateShortTerm(Orbit orbit, ShortTermObjective shortTerm, ShortTermObjectiveValueItem shortTermObjectiveValue)
    {
        shortTerm.Title = shortTermObjectiveValue.Title;
        shortTerm.Year = orbit.Years.FirstOrDefault(y=>y.Id  == shortTermObjectiveValue.YearId);
        shortTerm.Owner = ObjectiveOwner.Of(shortTermObjectiveValue.OwnerId);

        foreach (var actionValue in shortTermObjectiveValue.ActionItems)
        {
            var action = shortTerm.Actions.FirstOrDefault(c => c.Id == actionValue.Id);
            if (action != null)
            {
                UpdateAction(action, actionValue);
            }
            else
            {
                shortTerm.AddAction(actionValue.Id, actionValue.Title, actionValue.Order, actionValue.OriginalDueDate, actionValue.WhoId);
            }
        }
    }
    private void AddShortTerm(Orbit orbit, LongTermObjective longTerm, ShortTermObjectiveValueItem shortTermObjectiveValue)
    {
        var year = orbit.Years.FirstOrDefault(y => y.Id == shortTermObjectiveValue.YearId);
        var shortTerm = longTerm.AddShortTermObjective(shortTermObjectiveValue.Id, year,shortTermObjectiveValue.Title,shortTermObjectiveValue.OwnerId);

        foreach (var actionValue in shortTermObjectiveValue.ActionItems)
        {
            shortTerm.AddAction(actionValue.Id, actionValue.Title, actionValue.Order, actionValue.OriginalDueDate, actionValue.WhoId);
        }
    }
    private void UpdateAction(ObjectiveAction action, ActionObjectiveValueItem actionValue)
    {
        action.Title = actionValue.Title;
        action.Order = actionValue.Order;
        action.OriginalDueDate = actionValue.OriginalDueDate;
        action.Who = ActionWho.Of(actionValue.WhoId);
    }

    
}