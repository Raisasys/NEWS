/*
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class UserMapper : CustomEntityMapper<User>
{
    public override void MapBuilder(EntityTypeBuilder<User> entityBuilder)
    {
        entityBuilder
            .HasOne(d => d.Person)
            .WithMany(d => d.Users)
            .HasForeignKey(e => e.PersonId)
            .IsRequired();
    }
}


public class UserTokenMapper : CustomEntityMapper<UserToken>
{
    public override void MapBuilder(EntityTypeBuilder<UserToken> entityBuilder)
    {
        entityBuilder
            .HasOne(d => d.User)
            .WithMany(p => p.Tokens)
            .HasForeignKey(d => d.UserId);
    }
}
*/
