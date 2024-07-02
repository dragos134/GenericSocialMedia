using GenericSocialMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GenericSocialMedia.Persistence.EntityConfigurations
{
    public class ChatUsersEntityConfiguration : IEntityTypeConfiguration<ChatUsers>
    {
        public void Configure(EntityTypeBuilder<ChatUsers> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.ChatUsers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.ChatUser)
                .WithMany(x => x.IsChatUsersFor)
                .HasForeignKey(x => x.ChatUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
