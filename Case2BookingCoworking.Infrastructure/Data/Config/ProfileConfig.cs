using Case2BookingCoworking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Case2BookingCoworking.Infrastructure.Data.Config
{
    public class ProfileConfig : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");

            builder.HasKey(x => x.Id);
            //builder.HasOne(p => p.User).WithOne(u => u.Profile).HasForeignKey<Profile>(p => p.Id);
        }
    }
}
