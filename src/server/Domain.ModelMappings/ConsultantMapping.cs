using Data;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.ModelMappings;

public class ConsultantMapping : CustomEntityMapper<Consultant>
{
    public override void MapBuilder(EntityTypeBuilder<Consultant> builder)
    {
        builder.OwnsOne(e => e.User);
    }
}
