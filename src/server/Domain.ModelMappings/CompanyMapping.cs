using Core;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings;

public class CompanyMapping : CustomEntityMapper<Company>
{
    public override void MapBuilder(EntityTypeBuilder<Company> builder)
    {
        builder.OwnsOne(e => e.Address)
            .Property(e => e.Detail)
            .HasMaxLength(200);

        builder.Property(e=>e.Email)
            .HasConversion(
                v=>v.Value,
                v=>Email.Of(v));

        builder.Property(e => e.PhoneNumber)
            .HasConversion(
                v => v.Value,
                v => PhoneNumber.Of(v));

        /*builder.Property(e => e.Owner)
            .HasConversion(
                v => (Guid?)v.PersonId,
                v => CompanyOwner.Of(v));*/

        builder.HasMany(d => d.Personnel)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class PersonMapping : CustomEntityMapper<Person>
{
    public override void MapBuilder(EntityTypeBuilder<Person> builder)
    {
        builder.OwnsOne(e => e.User);
    }
}