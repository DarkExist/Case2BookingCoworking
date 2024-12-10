using Case2BookingCoworking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Case2BookingCoworking.Infrastructure.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(e => e.Id);

            builder.HasMany(p => p.Roles).WithMany(r => r.Users);

            builder.HasOne(u => u.Profile).WithOne(p => p.User).HasForeignKey<User>(u => u.Id);

            builder.HasMany(u => u.Roles).WithMany(r => r.Users);


        }
    }
}
