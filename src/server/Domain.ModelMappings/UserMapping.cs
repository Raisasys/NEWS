using Core;
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings;

public class UserMapping : CustomEntityMapper<User>
{
    public override void MapBuilder(EntityTypeBuilder<User> builder)
    {
        builder.OwnsOne(e => e.FullName);
        builder.OwnsOne(e => e.Address);


        builder.Property(e => e.PhoneNumber)
            .HasConversion(
                v => v.Value,
                v => PhoneNumber.Of(v));
    }
}