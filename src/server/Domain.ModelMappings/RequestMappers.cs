/*
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class RequestMapper : CustomEntityMapper<AIRequest>
{
    public override void MapBuilder(EntityTypeBuilder<AIRequest> entityBuilder)
    {
        entityBuilder.HasOne(p => p.Customer).WithMany().HasForeignKey(p=>p.CustomerId);
    }
}

public class ConversationRequestMapper : CustomEntityMapper<AIChatCompletionRequest>
{
    public override void MapBuilder(EntityTypeBuilder<AIChatCompletionRequest> entityBuilder)
    {
        entityBuilder.HasOne(c => c.Chat).WithMany().HasForeignKey(c=>c.ChatId);
    }
}
*/


