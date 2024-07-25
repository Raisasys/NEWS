using Core;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings;

public class OrbitMapping : CustomEntityMapper<Orbit>
{
    public override void MapBuilder(EntityTypeBuilder<Orbit> builder)
    {
        builder.OwnsOne(e => e.Owner);

        /*builder.Property(e => e.MainConsultant)
            .HasConversion(
                v => (Guid?)v.ConsultantId,
                v => MainConsultant.Of(v));*/
        
        builder.HasMany(d => d.Consultants)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(d => d.Years)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.LongTermObjectives)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}


public class ParticipantConsultantMapping : CustomEntityMapper<ParticipantConsultant>
{
    public override void MapBuilder(EntityTypeBuilder<ParticipantConsultant> builder)
    {
        builder.OwnsOne(e => e.AssignedBy);
    }
}
public class LongTermObjectiveMapping : CustomEntityMapper<LongTermObjective>
{
    public override void MapBuilder(EntityTypeBuilder<LongTermObjective> builder)
    {
        builder.HasMany(d => d.ShortTermObjectives)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ShortTermObjectiveMapping : CustomEntityMapper<ShortTermObjective>
{
    public override void MapBuilder(EntityTypeBuilder<ShortTermObjective> builder)
    {
        builder.HasOne(d => d.Year);

        builder.OwnsOne(e => e.Status);

        builder.Property(e => e.Owner)
            .HasConversion(
                v => (Guid?)v.PersonId,
                v => ObjectiveOwner.Of(v));

        builder.HasMany(d => d.MonthlyStatuses)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Actions)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}


public class MonthlyStatusMapping : CustomEntityMapper<MonthlyStatus>
{
    public override void MapBuilder(EntityTypeBuilder<MonthlyStatus> builder)
    {
        builder.OwnsOne(e => e.Status);
    }
}

public class ActionMapping : CustomEntityMapper<ObjectiveAction>
{
    public override void MapBuilder(EntityTypeBuilder<ObjectiveAction> builder)
    {
        builder.Property(e => e.Who)
            .HasConversion(
                v => (Guid?)v.PersonId,
                v => ActionWho.Of(v));

        builder.OwnsOne(e => e.ImportantUrgent);
        builder.OwnsOne(e => e.Duration);

        builder.HasMany(d => d.Statuses)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Comments)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ActionStatusMapping : CustomEntityMapper<ActionStatus>
{
    public override void MapBuilder(EntityTypeBuilder<ActionStatus> builder)
    {
        builder.OwnsOne(e => e.Status);
    }
}



public class YearMapping : CustomEntityMapper<Year>
{
    public override void MapBuilder(EntityTypeBuilder<Year> builder)
    {
        builder.OwnsOne(e => e.Start);
        builder.OwnsOne(e => e.End);
    }
}