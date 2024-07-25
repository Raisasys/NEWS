using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

/*
public class CustomerSubscriptionMapper : CustomEntityMapper<CustomerSubscription>
{
    public override void MapBuilder(EntityTypeBuilder<CustomerSubscription> entityBuilder)
    {
        entityBuilder
            .HasOne(d => d.Customer)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(d => d.CustomerId);
    }
}



public class AIChatCustomersMapper : CustomEntityMapper<AIChatCustomers>
{
    public override void MapBuilder(EntityTypeBuilder<AIChatCustomers> entityBuilder)
    {
        entityBuilder.HasOne(d => d.Chat);
        entityBuilder.HasOne(p => p.Customer);
    }
}
*/
