using Case2BookingCoworking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Case2BookingCoworking.Infrastructure.Data.Config
{
    public class AudienceConfig : IEntityTypeConfiguration<Audience>
    {
        public void Configure(EntityTypeBuilder<Audience> builder)
        {
            builder.ToTable("Audiences");
            builder.OwnsOne(a => a.Type);
        }
    }
}
